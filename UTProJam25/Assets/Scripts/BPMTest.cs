using UnityEngine;

public class BPMTest : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {           
        var _beatManager = FindAnyObjectByType<BeatManager>();
        if (_beatManager != null)
            _beatManager.SetBPM(levelData.levelBPM);
        else Debug.LogWarning("how is the beat manager gone ????????");
        if (!AudioManager.Instance.HasMusicInitialized())
            AudioManager.Instance.InitializeMusic(FMODEvents.Instance.AllMusic);
<<<<<<< HEAD
        // if (levelData.bgmStage != BGMStage.None)
        AudioManager.Instance.SetMusicParameter("MusicSwitch", 2);
=======
        if (levelData.bgmStage != BGMStage.None)
        AudioManager.Instance.SetMusicParameter("MusicSwitch", (int)levelData.bgmStage);
>>>>>>> 537cf781649d4f7b7716e2feebbd1ca15f79fd7e
    }

    private void Start()
    {
        // start the song if it's not playing already
        if (AudioManager.Instance.GetMusicPosition() == 0)
            AudioManager.Instance.StartMusic();
    }
}
