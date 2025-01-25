using UnityEngine;

public class WaterMaskController : MonoBehaviour
{
    public string targetTag = "Player";  // Tag to identify the player
    public GameObject player;  // Reference to the player GameObject
    public SpriteMask waterMask;  // Reference to the SpriteMask that will mask the player sprite
    public BoxCollider2D waterCollider;  // Reference to the water's BoxCollider2D

    private SpriteRenderer playerRenderer;  // Reference to the player sprite renderer
    private float waterTopEdgeY;  // The Y-coordinate of the water's top edge

    private void Start()
    {
        // Find the player object if not already assigned in the inspector
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag(targetTag); // Find player by tag
        }

        // Get the player sprite renderer
        playerRenderer = player.GetComponent<SpriteRenderer>();
        
        if (playerRenderer == null)
        {
            Debug.LogError("Player does not have a SpriteRenderer component!");
        }

        // Get the water collider's top edge Y-coordinate (the top of the water surface)
        waterTopEdgeY = waterCollider.bounds.max.y;
    }

    private void Update()
    {
        // If the player is inside the water's BoxCollider
        Vector3 playerPosition = player.transform.position;
        
        // If the player is below the water's top edge, apply the mask effect
        if (playerPosition.y < waterTopEdgeY)
        {
            // Calculate how deep the player is into the water
            float maskDepth = Mathf.Clamp(waterTopEdgeY - playerPosition.y, 0f, waterTopEdgeY);

            // Move the water mask down to simulate the player going deeper into the water
            waterMask.transform.position = new Vector3(playerPosition.x, waterTopEdgeY - maskDepth, waterMask.transform.position.z);
            
            // Enable Sprite Mask interaction (mask the sprite inside water)
            playerRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
        else
        {
            // Player is above the water level, disable the mask
            playerRenderer.maskInteraction = SpriteMaskInteraction.None;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // When the player enters the water collider, start using the SpriteMask
        if (other.CompareTag(targetTag))
        {
            waterMask.gameObject.SetActive(true); // Enable the SpriteMask
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // When the player exits the water collider, stop using the SpriteMask
        if (other.CompareTag(targetTag))
        {
            waterMask.gameObject.SetActive(false); // Disable the SpriteMask
        }
    }
}