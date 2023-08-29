using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStateAI : IState
{
    DependencyAI dependency;
    public MovingStateAI(DependencyAI dependency)
    {
        this.dependency = dependency;
    }
    public void Enter()
    {
        Debug.Log("AI Moving State");
    }

    public void Exit()
    {

    }

    public void FixedUpdate()
    {

    }

    public void StateCheckTransition()
    {

    }

    public void Update()
    {

    }
}
