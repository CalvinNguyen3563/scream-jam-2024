using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeathState : MonsterBaseState
{
    // nothing here, dude is fuckin dead
    public override void EnterState(MonsterStateManager stateManager)
    {
        stateManager.animator.SetLayerWeight(1, 1);
        stateManager.animator.Play("Mutant Death", 1);


    }

    public override void UpdateState(MonsterStateManager stateManager)
    {
        stateManager.agent.speed = 0f;
        stateManager.agent.acceleration = 40f;
    }
}
