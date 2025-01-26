using System;
using System.Collections;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    public static event Action<Vector3> OnWalking;
    private BoxCollider2D waterCollider;
    public Vector3 startPosition = Vector3.zero;
    private float moveDuration = 0.2f; // Time it takes to complete the movement
    private Animator animator;

    private bool isMoving = false; // Prevent multiple movements at the same time

    private void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        waterCollider = GameObject.FindWithTag("WaterCollider").GetComponent<BoxCollider2D>();
    }

    // Coroutine for smooth movement
    private IEnumerator SmoothMove(Vector3 startPosition, Vector3 targetPosition, float duration)
    {
        isMoving = true;
        
        animator.SetBool("IsWalking", true);
        OnWalking?.Invoke(transform.position);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (gameObject == null)
            {
                yield return null;
            }
            // Interpolate position between start and target
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);

            elapsedTime += Time.deltaTime; // Increment elapsed time
            yield return null; // Wait for the next frame
        }

        animator.SetBool("IsWalking", false);
        // Ensure final position is exactly at the target
        transform.position = targetPosition;

        isMoving = false;
    }

    public void MoveTowardsWater(int steps)
    {
        if (startPosition == Vector3.zero)
        {
            startPosition = transform.position;
        }
        float moveDownDistance = (startPosition.y - waterCollider.bounds.max.y) / (steps-1);
        
        if (!isMoving) // Prevent overlapping moves
        {
            Vector3 targetPosition = transform.position;
            targetPosition.y -= moveDownDistance; // Set the target position

            StartCoroutine(SmoothMove(transform.position, targetPosition, moveDuration));
        }
    }
}
