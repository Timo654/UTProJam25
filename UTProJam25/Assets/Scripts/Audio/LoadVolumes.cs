using UnityEngine;

public class WwiseVolumeInitializer : MonoBehaviour
{
    private void Start()
    {
        InitializeVolume("Master_volume", "MASTER");
        InitializeVolume("Muisc_volume", "MUSIC");
        InitializeVolume("SFX_volume", "SFX");
        InitializeVolume("UI_volume", "UI");
    }

    private void InitializeVolume(string rtpcName, string prefKey)
    {
        // Load saved volume or default to 0.5
        float savedVolume = PlayerPrefs.GetFloat(prefKey + "_volume", 0.5f);
        AkSoundEngine.SetRTPCValue(rtpcName, savedVolume);
    }
}
