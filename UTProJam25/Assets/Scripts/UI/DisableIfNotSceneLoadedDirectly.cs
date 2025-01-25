using UnityEngine;

public class DisableIfNotSceneLoadedDirectly : MonoBehaviour
{
    // this is to stop that annoying black screen from hopefully ever appearing again during testing
    void Awake()
    {
        if (SaveManager.Instance.runtimeData.previousSceneName.Length > 0 )
            gameObject.SetActive(false);
    }
}
