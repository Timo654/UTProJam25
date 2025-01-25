using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{

    List<GameObject> gameobjectsInView = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnEnable()
    {
        PlayerAttack.AttackPlayer += HandleAttack;
    }

    private void OnDisable()
    {
        PlayerAttack.AttackPlayer -= HandleAttack;
    }

    private void HandleAttack(bool onBeat)
    {
        List<HumanHealthSystem> toDamage = new List<HumanHealthSystem>();

        for (int i = 0; i < gameobjectsInView.Count; i++)
        {
            var obj = gameobjectsInView[i];
            if (obj.TryGetComponent(out SpriteRenderer sr))
            {
                sr.color = onBeat ? Color.green : Color.red;
            }
            if (onBeat)
            {
                if (obj.TryGetComponent(out HumanHealthSystem hhs))
                {
                    toDamage.Add(hhs);
                }
            }
        }

        for (int i = 0; i < toDamage.Count; i++)
        {
            toDamage[i].TakeDamage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        var lookAtPos = Input.mousePosition;
        lookAtPos.z = transform.position.z - Camera.main.transform.position.z;
        lookAtPos = Camera.main.ScreenToWorldPoint(lookAtPos);
        transform.up = lookAtPos - transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"added {collision.gameObject.name}");
        gameobjectsInView.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log($"removed {collision.gameObject.name}");
        gameobjectsInView.Remove(collision.gameObject);
    }
}
