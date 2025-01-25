using UnityEngine;

public class BPMTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        AudioManager.Instance.InitializeMusic(FMODEvents.Instance.BPM90Test);
    }

    private void Start()
    {
        AudioManager.Instance.StartMusic();
    }
}
