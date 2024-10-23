using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterPatrolState : MonsterBaseState  
{
    public MonsterStateManager stateManager;
    public float runningThreshold = 45;
    public override void EnterState(MonsterStateManager stateManager)
    {
        this.stateManager = stateManager;
        Debug.Log("patrol");
        stateManager.agent.acceleration = stateManager.walkingAcceleration;
        stateManager.agent.speed = stateManager.walkingSpeed;
        stateManager.agent.angularSpeed = 200f;
    }

    public override void UpdateState(MonsterStateManager stateManager)
    {

        if (stateManager.agent.remainingDistance <= stateManager.agent.stoppingDistance) //done with path
        {
            stateManager.pointFound = false;
            Vector3 point;
            if (RandomPoint(WorldGameObjectStorage.Instance.player.transform.position, stateManager.patrolRange, out point)) //pass in our centre point and radius of area
            {
                stateManager.pointFound = true;
                stateManager.point = point;
                stateManager.agent.SetDestination(point);
            }
        }

        if (Vector3.Distance(stateManager.transform.position, WorldGameObjectStorage.Instance.player.transform.position) > runningThreshold)
        {
            stateManager.agent.speed = stateManager.runningSpeed;
        }
        else
        {
            stateManager.agent.speed = stateManager.walkingSpeed;
        }

        DetectPlayer();
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * stateManager.patrolRange;
        Vector3 midpoint = Vector3.Lerp(stateManager.transform.position, randomPoint, 0.75f);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(midpoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void DetectPlayer()
    {
        stateManager.playerInRadius = Physics.CheckSphere(stateManager.transform.position, stateManager.playerDetectRadius, stateManager.whatIsPlayer, QueryTriggerInteraction.Ignore);
        Vector3 direction = WorldGameObjectStorage.Instance.player.transform.position - stateManager.transform.position;
        float angle = Vector3.Angle(stateManager.orientation.transform.forward, direction);

        if (stateManager.playerInRadius && angle < 75)
        {
            if (!Physics.Raycast(stateManager.transform.position, WorldGameObjectStorage.Instance.player.transform.position - stateManager.transform.position, out RaycastHit hit, stateManager.patrolRange, stateManager.whatIsObstacle))
            {
                stateManager.SwitchState(stateManager.chaseState);
            }
        }
    }

    

}
