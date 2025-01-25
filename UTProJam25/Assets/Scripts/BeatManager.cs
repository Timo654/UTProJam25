using System;
using UnityEngine;
using UnityEngine.Events;

public class BeatManager : MonoBehaviour
{
    // based on https://www.youtube.com/watch?v=gIjajeyjRfE
    [SerializeField] private float _bpm;
    [SerializeField] private Intervals[] _intervals;

    private void Update()
    {
        if (AudioManager.Instance.GetMusicPosition() == 0) return; // not playing the song yet

        foreach (var interval in _intervals)
        {
            float sampledTime = (AudioManager.Instance.GetMusicPosSamples() / (AudioManager.Instance.GetSampleRate() * interval.GetBeatLength(_bpm)));
            interval.CheckForNewInterval(sampledTime);
        }
    }
}

[Serializable]
public class Intervals
{
    [SerializeField] private float _subdivisions;
    [SerializeField] private UnityEvent _trigger;
    private int _lastInterval;

    public float GetBeatLength(float bpm)
    {
        return 60f / (bpm * _subdivisions);
    }

    public void CheckForNewInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != _lastInterval)
        {
            _lastInterval = Mathf.FloorToInt(interval);
            _trigger.Invoke();
        }
    }
}
