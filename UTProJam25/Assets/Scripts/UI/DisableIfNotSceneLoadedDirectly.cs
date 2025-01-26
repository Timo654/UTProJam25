using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableIfNotSceneLoadedDirectly : MonoBehaviour
{
    // this is to stop that annoying black screen from hopefully ever appearing again during testing
    void Awake()
    {
        if (SaveManager.Instance.runtimeData.previousSceneName != null || SceneManager.GetActiveScene().name != "Ui")
            gameObject.SetActive(false);
    }
}
