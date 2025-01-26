using UnityEngine;
using FMOD.Studio;

public class GameplayMusic : MonoBehaviour
{   
    private EventInstance kannel;

    private void OnEnable()
    {
        LevelLoader.OnGameplayLevelLoaded += InitializeMusic;
        PlayerAim.OnHitHuman += PlaySongSFX;
        HumanHealthSystem.OnDrownPosition += PlayDrownSFX;
        HumanController.OnWalking += PlayFootsteps;
    }

    private void OnDisable()
    {
        LevelLoader.OnGameplayLevelLoaded -= InitializeMusic;
        PlayerAim.OnHitHuman -= PlaySongSFX;
        HumanHealthSystem.OnDrownPosition -= PlayDrownSFX;
        HumanController.OnWalking -= PlayFootsteps;
    }


    private void PlayFootsteps(Vector3 position)
    {
        EventInstance audio;
        audio = AudioManager.Instance.CreateInstance(FMODEvents.Instance.StepSound);
        //Debug.Log(position);
        audio.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(position));
        audio.start();
        audio.release();
    }
    private void PlayDrownSFX(Vector3 position)
    {
        EventInstance audio;
        audio = AudioManager.Instance.CreateInstance(FMODEvents.Instance.DrownSound);
        //Debug.Log(position);
        audio.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(position));
        audio.start();
        audio.release();
    }
    private void PlaySongSFX(HumanType type, Vector3 position)
    {
        EventInstance audio;
        switch (type)
        {
            case HumanType.Male:
                audio = AudioManager.Instance.CreateInstance(FMODEvents.Instance.Kannel3DSound);
                break;
            case HumanType.Female:
                audio = AudioManager.Instance.CreateInstance(FMODEvents.Instance.Flute3DSound);
                break;
            case HumanType.Group:
                audio = AudioManager.Instance.CreateInstance(FMODEvents.Instance.Choir3DSound);
                break;
            default:
                return;
        }

        audio.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(position));
        audio.start();
        audio.release();
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
