using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateAI : IState
{
    DependencyAI dependencyAI;

    private float enterStateTime;
    public IdleStateAI(DependencyAI dependencyAI)
    {
        this.dependencyAI = dependencyAI;
    }
    public void Enter()
    {
        Debug.Log("AI idle state");
        enterStateTime = 0;
    }

    public void Exit()
    {
        
    }

    public void FixedUpdate()
    {
        
    }

    public void StateCheckTransition()
    {
        enterStateTime += Time.deltaTime;
        if(enterStateTime > dependencyAI.ManagerAI.dataAI.idleStateDuration)
        {
            dependencyAI.FiniteStateMachineAI.ChangeState(dependencyAI.StateManagerAI.MovingStateAI);
        }
    }

    public void Update()
    {
        StateCheckTransition();
    }
}
