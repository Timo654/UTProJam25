using TMPro;
using UnityEngine;

public class UIScoreUpdater : MonoBehaviour
{
    private TextMeshProUGUI m_text;

    private void Awake()
    {
        m_text = GetComponent<TextMeshProUGUI>();
        m_text.text = "0";
    }
    private void OnEnable()
    {
        StatisticsHandler.OnScoreUpdated += UpdateScore;
    }

    private void OnDisable()
    {
        StatisticsHandler.OnScoreUpdated -= UpdateScore;
    }

    private void UpdateScore(int newScore)
    {
        m_text.text = newScore.ToString();
    }
}
