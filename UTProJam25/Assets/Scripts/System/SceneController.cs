using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "SceneController", menuName = "Custom Scene/SceneController")]
public class SceneController : ScriptableObject
{
    public static Action OnSceneLoad;
    private GameObject prevEvS;
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        LoadUI(true);
        OnSceneLoad?.Invoke();
    }


    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
            SceneManager.LoadScene("Settings");
        }
        Time.timeScale = 0f;
    }

    public void LoadUI(bool additive)
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
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
        Application.Quit();
    }
}

