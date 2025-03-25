using UnityEngine;
using UnityEngine.UI;

public class WwiseVolumeSlider : MonoBehaviour
{
    private enum VolumeType
    {
        MASTER,
        MUSIC,
        SFX,
        UI
    }

    [Header("Volume Type")]
    [SerializeField] private VolumeType volumeType;

    private Slider volumeSlider;

    private void Start()
    {
        volumeSlider = GetComponent<Slider>();

        // Load saved volume or default to 0.5
        float savedVolume = PlayerPrefs.GetFloat(volumeType.ToString() + "_volume", 0.5f);
        volumeSlider.value = savedVolume;

        // Apply saved/default volume
        SetVolume(savedVolume);

        // Add listener for live slider changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float value)
    {
        float clampedValue = Mathf.Clamp01(value);  // Ensure it's within 0-1 range
        string busRTPC = GetBusRTPCName();

        // Apply volume to Wwise RTPCs directly in 0-1 range
        AkSoundEngine.SetRTPCValue(busRTPC, clampedValue);

        // Save this new value
        PlayerPrefs.SetFloat(volumeType.ToString() + "_volume", clampedValue);
        PlayerPrefs.Save();
    }

    private string GetBusRTPCName()
    {
        // Match this to the exact RTPC names in Wwise
        switch (volumeType)
        {
            case VolumeType.MASTER:
                return "Master_volume";
            case VolumeType.MUSIC:
                return "Music_volume";
            case VolumeType.SFX:
                return "SFX_volume";
            case VolumeType.UI:
                return "UI_volume";
            default:
                Debug.LogWarning("Unknown volume type!");
                return "RTPC_MasterVolume";
        }
    }
}
