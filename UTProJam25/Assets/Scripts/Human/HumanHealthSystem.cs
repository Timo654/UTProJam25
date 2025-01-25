using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HumanHealthSystem : MonoBehaviour
{
    [SerializeField]
    private HealthBarLogic healthBarLogic;
    [SerializeField] private HumanTimer humanTimer;
    private int maxHealth = 100;
    private int currentHealth = 100;
    private float cooldownTimer = 5f;
    private float timerIncreaseOnHit = 0.5f;
    

    public void InitializeHumanStats(int maxHealth, float cooldownTimer, float timerIncreaseOnHit)
    {
        this.maxHealth = maxHealth;
        this.cooldownTimer = cooldownTimer;
        this.timerIncreaseOnHit = timerIncreaseOnHit;
        
        currentHealth = maxHealth;
        healthBarLogic.InitializeHealthBar(this.maxHealth, this.maxHealth);
    }


    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        UpdateTimer(cooldownTimer);
        if (cooldownTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateTimer(float timerValue)
    {
        humanTimer.UpdateTimer((int)timerValue);
    }

    public void TakeDamage()
    {
        currentHealth -= 1;
        cooldownTimer += timerIncreaseOnHit;
        healthBarLogic.UpdateHealth(currentHealth, maxHealth);
        
        //Add highscore
    }

    public void HandleDeath()
    {
        //Add score to highscore
        
        //Add animation
        
        //If stopped animation then Destory.
        Destroy(gameObject);
    }
}
