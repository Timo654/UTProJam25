using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public static event Action<HumanType, Vector3> OnHitHuman; // type and pos
    List<GameObject> gameobjectsInView = new();
    private bool gameActive = false;

    private void OnEnable()
    {
        PlayerAttack.AttackPlayer += HandleAttack;
        GameManager.OnGameEnd += StopPlayer;
        PauseListener.PauseUpdated += HandlePause;
        GameManager.OnGameStart += StartPlayer;
    }

    private void OnDisable()
    {
        PlayerAttack.AttackPlayer -= HandleAttack;
        GameManager.OnGameEnd -= StopPlayer;
        PauseListener.PauseUpdated -= HandlePause;
        GameManager.OnGameStart -= StartPlayer;
    }

    private void StartPlayer()
    {
        gameActive = true;
    }

    private void HandlePause(bool paused)
    {
        gameActive = !paused;
    }

    private void StopPlayer()
    {
        gameActive = false;
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

        if (toDamage.Count == 1) // single
        {
            OnHitHuman?.Invoke(toDamage[0].humanType, toDamage[0].transform.position);
        }
        else if (toDamage.Count > 1) // group
        {
            OnHitHuman?.Invoke(HumanType.Group, toDamage[0].transform.position);
        }

        for (int i = 0; i < toDamage.Count; i++)
        {
            toDamage[i].TakeDamage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameActive) return;
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


public enum HumanType
{
    Male,
    Female,
    Group
}