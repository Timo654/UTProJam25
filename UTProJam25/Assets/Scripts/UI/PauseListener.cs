using System;
using UnityEngine;

public class PauseListener : MonoBehaviour
{
    public static event Action<bool> PauseUpdated;
    [SerializeField] private SceneController sceneController;
    private bool isPaused;
    private bool isSettings;
    private bool isTutorial;

    private void OnEnable()
    {
        GameplayInputHandler.OnPauseInput += OnPausePressed;
        SceneController.OnPause += Pause;
        SceneController.OnSettings += HandleSettings;
        SceneController.OnTutorial += HandleTutorial;
    }

    private void OnDisable()
    {
        GameplayInputHandler.OnPauseInput -= OnPausePressed;
        SceneController.OnPause -= Pause;
        SceneController.OnSettings -= HandleSettings;
        SceneController.OnTutorial -= HandleTutorial;
    }

    private void HandleSettings(bool isActive)
    {
        isSettings = isActive;
    }
    private void HandleTutorial(bool isActive)
    {
        isTutorial = isActive;
    }

    private void Pause(bool paused)
    {
        isPaused = paused;
        PauseUpdated?.Invoke(isPaused);
    }

    private void OnPausePressed()
    {
        if (isTutorial) return;
        if (isSettings)
        {
            Debug.Log("sure is settings");
            sceneController.UnloadSettings();
        }
        else
        {
            isPaused = !isPaused;
            if (isPaused) HandlePause();
            else HandleUnpause();
        } 
    }

    private void HandleUnpause()
    {
        sceneController.UnloadPauseMenu();
    }

    private void HandlePause()
    {
        sceneController.LoadPauseMenu(true);
    }
}
