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
            AudioManager.Instance.InitializeMusic(levelData.levelBGM);
        if (levelData.bgmStage != BGMStage.None)
            AudioManager.Instance.SetMusicParameter("MusicSwitch", BGMStage.Music90.ToString());
    }

    private void Start()
    {
        AudioManager.Instance.StartMusic();
    }
}
