using System;
using UnityEngine;

public class PauseListener : MonoBehaviour
{
    public static event Action<bool> PauseUpdated;
    [SerializeField] private SceneController sceneController;
    private bool isPaused;
    private bool isSettings;
    private void OnEnable()
    {
        GameplayInputHandler.OnPauseInput += OnPausePressed;
        SceneController.OnPause += Pause;
        SceneController.OnSettings += HandleSettings;
    }

    private void OnDisable()
    {
        GameplayInputHandler.OnPauseInput -= OnPausePressed;
        SceneController.OnPause -= Pause;
        SceneController.OnSettings -= HandleSettings;
    }

    private void HandleSettings(bool isTrue)
    {
        Debug.Log("settings yay !");
        isSettings = isTrue;
    }

    private void Pause(bool paused)
    {
        isPaused = paused;
        PauseUpdated?.Invoke(isPaused);
    }

    private void OnPausePressed()
    {
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
