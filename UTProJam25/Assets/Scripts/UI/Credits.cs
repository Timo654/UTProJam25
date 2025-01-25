using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    private void Awake()
    {
        if (!AudioManager.Instance.HasMusicInitialized())
            AudioManager.Instance.InitializeMusic(FMODEvents.Instance.AllMusic);
        AudioManager.Instance.SetMusicParameter("MusicSwitch", (int)BGMStage.Credits);
    }

    private void Start()
    {
        // start the song if it's not playing already
        if (AudioManager.Instance.GetMusicPosition() == 0)
            AudioManager.Instance.StartMusic();
    }
    public void OnCreditsEnd()
    {
        sceneController.LoadMainMenu();
    }
}
