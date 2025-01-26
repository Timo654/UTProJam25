using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameStart;
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
        SceneController.OnTutorial += HandleTutorialEnd;
    }
    private void OnDisable()
    {
        LevelLoader.OnGameplayLevelLoaded -= SetupParams;
        SceneController.OnTutorial -= HandleTutorialEnd;
    }

    private void HandleTutorialEnd(bool isActive)
    {
        if (!isActive)
        {
            gameActive = true;
            Time.timeScale = 1.0f; // in case it gets set somewhere else, ensure we're always at 1.
            OnGameStart?.Invoke();
        }
    }

    private void SetupParams(LevelData data)
    {
        Debug.Log("loaded !");
        currentTime = data.time;

        if (SaveManager.Instance.runtimeData.currentLevel.levelID == 0) // only use tutorial for lvl1
            sceneController.LoadTutorial();
        else
        {
            gameActive = true;
            Time.timeScale = 1.0f; // in case it gets set somewhere else, ensure we're always at 1.
            OnGameStart?.Invoke();
        } 
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
