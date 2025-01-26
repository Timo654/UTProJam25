using UnityEngine;

public class HealthBarLogic : MonoBehaviour
{
    [SerializeField] private GameObject hpHeart;
    private Transform healthParent;

    private void Awake()
    {
        healthParent = transform.GetChild(0);
    }
    public void InitializeHealthBar(int currentHealth, int maxHealth)
    {
        for (int i = 0; i < currentHealth; i++)
        {
            Instantiate(hpHeart, healthParent);
        }
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        if (healthParent.childCount > currentHealth) // kill some children
        {
            for (int i = 1; i < healthParent.childCount - currentHealth + 1; i++)
            {
                Destroy(healthParent.GetChild(healthParent.childCount - i).gameObject);
            }
        }
    }
}
