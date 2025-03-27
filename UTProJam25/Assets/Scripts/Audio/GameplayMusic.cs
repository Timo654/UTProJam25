using UnityEngine;
using FMOD.Studio;

public class GameplayMusic : MonoBehaviour
{   
    private EventInstance kannel;

    public AK.Wwise.Event KannelPlay;
    public AK.Wwise.Event FlutePlay;
    public AK.Wwise.Event ChoirPlay;
    public AK.Wwise.Event StepSound;
    public AK.Wwise.Event DrowningSound;

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
        //W w i s e
        GameObject tempSoundObject = new GameObject("TempFootstepSound");
        tempSoundObject.transform.position = position;

        // Add Wwise component so it can emit sound
        var akObject = tempSoundObject.AddComponent<AkGameObj>();

        // AkSoundEngine.SetObjectPosition(gameObject, position.x, position.y, position.z);
        // Post the sound event
        StepSound.Post(tempSoundObject);

        // Destroy the temp GameObject after sound plays
        Destroy(tempSoundObject, 1f);


        EventInstance audio;
        audio = AudioManager.Instance.CreateInstance(FMODEvents.Instance.StepSound);
        //Debug.Log(position);
        audio.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(position));
        audio.start();
        audio.release();
    }
    private void PlayDrownSFX(Vector3 position)
    {   
        //W w i s e
        GameObject tempSoundObject = new GameObject("TempDrowningSound");
        tempSoundObject.transform.position = position;

        // Add Wwise component so it can emit sound
        var akObject = tempSoundObject.AddComponent<AkGameObj>();

        // AkSoundEngine.SetObjectPosition(gameObject, position.x, position.y, position.z);
        // Post the sound event
        DrowningSound.Post(tempSoundObject);

        // Destroy the temp GameObject after sound plays
        Destroy(tempSoundObject, 1f);

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


        //W w i s e
        GameObject tempSoundObject = new GameObject("TempSongSound");
        tempSoundObject.transform.position = position;

        var akObject = tempSoundObject.AddComponent<AkGameObj>();

        // AkSoundEngine.SetObjectPosition(gameObject, position.x, position.y, position.z);
        switch (type)
        {
            case HumanType.Male:
                audio = AudioManager.Instance.CreateInstance(FMODEvents.Instance.Kannel3DSound);
                KannelPlay.Post(tempSoundObject);
                break;
            case HumanType.Female:
                audio = AudioManager.Instance.CreateInstance(FMODEvents.Instance.Flute3DSound);
                FlutePlay.Post(tempSoundObject);
                break;
            case HumanType.Group:
                audio = AudioManager.Instance.CreateInstance(FMODEvents.Instance.Choir3DSound);
                ChoirPlay.Post(tempSoundObject);
                break;
            default:
                return;
        }

        Destroy(tempSoundObject, 2f);

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
