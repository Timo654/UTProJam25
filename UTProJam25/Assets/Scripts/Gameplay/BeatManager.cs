using System;
using UnityEngine;
using UnityEngine.Events;

public class BeatManager : MonoBehaviour
{
    // based on https://www.youtube.com/watch?v=gIjajeyjRfE
    [SerializeField] private float _bpm;
    [SerializeField] private Intervals[] _intervals;
    private float _startOffset;

    // startoffset is song start position in milliseconds
    public void SetBPM(float bpm, int startOffset)
    {
        _bpm = bpm;
        _startOffset = (startOffset * AudioManager.Instance.GetSampleRate() / 1000);
    }
    private void Update()
    {
        if (AudioManager.Instance.GetMusicPosition() == 0) return; // not playing the song yet

        foreach (var interval in _intervals)
        {
            float sampledTime = ((_startOffset - AudioManager.Instance.GetMusicPosSamples()) / (AudioManager.Instance.GetSampleRate() * interval.GetBeatLength(_bpm)));
            interval.CheckForNewInterval(sampledTime);
        }
    }

    public bool CheckIfHitClose(int intervalIndex)
    {
        float sampledTime = (AudioManager.Instance.GetMusicPosSamples() / (AudioManager.Instance.GetSampleRate() * _intervals[intervalIndex].GetBeatLength(_bpm)));
        return _intervals[intervalIndex].CheckIfCloseEnough(sampledTime);
    }
}

[Serializable]
public class Intervals
{
    [SerializeField] private float _subdivisions;
    [SerializeField] private UnityEvent _trigger;
    private int _lastInterval;

    private bool isHitPrevInterval;
    private bool isHitCurrentInterval;

    public float GetBeatLength(float bpm)
    {
        return 60f / (bpm * _subdivisions);
    }

    // TODO - more precise checks, but for now its either all or nothing

    public bool CheckIfCloseEnough(float interval)
    {
        float diff = interval - _lastInterval;
        if (diff <= 0.15f)
        {
            // hit late
            if (isHitPrevInterval) return false;
            return true;
        }
        else if (diff >= 0.85f)
        {
            // hit early
            if (isHitCurrentInterval) return false;

            isHitCurrentInterval = true;

            return true;
        }
        return false;
    }

    public void CheckForNewInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != _lastInterval)
        {
            // interval is larger by one
            isHitPrevInterval = isHitCurrentInterval;
            isHitCurrentInterval = false;
            _lastInterval = Mathf.FloorToInt(interval);
            _trigger.Invoke();
        }
    }
}
