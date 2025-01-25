using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarLogic : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthText;

    public void InitializeHealthBar(int currentHealth, int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        UpdateHealth(maxHealth, maxHealth);
    }
    
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        healthText.text = currentHealth + "/" + maxHealth;
        healthSlider.value = currentHealth;
    } 
}
