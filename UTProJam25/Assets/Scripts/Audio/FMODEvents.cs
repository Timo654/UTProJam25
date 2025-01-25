using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("SFX")]
    [field: SerializeField] public EventReference TestSound { get; private set; }
    [field: SerializeField] public EventReference WrongSound { get; private set; }

    [field: Header("UI")]
    [field: SerializeField] public EventReference ButtonClick { get; private set; }
    [field: SerializeField] public EventReference ButtonBack { get; private set; }
    [field: SerializeField] public EventReference ButtonSelect { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference BPMTest { get; private set; }
    public static FMODEvents Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
    }
}
