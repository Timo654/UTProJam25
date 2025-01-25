using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Levels/New Level")]
public class LevelData : ScriptableObject
{
    public EventReference levelBGM;
    public BGMStage bgmStage;
    public int levelBPM;
    // put any other level specific variables here
}
