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
    private bool gameActive = false;

    private void Awake()
    {
        sceneController.LoadUI();
    }

    private void Start()
    {
        LevelLoader.Instance.HandleLevelLoad();
    }
    private void OnEnable()
    {
        LevelLoader.OnGameplayLevelLoaded += SetupParams;
    }
    private void OnDisable()
    {
        LevelLoader.OnGameplayLevelLoaded -= SetupParams;
    }

    private void SetupParams(LevelData data)
    {
        Debug.Log("loaded !");
        currentTime = data.time;
        gameActive = true;
    }

    private void Update()
    {
        if (!gameActive) return;
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
        gameActive = false;
        //Load AD for bank loot moneyz
        //then Load End game UI? or more adzzz?
        OnGameEnd?.Invoke();
        //sceneController.LoadMainMenu();
        sceneController.LoadStatistics(); // NOTE - maybe not the best place for this
    }
}
