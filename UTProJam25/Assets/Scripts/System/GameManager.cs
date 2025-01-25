using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameLoopSettings gameLoopSettings;
    [SerializeField] private SceneController sceneController;
    private float currentTime;
    private int lastTimeValue;

    public static event Action<int> OnGameTimeChanged;

    private void Start()
    {
        currentTime = gameLoopSettings.gameDuration;
    }

    private void Update()
    {
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
        //Load AD for bank loot moneyz
        //then Load End game UI? or more adzzz?
        sceneController.LoadMainMenu();
    }
}
