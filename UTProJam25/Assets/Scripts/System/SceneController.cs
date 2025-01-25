using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "SceneController", menuName = "Custom Scene/SceneController")]
public class SceneController : ScriptableObject
{
    public static Action OnSceneLoad;
    private GameObject prevEvS;
    public void LoadGame(bool nextLevel = false)
    {
        SaveManager.Instance.runtimeData.previousSceneName = SceneManager.GetActiveScene().name;
        //SceneManager.LoadScene("Game");
        // not nice logic but it's almost 2 am i dont care
        if (nextLevel && SaveManager.Instance.runtimeData.currentLevel.levelID == 1)
        {
            LoadCredits();
            return;
        }

        if (nextLevel) SaveManager.Instance.runtimeData.currentLevel = LevelLoader.Instance.GetLevels()[1];
        else SaveManager.Instance.runtimeData.currentLevel = LevelLoader.Instance.GetLevels()[0];
        
        LevelLoader.Instance.FadeToLevel("Game");
        //LoadUI();
        OnSceneLoad?.Invoke();
    }

    public void LoadMainMenu()
    {
        SaveManager.Instance.runtimeData.previousSceneName = SceneManager.GetActiveScene().name;
        //SceneManager.LoadScene("MainMenu");
        LevelLoader.Instance.FadeToLevel("MainMenu");
        OnSceneLoad?.Invoke();
    }

    public void LoadCredits()
    {
        SaveManager.Instance.runtimeData.previousSceneName = SceneManager.GetActiveScene().name;
        //SceneManager.LoadScene("Credits");
        LevelLoader.Instance.FadeToLevel("Credits");
        OnSceneLoad?.Invoke();
    }

    public void LoadSettings(bool additive)
    {
        if (additive)
        {
            prevEvS = EventSystem.current.gameObject;
            prevEvS.SetActive(false);
            SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
        }
        else
        {
            SaveManager.Instance.runtimeData.previousSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Settings");
        }
        Time.timeScale = 0f;
    }

    public void LoadUI()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    public void LoadStatistics()
    {
        prevEvS = EventSystem.current.gameObject;
        prevEvS.SetActive(false);
        SceneManager.LoadScene("StatisticsScreen", LoadSceneMode.Additive);
    }
    public void UnloadSettings()
    {
        EventSystem.current.gameObject.SetActive(false);
        SceneManager.UnloadSceneAsync("Settings");
        prevEvS.SetActive(true);
        Time.timeScale = 1.0f;
    }

    public void ExitGame()
    {
        //Application.Quit();
        LevelLoader.Instance.FadeToDesktop();
    }
}

