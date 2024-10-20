using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLocomotionManager : MonoBehaviour
{
    public MonsterManager monster;

    private void Awake()
    {
        monster = GetComponent<MonsterManager>();
    }

    
}
