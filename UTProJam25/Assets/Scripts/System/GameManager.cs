using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameEnd;
    [SerializeField] private GameLoopSettings gameLoopSettings;
    [SerializeField] private SceneController sceneController;
    private float currentTime;
    private int lastTimeValue;

    public static event Action<int> OnGameTimeChanged;
    private bool gameEnded = false;
    private void Start()
    {
        currentTime = gameLoopSettings.gameDuration;
    }

    private void Update()
    {
        if (gameEnded) return;
        if (currentTime <= 0)
        {
            EndGame();
        }
        else
        {
            currentTime -= Time.deltaTime;
        }

        if (lastTimeValue != (int)currentTime)
        {
            lastTimeValue = (int)currentTime;
            OnGameTimeChanged?.Invoke(lastTimeValue);
        }
    }

    private void EndGame()
    {
        gameEnded = true;
        //Load AD for bank loot moneyz
        //then Load End game UI? or more adzzz?
        OnGameEnd?.Invoke();
        //sceneController.LoadMainMenu();
        sceneController.LoadStatistics(); // NOTE - maybe not the best place for this
    }
}
