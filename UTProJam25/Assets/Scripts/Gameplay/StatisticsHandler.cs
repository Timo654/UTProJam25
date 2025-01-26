using System;
using UnityEngine;

public class StatisticsHandler : MonoBehaviour
{
    public static event Action<int> OnScoreUpdated;

    [SerializeField] private StatisticsData statsData;

    private void Awake()
    {
        statsData.score = 0;
        statsData.successfulClicks = 0;
        statsData.failedClicks = 0;
        statsData.peopleDrowned = 0;
        statsData.peopleEscaped = 0;
    }
    private void OnEnable()
    {
        HumanHealthSystem.AddScoreOnDeath += AddScore;
        GameManager.OnGameEnd += OnGameEnd;
        HumanHealthSystem.RanAway += AddEscaped;
        PlayerAttack.AttackPlayer += OnAttack;
        // TODO add more funny stats here
    }

    private void OnDisable()
    {
        HumanHealthSystem.AddScoreOnDeath -= AddScore;
        GameManager.OnGameEnd -= OnGameEnd;
        HumanHealthSystem.RanAway -= AddEscaped;
        PlayerAttack.AttackPlayer -= OnAttack;
    }

    private void OnAttack(bool success)
    {
        if (success) statsData.successfulClicks++;
        else statsData.failedClicks++;
    }

    private void AddEscaped()
    {
        statsData.peopleEscaped++;
    }

    private void AddScore(int toAdd)
    {
        statsData.peopleDrowned++;
        statsData.score += toAdd;
        OnScoreUpdated?.Invoke(statsData.score);
    }


    private void OnGameEnd()
    {
        var saveManager = SaveManager.Instance;
        bool highScore = statsData.score > saveManager.GetLevelSave(saveManager.runtimeData.currentLevel.levelID).score;
        statsData.isHighScore = highScore;
        if (highScore)
            saveManager.GetLevelSave(saveManager.runtimeData.currentLevel.levelID).score = statsData.score;
    }
}
