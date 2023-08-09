using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedStateDependency
{
    public FiniteStateMachine FiniteStateMachine { get; private set; }
    public StateManager StateManager { get; private set; }
    public PlayerManager PlayerManager { get; private set; }
    public PlayerData PlayerData { get; private set; }
    public PlayerMovementInput PlayerMovementInput { get; private set; }
    public PlayerAttackInput PlayerAttackInput { get; private set; }

    public SharedStateDependency(FiniteStateMachine finiteStateMachine, StateManager stateManager, PlayerManager playerManager, PlayerData playerData, PlayerMovementInput playerInput, PlayerAttackInput playerAttackInput)
    {
        FiniteStateMachine = finiteStateMachine;
        StateManager = stateManager;
        PlayerManager = playerManager;
        PlayerData = playerData;
        PlayerMovementInput = playerInput;
        PlayerAttackInput = playerAttackInput;
    }
}
