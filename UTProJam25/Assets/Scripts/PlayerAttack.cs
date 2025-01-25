using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
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
    }

    private void OnDisable()
    {
        attack.performed -= OnAttack;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        bool didHit = m_beatManager.CheckIfHitClose(attackBeatIndex);
        debugSprite.color = didHit ? Color.green : Color.red;
    }
}
