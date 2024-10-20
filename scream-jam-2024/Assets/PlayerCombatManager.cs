using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    public PlayerManager player;
    public GameObject cam;
    public float attackRange = 5f;
    public float damage = 15f;

    private void Awake()
    {
        
    }

    private void Start()
    {
        player = WorldGameObjectStorage.Instance.player;
        cam = WorldGameObjectStorage.Instance.mainCam;
    }
    public void Attack()
    {
        if (Physics.Raycast(player.transform.position, cam.transform.forward, attackRange, player.whatIsEnemy, QueryTriggerInteraction.Ignore))
        {
            MonsterStatsManager.Instance.DecreaseHealth(damage);
        }
    }
}
