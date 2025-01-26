using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayInputHandler : MonoBehaviour
{

    public static event Action OnAttackInput; 
    public static event Action OnPauseInput; // bool is if paused or not, true is paused

    private bool isPaused = false;
    private bool isGameNotStarted = true; // don't allow pausing on game over
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

        GameManager.OnGameEnd += DisableInput;
        PauseListener.PauseUpdated += PauseChanged;
        GameManager.OnGameStart += EnableInput;
    }

    private void OnDisable()
    {
        attack.performed -= OnAttack;
        pause.performed -= OnPause;
        attack.Disable();
        pause.Disable();
        GameManager.OnGameEnd -= DisableInput;
        PauseListener.PauseUpdated -= PauseChanged;
        GameManager.OnGameStart -= EnableInput;
    }

    private void EnableInput()
    {
        isGameNotStarted = false;
    }

    private void DisableInput()
    {
        isGameNotStarted = true;
    }

    private void PauseChanged(bool paused)
    {
        isPaused = paused;
    }
    private void OnPause(InputAction.CallbackContext context)
    {
        if (isGameNotStarted) return; // no pausing when game's over
        OnPauseInput?.Invoke();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (isPaused || isGameNotStarted) return; // no attacking while paused or endgame
        OnAttackInput?.Invoke();
    }
}
