using System;
using UnityEngine;

public class SendUIBPMEvent : MonoBehaviour
{
    public static event Action OnBeat;

    public void RunOnBeat()
    {
        OnBeat?.Invoke();
    }
}
