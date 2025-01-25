using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnCreditsEnd()
    {
        sceneController.LoadMainMenu();
    }
}
