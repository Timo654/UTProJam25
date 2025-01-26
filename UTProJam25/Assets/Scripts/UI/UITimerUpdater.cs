using System;
using TMPro;
using UnityEngine;

public class UITimerUpdater : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;

    private void Start()
    {
        if (SaveManager.Instance.runtimeData.currentLevel != null)
        {
            timerText.text = SaveManager.Instance.runtimeData.currentLevel.time.ToString();
        }
    }
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
