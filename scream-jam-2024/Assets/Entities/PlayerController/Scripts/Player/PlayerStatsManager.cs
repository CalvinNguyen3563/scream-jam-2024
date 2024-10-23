using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
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

    [Header("Post Processing")]
    public Volume volume;
    public Vignette vignette;
    public float timeToFlashRed = 1.5f;

    private void Start()
    {
        volume.profile.TryGet<Vignette>(out vignette);
    }


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

        if (Health <= 40)
        {
            // Pulsate between red and light red
            float pulseSpeed = 1f; // Speed of the pulsating effect
            Color redColor = Color.red;
            Color lightRedColor = new Color(1f, 0.5f, 0.5f); // A lighter shade of red
            float time = Mathf.PingPong(Time.time * pulseSpeed, 1f); // Oscillates between 0 and 1
            vignette.color.value = Color.Lerp(redColor, lightRedColor, time);
        }
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


        if (Health >= 25)
        {
            StartCoroutine(FlashRed(timeToFlashRed));
        }
    }

    public void IncreaseHealth(float value)
    {
        if (Health + value > maxHealth)
        {
            Health = maxHealth;
        }
        else
        {
            Health += value;
        }

        StartCoroutine(FlashGreen(timeToFlashRed));
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

    public IEnumerator FlashRed(float time)
    {
        vignette.color.value = Color.red;   
        float elapsedTime = 0;
        Color originalColor = vignette.color.value;

        while (elapsedTime < time)
        {
            float fixedTime = elapsedTime / time;
            vignette.color.value = Color.Lerp(originalColor, Color.black, fixedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        vignette.color.value = Color.black;
    }

    public IEnumerator FlashGreen(float time)
    {
        vignette.color.value = Color.green;
        float elapsedTime = 0;
        Color originalColor = vignette.color.value;

        while (elapsedTime < time)
        {
            float fixedTime = elapsedTime / time;
            vignette.color.value = Color.Lerp(originalColor, Color.black, fixedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        vignette.color.value = Color.black;
    }


}
