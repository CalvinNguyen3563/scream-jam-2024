using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance;

    [Header("Health")]
    public float maxHealth = 100f;
    public float health = 100f;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float stamina = 100f;
    public float elapsedTimeSinceUse = 0;
    public float replenishTimeThreshold = 1f;
    public float staminaReplenishValue = 2f;
    public float staminaUseValue = 5f;

    [Header("Sliders")]
    public Slider healthSlider;
    public Slider staminaSlider;

    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
            UpdateHealthSlider(health);
        }
    }

    public float Stamina
    {
        get
        {
            return stamina;
        }

        set
        {
            CheckStaminaTimer(value, stamina);
            stamina = value;
            UpdateStaminaSlider(stamina);
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        Health = maxHealth;
        Stamina = maxStamina;

        UpdateHealthSlider(health);
        UpdateStaminaSlider(stamina);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Stamina -= 10;
        }

    }

    private void FixedUpdate()
    {
        ReplenishStamina();
    }
    private void UpdateHealthSlider(float health)
    {
        healthSlider.value = health / maxHealth;
    }

    public void DecreaseHealth(float value)
    {
        if (Health - value < 0)
        {
            Health = 0;
        }
        else
        {
            Health -= value;
        }
    }

    public void IncreaseHealth(float value)
    {
        if (Health + value < maxHealth)
        {
            Health = maxHealth;
        }
        else
        {
            Health += value;
        }
    }

    private void UpdateStaminaSlider(float stamina)
    {
        staminaSlider.value = stamina / maxStamina; 
    }

    // check how long since stamina was used
    private void CheckStaminaTimer(float newValue, float oldValue)
    {
        if (newValue < oldValue)
        {
            elapsedTimeSinceUse = 0f;
        }
    }

    private void ReplenishStamina()
    {
        if (elapsedTimeSinceUse > replenishTimeThreshold && !PlayerInputManager.instance.sprintInput)
        {
            if (Stamina >= 100)
            {
                Stamina = 100;
            }
            else
            {
                Stamina += staminaReplenishValue;
            }
        }

        elapsedTimeSinceUse += Time.deltaTime;
    }

    public void DecreaseStamina(float value = -1)
    {

        if (Stamina > 0)
        {
            if (value > 0)
            {
                Stamina -= value;
            }
            else
            {
                Stamina -= staminaUseValue;
            }
        }
        else
        {
            Stamina = 0;
        }
    }

   

    
}
