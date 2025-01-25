using System;
using UnityEngine;

public class StatisticsHandler : MonoBehaviour
{
    public static event Action<int> OnScoreUpdated;

    private int score = 0;
    private void OnEnable()
    {
        HumanHealthSystem.AddScoreOnDeath += AddScore;
        GameManager.OnGameEnd += OnGameEnd;
        // TODO add more funny stats here
    }

    private void OnDisable()
    {
        HumanHealthSystem.AddScoreOnDeath -= AddScore;
        GameManager.OnGameEnd -= OnGameEnd;
    }

    private void AddScore(int toAdd)
    {
        score += toAdd;
        OnScoreUpdated?.Invoke(score);
    }


    private void OnGameEnd()
    {
        bool highScore = score > SaveManager.Instance.gameData.highScore;
        // TODO - do something if high score
        SaveManager.Instance.gameData.highScore = score;
    }
}
