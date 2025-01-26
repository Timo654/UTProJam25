using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "SceneController", menuName = "Custom Scene/SceneController")]
public class SceneController : ScriptableObject
{
    public static Action OnSceneLoad;
    public static Action<bool> OnPause; // unpaused paused
    public static Action<bool> OnSettings; // open close
    public static Action<bool> OnTutorial; // open close
    private GameObject prevEvS;
    private GameObject prevEvSSettings;
    public void LoadGame(bool nextLevel = false)
    {
        SaveManager.Instance.runtimeData.previousSceneName = SceneManager.GetActiveScene().name;
        //SceneManager.LoadScene("Game");
        // not nice logic but it's almost 2 am i dont care
        if (SaveManager.Instance.runtimeData.currentLevel == null)
        {
            SaveManager.Instance.runtimeData.currentLevel = LevelLoader.Instance.GetLevels()[0];
        }
        else if (nextLevel && SaveManager.Instance.runtimeData.currentLevel.levelID == 1)
        {
            LoadCredits();
            return;
        }
        else if (nextLevel) SaveManager.Instance.runtimeData.currentLevel = LevelLoader.Instance.GetLevels()[1]; // next level mode, only lvl 0 can get to here

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
            prevEvSSettings = EventSystem.current.gameObject;
            prevEvSSettings.SetActive(false);
            SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
            OnSettings?.Invoke(true);
        }
        else
        {
            SaveManager.Instance.runtimeData.previousSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Settings");
        }
    }

    public void LoadPauseMenu(bool additive)
    {
        if (additive)
        {
            prevEvS = EventSystem.current.gameObject;
            prevEvS.SetActive(false);
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            Time.timeScale = 0f;
            OnPause?.Invoke(true);
        }
        else
        {
            SaveManager.Instance.runtimeData.previousSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("PauseMenu");
        }
    }

    public void LoadUI()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    public void LoadTutorial()
    {
        prevEvS = EventSystem.current.gameObject;
        prevEvS.SetActive(false);
        SceneManager.LoadScene("TutorialScreen", LoadSceneMode.Additive);
        Time.timeScale = 0f;
        OnTutorial?.Invoke(true);
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
        prevEvSSettings.SetActive(true);
        OnSettings?.Invoke(false);
    }
    public void UnloadTutorial()
    {
        EventSystem.current.gameObject.SetActive(false);
        SceneManager.UnloadSceneAsync("TutorialScreen");
        prevEvS.SetActive(true);
        OnSettings?.Invoke(false);
        OnTutorial?.Invoke(false);
    }

    public void UnloadPauseMenu()
    {
        EventSystem.current.gameObject.SetActive(false);
        SceneManager.UnloadSceneAsync("PauseMenu");
        prevEvS.SetActive(true);
        Time.timeScale = 1.0f;
        OnPause?.Invoke(false);
    }

    public void ExitGame()
    {
        //Application.Quit();
        LevelLoader.Instance.FadeToDesktop();
    }
}

