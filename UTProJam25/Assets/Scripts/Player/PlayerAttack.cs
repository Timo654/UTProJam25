using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public static event Action<bool> AttackPlayer;
    [SerializeField] private SpriteRenderer debugSprite; // debug
    private BeatManager m_beatManager;
    private int attackBeatIndex = 0; // index it uses in beatManager, ADJUST IF NEEDED
    private InputAction attack;
    private void Awake()
    {
        m_beatManager = FindAnyObjectByType<BeatManager>();
        attack = InputSystem.actions.FindAction("Attack");
    }
    private void OnEnable()
    {
        attack.performed += OnAttack;
        attack.Enable();
        GameManager.OnGameEnd += StopPlayer;
    }

    private void OnDisable()
    {
        attack.performed -= OnAttack;
        GameManager.OnGameEnd -= StopPlayer;
    }

    private void StopPlayer()
    {
        attack.Disable();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (m_beatManager == null) return;
        bool didHit = m_beatManager.CheckIfHitClose(attackBeatIndex);
        AttackPlayer?.Invoke(didHit);
    }
}
