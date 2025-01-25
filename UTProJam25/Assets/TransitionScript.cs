using System;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    public static event Action OnEndTransition;

    public void EndTransition()
    {
        OnEndTransition?.Invoke();
    }
}
