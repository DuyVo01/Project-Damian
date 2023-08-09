using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    protected FiniteStateMachine finiteStateMachine;
    protected StateManager stateManager;
    protected PlayerMovementInput playerInput;
    public State(FiniteStateMachine finiteStateMachine, StateManager stateManager, PlayerMovementInput playerInput)
    {
        this.finiteStateMachine = finiteStateMachine;
        this.stateManager = stateManager; 
        this.playerInput = playerInput;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void EventHanlder(string eventName) { }
    public virtual void StateCheckTransition() { }
}
