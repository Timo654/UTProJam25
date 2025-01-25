using UnityEngine;

[CreateAssetMenu(fileName = "StatisticsData", menuName = "Stats/Statistics")]
public class StatisticsData : ScriptableObject
{
    public int score;
    public int successfulClicks;
    public int failedClicks;
    public int peopleDrowned;
    public int peopleEscaped;
    public bool isHighScore;
}
