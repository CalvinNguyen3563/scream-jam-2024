using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class MonsterChaseState : MonsterBaseState
{
    public float acceleration = 8f;
    public float speed = 5f;

    float elapsedTime = 0f;
    float attackCooldown = 2f;
    bool canAttack = true;
    float minDistanceToPlayer = 5f;
    public override void EnterState(MonsterStateManager stateManager)
    {
        stateManager.agent.acceleration = stateManager.runningAcceleration;
        stateManager.agent.speed = stateManager.runningSpeed;
    }

    public override void UpdateState(MonsterStateManager stateManager)
    {
        stateManager.agent.SetDestination(WorldGameObjectStorage.Instance.player.transform.position);

        float distance = Vector3.Distance(stateManager.transform.position, WorldGameObjectStorage.Instance.player.transform.position);

        if (distance <= stateManager.agent.stoppingDistance + 0.5f)
        {
            if (canAttack)
            {
                stateManager.animator.CrossFade("Mutant Swiping", 0.3f);
                canAttack = false;
            }
            FaceTarget(WorldGameObjectStorage.Instance.player.transform.position, stateManager);
        }

        if (elapsedTime > attackCooldown)
        {
            canAttack = true;
            elapsedTime = 0f;
        }

        var clipInfo = stateManager.animator.GetCurrentAnimatorClipInfo(1);

        if (clipInfo.Length > 0)
        {
            string clipName = clipInfo[0].clip.name;

            if (clipName == "Mutant Swiping")
            {
                stateManager.agent.speed = 0f;
                stateManager.agent.acceleration = 40f;
            }
        }
        else
        {
            // Default values if no clip is playing
            stateManager.agent.acceleration = stateManager.runningAcceleration;
            stateManager.agent.speed = stateManager.runningSpeed;
        }


        elapsedTime += Time.deltaTime; 
    }

    private void FaceTarget(Vector3 destination, MonsterStateManager stateManager)
    {
        Vector3 lookPos = destination - stateManager.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        stateManager.transform.rotation = Quaternion.Slerp(stateManager.transform.rotation, rotation, 7f * Time.deltaTime);
    }


}
