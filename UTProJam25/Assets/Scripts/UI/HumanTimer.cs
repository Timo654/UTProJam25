using TMPro;
using UnityEngine;

public class HumanTimer : MonoBehaviour
{
    [SerializeField]TMP_Text timerText;
    
    public void UpdateTimer(int timerValue)
    {
        timerText.text = timerValue.ToString();
    }
}
