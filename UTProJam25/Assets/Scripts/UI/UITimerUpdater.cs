using System;
using TMPro;
using UnityEngine;

public class UITimerUpdater : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;

    private void OnEnable()
    {
        GameManager.OnGameTimeChanged += UpdateUITimerText;
    }

    private void OnDisable()
    {
        GameManager.OnGameTimeChanged -= UpdateUITimerText;
    }

    private void UpdateUITimerText(int time)
    {
        timerText.text = time.ToString();
    }
}
