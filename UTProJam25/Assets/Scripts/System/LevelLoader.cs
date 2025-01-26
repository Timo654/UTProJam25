using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public static event Action<LevelData> OnGameplayLevelLoaded;
    public static event Action OnFadeInFinished;

    public LevelData debugLevelData;
    private bool loadInProgess = false;
    private LevelData levelData;
    private List<Animator> animators = new();
    private bool transitionFinished;
    private GameObject preloadBlack;
    [SerializeField] private LevelData[] levels;
    public static LevelLoader Instance { get; private set; }

    private Animator GetRandomAnimator()
    {
        Animator animator = animators[UnityEngine.Random.Range(0, animators.Count)];
        animator.gameObject.SetActive(true);
        return animator;
    }
    public void Awake()
    {
        Instance = this;
        Animator childAnimator;
        // find all transitions
        foreach (Transform child in transform)
        {
            if (child.name == "Preload")
            {
                preloadBlack = child.gameObject;
                preloadBlack.GetComponentInChildren<Image>().color = Color.black;
            }
            if (child.TryGetComponent(out childAnimator))
            {
                animators.Add(childAnimator);
            }
        }
    }

    private void OnEnable()
    {
        TransitionScript.OnEndTransition += OnTransitionFinish;
    }

    private void OnDisable()
    {
        TransitionScript.OnEndTransition -= OnTransitionFinish;
    }
    public LevelData[] GetLevels()
    {
        return levels;
    }

    public LevelData GetNextLevel(LevelData currentLevel)
    {
        var currentLvlIndex = Array.IndexOf(levels, currentLevel);
        if (currentLvlIndex == -1) // level does not exist in list
        {
            return null;
        }
        else if (currentLvlIndex + 1 < levels.Length)
        {
            return levels[currentLvlIndex + 1];
        }
        else
        {
            return null;
        }
    }

    private void OnTransitionFinish()
    {
        transitionFinished = true;
    }

    IEnumerator DisableBlack()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        preloadBlack.SetActive(false);
    }

    private void SetTrigger(Animator animator, string trigger)
    {
        animator.SetTrigger(trigger);
        StartCoroutine(DisableBlack());
    }
    public void HandleLevelLoad()
    {
        if (SaveManager.Instance.runtimeData.currentLevel == null)
        {
            if (Application.isEditor)
            {
                SaveManager.Instance.runtimeData.currentLevel = debugLevelData;
                Debug.LogWarning("Save levelData is null, loading debug level data instead.");
            }
            else
            {
                SaveManager.Instance.runtimeData.currentLevel = levels[0];
            }
        }
        levelData = SaveManager.Instance.runtimeData.currentLevel;
        OnGameplayLevelLoaded?.Invoke(levelData);
        FadeIn();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }
    public void FadeToLevel(string levelName, bool endMusic = true)
    {
        if (!loadInProgess) StartCoroutine(LoadLevel(levelName, endMusic));
        else Debug.LogWarning($"Already loading a level, cannot load {levelName}!");
    }
    public void FadeToDesktop()
    {
        if (!loadInProgess) StartCoroutine(QuitToDesktop());
        else Debug.LogWarning($"Already loading a level!");
    } 
    IEnumerator LoadLevel(string levelToLoad, bool endMusic = false)
    {
        if (SaveManager.Instance != null) SaveManager.Instance.runtimeData.previousSceneName = SceneManager.GetActiveScene().name;
        loadInProgess = true;
        if (AudioManager.Instance != null)
        {
            //if (endMusic) AudioManager.Instance.FadeOutMusic();
            AudioManager.Instance.StopSFX();
        }
        transitionFinished = false;
        SetTrigger(GetRandomAnimator(), "FadeOut");
        while (!transitionFinished)
            yield return null;
        SceneManager.LoadSceneAsync(levelToLoad);
        loadInProgess = false;
    }
    IEnumerator QuitToDesktop()
    {
        Debug.Log("Quitting.");
        loadInProgess = true;
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.FadeOutMusic();
            AudioManager.Instance.StopSFX();
        }
        transitionFinished = false;
        SetTrigger(GetRandomAnimator(), "FadeOut");
        while (!transitionFinished)
            yield return null;
        loadInProgess = false;
        Application.Quit();
    }
    IEnumerator FadeInCoroutine()
    {
        transitionFinished = false;
        SetTrigger(GetRandomAnimator(), "FadeIn");
        while (!transitionFinished)
            yield return null;
        OnFadeInFinished?.Invoke();
    }
}
