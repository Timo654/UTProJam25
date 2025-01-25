using UnityEngine;

public class GameplayMusic : MonoBehaviour
{
    private void OnEnable()
    {
        LevelLoader.OnGameplayLevelLoaded += InitializeMusic;
    }

    private void OnDisable()
    {
        LevelLoader.OnGameplayLevelLoaded -= InitializeMusic;
    }

    private void InitializeMusic(LevelData data)
    {
        var _beatManager = FindAnyObjectByType<BeatManager>();
        if (_beatManager != null)
            _beatManager.SetBPM(data.levelBPM, data.songOffset, data.mercyRange);
        else Debug.LogWarning("how is the beat manager gone ????????");
        if (!AudioManager.Instance.HasMusicInitialized())
            AudioManager.Instance.InitializeMusic(FMODEvents.Instance.AllMusic);
        if (data.bgmStage != BGMStage.None)
            AudioManager.Instance.SetMusicParameter("MusicSwitch", (int)data.bgmStage);
    }

    private void Start()
    {
        // start the song if it's not playing already
        if (AudioManager.Instance.GetMusicPosition() == 0)
            AudioManager.Instance.StartMusic();
    }
}
