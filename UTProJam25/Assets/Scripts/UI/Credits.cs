using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Credits : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    IDisposable listener;
    private void Awake()
    {
        if (!AudioManager.Instance.HasMusicInitialized())
            AudioManager.Instance.InitializeMusic(FMODEvents.Instance.AllMusic);
        AudioManager.Instance.SetMusicParameter("MusicSwitch", (int)BGMStage.Credits);
        listener = InputSystem.onAnyButtonPress.CallOnce(OnSkipCredits);
    }

    private void OnSkipCredits(InputControl control)
    {
        OnCreditsEnd();
    }

    private void Start()
    {
        // start the song if it's not playing already
        if (AudioManager.Instance.GetMusicPosition() == 0)
            AudioManager.Instance.StartMusic();
        LevelLoader.Instance.FadeIn();
    }
    public void OnCreditsEnd()
    {
        listener.Dispose();
        sceneController.LoadMainMenu();
    }
}
