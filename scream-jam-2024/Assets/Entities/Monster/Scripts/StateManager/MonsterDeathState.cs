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

        stateManager.monster.headRig.weight = 0;
        SoundManager.instance.PlaySoundFXClip(stateManager.deathClip, stateManager.orientation.transform.position, 0.5f);
    }

    public override void UpdateState(MonsterStateManager stateManager)
    {
        stateManager.agent.speed = 0f;
        stateManager.agent.acceleration = 40f;
    }
}
