using UnityEngine;

public class UIBPMHandler : MonoBehaviour
{
    ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        if (SaveManager.Instance.runtimeData.currentLevel != null)
        {
            SetupBPM(SaveManager.Instance.runtimeData.currentLevel);
        }
    }
    private void OnEnable()
    {
        LevelLoader.OnGameplayLevelLoaded += SetupBPM;
    }

    private void OnDisable()
    {
        LevelLoader.OnGameplayLevelLoaded -= SetupBPM;
    }

    private void SetupBPM(LevelData data)
    {
        //var main = _particleSystem.main;
        //main.simulationSpeed = data.levelBPM / 60f;
        //Debug.Log($"UI BPM simulation speed is {main.simulationSpeed}");
    }
}
