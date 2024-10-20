using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatsManager : MonoBehaviour
{
    public static MonsterStatsManager Instance;
    public float maxHealth;
    public float health = 1f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        maxHealth = health;
    }

    public void DecreaseHealth(float damage)
    {
        if (health - damage < 0)
        {
            health = 0f;
        }
        else
        {
            health -= damage;   
        }
    }
}
