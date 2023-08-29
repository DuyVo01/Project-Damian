using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    //Neccessary Components
    [field: SerializeField] public PlayerData playerData { get; private set; }

    private FiniteStateMachine finiteStateMachine;
    private SharedStateDependency shareStateDependency;
    public PlayerMovementInput PlayerMovementInput { get; private set; }
    public PlayerManager PlayerManager { get; private set; }
    public AttackHandler AttackHandler { get; private set; }
    public PlayerAttackInput PlayerAttackInput { get; private set; }

    //States
    public IdleState IdleState { get; private set; }
    public MovingState MovingState { get; private set; }
    public JumpState JumpState { get; private set; }
    public FallingState FallingState { get; private set; }
    public DashState DashState { get; private set; }
    public WallJumpState WallJumpState { get; private set; }
    public WallSlideState WallSlideState { get; private set; }
    public WallBounceState WallBounceState { get; private set; }
    public AttackComboState AttackComboState { get; private set; }

    private void Awake()
    {
        PlayerMovementInput = GetComponent<PlayerMovementInput>();
        PlayerManager = GetComponent<PlayerManager>();
        AttackHandler = GetComponent<AttackHandler>();
        PlayerAttackInput = GetComponent<PlayerAttackInput>();
    }
    void Start()
    {
        finiteStateMachine = new FiniteStateMachine();
        shareStateDependency = new SharedStateDependency(finiteStateMachine, this, PlayerManager, playerData, PlayerMovementInput, PlayerAttackInput);

        IdleState = new IdleState(shareStateDependency);
        MovingState = new MovingState(shareStateDependency);
        JumpState = new JumpState(shareStateDependency);
        FallingState = new FallingState(shareStateDependency);
        DashState = new DashState(shareStateDependency);
        WallJumpState = new WallJumpState(shareStateDependency);
        WallSlideState = new WallSlideState(shareStateDependency);
        WallBounceState = new WallBounceState(shareStateDependency);

        AttackComboState = new AttackComboState(shareStateDependency, AttackHandler);

        finiteStateMachine.InitializeState(IdleState);
    }

    // Update is called once per frame
    void Update()
    {
        finiteStateMachine.Update();
    }

    private void FixedUpdate()
    {
        finiteStateMachine.FixedUpdate();
    }
}
