using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

// from https://gamedevbeginner.com/how-to-keep-score-in-unity-with-loading-and-saving/#save_with_xml
public class SaveManager : MonoSingleton<SaveManager>
{
    public SystemData systemData;
    public GameData gameData;
    public RuntimeData runtimeData; // for values that won't get saved to disk, but we use in-game
    private static string mPersistentDataPath;

    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        mPersistentDataPath = "idbfs/UTProJam25"; 
#else
        mPersistentDataPath = Application.persistentDataPath;
#endif
        if (!Directory.Exists(mPersistentDataPath)) // webgl does not automatically make the folder
        {
            Directory.CreateDirectory(mPersistentDataPath);
        }
        systemData = LoadData<SystemData>("options.json");
        gameData = LoadData<GameData>("data.json");
        runtimeData = new();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SaveAll();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Autosave on scene load.
    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SaveAll();
    }

    private static string GetSaveFilePath(string fileName)
    {
        return Path.Combine(mPersistentDataPath, fileName);
    }

    private static void SaveAll()
    {
        if (Instance.systemData != null) { SaveData(Instance.systemData, "options.json"); }
        if (Instance.gameData != null) { SaveData(Instance.gameData, "data.json"); }
    }

    public LevelSave GetLevelSave(int levelID)
    {
        LevelSave saveData;
        if (gameData.levelSaves.ContainsKey(levelID))
        {
            saveData = gameData.levelSaves[levelID];
        }
        else
        {
            gameData.levelSaves[levelID] = saveData = new LevelSave { score = 0 };
        }
        return saveData;
    }

    private static void SaveData<T>(T saveData, string fileName)
    {
        if (!typeof(T).IsSerializable)
        {
            throw new InvalidOperationException("A serializable type is required");
        }
        string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
        File.WriteAllText(GetSaveFilePath(fileName), json);
    }

    private static T LoadData<T>(string fileName) where T : new()
    {
        if (!typeof(T).IsSerializable)
        {
            throw new InvalidOperationException("A serializable type is required");
        }
        T data = new();
        if (File.Exists(GetSaveFilePath(fileName)))
        {
            string json = File.ReadAllText(GetSaveFilePath(fileName));
            data = JsonConvert.DeserializeObject<T>(json);
        }
        return data;
    }
}

[Serializable]
public class SystemData
{
    public float MasterVolume = 1f;
    public float SFXVolume = 0.5f;
    public float UIVolume = 0.4f;
    public float MusicVolume = 0.2f;
}

[Serializable]
public class GameData
{
    public int version = 2;
    public int money = 0;
    public Dictionary<int, LevelSave> levelSaves = new(); // levelid - score
}

[Serializable]
public class RuntimeData
{
    public string previousSceneName;
    public LevelData currentLevel;
}

[Serializable]
public class LevelSave
{
    public int score;
}

