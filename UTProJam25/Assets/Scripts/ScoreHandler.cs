using System;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public static event Action<int> OnScoreUpdated;
    private int score = 0;
    private void OnEnable()
    {
        HumanHealthSystem.AddScoreOnDeath += AddScore;
    }

    private void OnDisable()
    {
        HumanHealthSystem.AddScoreOnDeath -= AddScore;
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
