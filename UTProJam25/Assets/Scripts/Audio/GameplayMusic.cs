using UnityEngine;
using FMOD.Studio;

public class GameplayMusic : MonoBehaviour
{   
    private EventInstance kannel;

    private void OnEnable()
    {
        LevelLoader.OnGameplayLevelLoaded += InitializeMusic;
        PlayerAim.OnHitHuman += PlaySongSFX;
    }

    private void OnDisable()
    {
        LevelLoader.OnGameplayLevelLoaded -= InitializeMusic;
        PlayerAim.OnHitHuman -= PlaySongSFX;
    }

    private void PlaySongSFX(HumanType type)
    {
        switch (type)
        {
            case HumanType.Male:
                kannel = AudioManager.Instance.CreateInstance(FMODEvents.Instance.Kannel3DSound);
                // AudioManager.PlayOneShot(FMODEvents.Instance.KannelSound);
                // kannel.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(kannel, GetComponent<Transform>(), GetComponent<Rigidbody>());
                kannel.start();
                Debug.Log("Object position: " + transform.position);

                break;
            case HumanType.Female:
                AudioManager.PlayOneShot(FMODEvents.Instance.FluteSound);
                break;
            case HumanType.Group:
                AudioManager.PlayOneShot(FMODEvents.Instance.ChoirSound);
                break;
        }
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
