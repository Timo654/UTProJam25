using UnityEngine;

public class MenuMusic : MonoBehaviour
{   
    public AK.Wwise.Event MenuStart;
    [SerializeField]
    private AK.Wwise.Switch MenuStartSwitch;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //fmod
        if (!AudioManager.Instance.HasMusicInitialized())
            AudioManager.Instance.InitializeMusic(FMODEvents.Instance.AllMusic);
        AudioManager.Instance.SetMusicParameter("MusicSwitch", (int)BGMStage.MainMenu);
    }

    private void Start()
    {
        // start the song if it's not playing already
        //fmod
        if (AudioManager.Instance.GetMusicPosition() == 0)
            AudioManager.Instance.StartMusic();

        //wwise
        MenuStart.Post(gameObject);
        MenuStartSwitch.SetValue(this.gameObject);

        LevelLoader.Instance.FadeIn();
    }
}
