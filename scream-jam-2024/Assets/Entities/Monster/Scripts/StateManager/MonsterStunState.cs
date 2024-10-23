using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterStunState : MonsterBaseState
{

    float stunTime = 3f;
    bool stunSet = false;
    public override void EnterState(MonsterStateManager stateManager)
    {
        stateManager.StartCoroutine(stateManager.stun(stunTime));
        SoundManager.instance.PlaySoundFXClip(stateManager.stunClip, stateManager.orientation.transform.position, 1f);
        stunSet = true;
    }


    public override void UpdateState(MonsterStateManager stateManager)
    {
        stateManager.agent.speed = 0;
        stateManager.agent.velocity = Vector3.zero;

        if (!stateManager.animator.GetBool("stunned") && stunSet)
        {
            stunSet = false;
            stateManager.SwitchState(stateManager.patrolState);
        }
    }
}
