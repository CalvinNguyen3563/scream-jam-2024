using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCombatScript : MonoBehaviour
{
    public MonsterManager monster;
    public List<Collider> collidersDamaged = new List<Collider>();
    public Collider attackCollider;
    public float attackDmg = 10f;

    private void Awake()
    {
        monster = GetComponentInParent<MonsterManager>();
    }

    private void Update()
    {
        DetectAttacking();
    }

    public void DetectAttacking()
    {
        if (monster.animator.GetFloat("Attacking") > 0)
        {
            Attack(attackDmg);
           
        }
        else
        {
            collidersDamaged.Clear();
        }
    }

    public void Attack(float damage)
    {
        Collider[] collidersToDamage = new Collider[10];

        // Perform the OverlapBox check
        int colliderCount = Physics.OverlapBoxNonAlloc(
            attackCollider.bounds.center,          
            attackCollider.bounds.extents,          
            collidersToDamage,                      
            Quaternion.identity,                    
            monster.whatIsPlayer,                   
            QueryTriggerInteraction.Collide         
        );
        Debug.Log(colliderCount);

        for (int i = 0; i < colliderCount; i++)
        {
            Debug.Log("Attacking");
            if (!collidersDamaged.Contains(collidersToDamage[i]) && collidersToDamage[i].gameObject.CompareTag("Player"))
            {
                PlayerStatsManager.Instance.DecreaseHealth(damage);
                collidersDamaged.Add(collidersToDamage[i]);
                
            }
        }

    }
}
