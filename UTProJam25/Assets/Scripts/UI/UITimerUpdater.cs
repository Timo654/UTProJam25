using System;
using TMPro;
using UnityEngine;

public class UITimerUpdater : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;

    private void Start()
    {
        GameManager.OnGameTimeChanged += UpdateUITimerText;
    }

    private void UpdateUITimerText(int time)
    {
        timerText.text = time.ToString();
    }
}
