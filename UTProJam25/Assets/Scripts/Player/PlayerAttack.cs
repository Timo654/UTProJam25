using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static event Action<bool> AttackPlayer;
    [SerializeField] private SpriteRenderer debugSprite; // debug
    private BeatManager m_beatManager;
    private int attackBeatIndex = 0; // index it uses in beatManager, ADJUST IF NEEDED

    private void Awake()
    {
        m_beatManager = FindAnyObjectByType<BeatManager>();
    }
    private void OnEnable()
    {
        GameplayInputHandler.OnAttackInput += OnAttack;
    }

    private void OnDisable()
    {
        GameplayInputHandler.OnAttackInput -= OnAttack;
    }

    private void OnAttack()
    {
        if (m_beatManager == null) return;
        bool didHit = m_beatManager.CheckIfHitClose(attackBeatIndex);
        AttackPlayer?.Invoke(didHit);
    }
}
