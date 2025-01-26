using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayInputHandler : MonoBehaviour
{

    public static event Action OnAttackInput; 
    public static event Action OnPauseInput; // bool is if paused or not, true is paused

    private bool isPaused = false;
    private bool isGameOver = false; // don't allow pausing on game over
    InputAction attack;
    InputAction pause;
    private void Awake()
    {
        attack = InputSystem.actions.FindAction("Attack");
        pause = InputSystem.actions.FindAction("Pause");
    }

    private void OnEnable()
    {
        attack.performed += OnAttack;
        pause.performed += OnPause;
        attack.Enable();
        pause.Enable();

        GameManager.OnGameEnd += SetGameEnd;
        PauseListener.PauseUpdated += PauseChanged;
    }

    private void OnDisable()
    {
        attack.performed -= OnAttack;
        pause.performed -= OnPause;
        attack.Disable();
        pause.Disable();
        GameManager.OnGameEnd -= SetGameEnd;
        PauseListener.PauseUpdated -= PauseChanged;
    }

    private void SetGameEnd()
    {
        isGameOver = true;
    }

    private void PauseChanged(bool paused)
    {
        isPaused = paused;
    }
    private void OnPause(InputAction.CallbackContext context)
    {
        if (isGameOver) return; // no pausing when game's over
        OnPauseInput?.Invoke();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (isPaused || isGameOver) return; // no attacking while paused or endgame
        OnAttackInput?.Invoke();
    }
}
