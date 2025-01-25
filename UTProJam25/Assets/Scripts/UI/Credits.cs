using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    private void Awake()
    {
        if (!AudioManager.Instance.HasMusicInitialized())
            AudioManager.Instance.InitializeMusic(FMODEvents.Instance.AllMusic);
        AudioManager.Instance.SetMusicParameter("MusicSwitch", 1);
    }

    private void Start()
    {
        AudioManager.Instance.StartMusic();
    }
    public void OnCreditsEnd()
    {
        sceneController.LoadMainMenu();
    }
}
