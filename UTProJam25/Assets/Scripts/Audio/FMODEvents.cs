using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("SFX")]
    [field: SerializeField] public EventReference TestSound { get; private set; }
    [field: SerializeField] public EventReference WrongSound { get; private set; }
    [field: SerializeField] public EventReference KannelSound { get; private set; }
    [field: SerializeField] public EventReference Kannel3DSound { get; private set; }
    [field: SerializeField] public EventReference ChoirSound { get; private set; }
    [field: SerializeField] public EventReference Choir3DSound { get; private set; }
    [field: SerializeField] public EventReference BagpipeSound { get; private set; }
    [field: SerializeField] public EventReference Bagpipe3DSound { get; private set; }
    [field: SerializeField] public EventReference FluteSound { get; private set; }
    [field: SerializeField] public EventReference Flute3DSound { get; private set; }

    [field: SerializeField] public EventReference DrownSound { get; private set; }
    

    [field: Header("UI")]
    [field: SerializeField] public EventReference ButtonClick { get; private set; }
    [field: SerializeField] public EventReference ButtonBack { get; private set; }
    [field: SerializeField] public EventReference ButtonSelect { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference AllMusic { get; private set; }
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


public enum BGMStage
{
    None = -1,
    MainMenu = 0,
    Credits = 1,
    Music90 = 2,
    Music120 = 3
}