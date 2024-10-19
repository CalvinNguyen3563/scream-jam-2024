using UnityEngine;

public abstract class MonsterBaseState
{ 
    public abstract void EnterState(MonsterStateManager stateManager);

    public abstract void UpdateState(MonsterStateManager stateManager);

}
