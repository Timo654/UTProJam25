using TMPro;
using UnityEngine;

public class UIStatistics : MonoBehaviour
{
    [SerializeField] private StatisticsData statsData;
    private void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Score: {statsData.score}";
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Times clicked: {statsData.successfulClicks + statsData.failedClicks}";
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"Successful clicks: {statsData.successfulClicks}";
        transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = $"Humans drowned: {statsData.peopleDrowned}";
        transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = $"Humans escaped: {statsData.peopleEscaped}";
        transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = $"Score: {statsData.score}";
        if (statsData.isHighScore) transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = $"congratulation you are get high score";
        else transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = $"you suck at this game noob";
    }
}
