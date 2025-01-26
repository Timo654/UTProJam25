using System;
using UnityEngine;

public class HumanHealthSystem : MonoBehaviour
{
    [SerializeField] private HealthBarLogic healthBarLogic;
    [SerializeField] private HumanTimer humanTimer;
    [SerializeField] private HumanController humanController;

    private int maxHealth = 100;
    private int currentHealth = 100;
    private float cooldownTimer = 5f;
    private float timerIncreaseOnHit = 0.5f;
    public HumanType gender;
    public static event Action<int> AddScoreOnDeath;
    public static event Action RanAway;
    bool isGameEnd = false;
    public void InitializeHumanStats(int maxHealth, float cooldownTimer, float timerIncreaseOnHit, HumanType gender)
    {
        this.maxHealth = maxHealth;
        this.cooldownTimer = cooldownTimer;
        this.timerIncreaseOnHit = timerIncreaseOnHit;
        this.gender = gender;

        currentHealth = maxHealth;
        healthBarLogic.InitializeHealthBar(this.maxHealth, this.maxHealth);
    }

    private void OnEnable()
    {
        GameManager.OnGameEnd += DisappearOnEnd;
    }

    private void OnDisable()
    {
        GameManager.OnGameEnd -= DisappearOnEnd;
    }

    private void DisappearOnEnd()
    {
        isGameEnd = true;
        RanAway?.Invoke(); // i invoke it but this wont show up in stats unless we work around but its probably not worth it rn
        Destroy(gameObject); // TODO - move off screen before destroying instead
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameEnd) return;
        cooldownTimer -= Time.deltaTime;
        UpdateTimer(cooldownTimer);
        if (cooldownTimer <= 0)
        {
            // TODO - move off screen instead
            RanAway?.Invoke();
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
        humanController.MoveTowardsWater(maxHealth);
        if (currentHealth <= 0) HandleDeath();
        //Add highscore
    }

    public void HandleDeath()
    {
        //Add score to highscore
        AddScoreOnDeath?.Invoke(10); // TODO - adjust score value
        //Add animation

        //If stopped animation then Destory.
        Destroy(gameObject);
    }
}
