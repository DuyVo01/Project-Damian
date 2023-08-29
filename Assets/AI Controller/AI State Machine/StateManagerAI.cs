using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManagerAI : MonoBehaviour
{
    private FiniteStateMachine finiteStateMachineAI;
    private ManagerAI managerAI;
    private DependencyAI dependencyAI;

    public IdleStateAI IdleStateAI { get; private set; }
    public MovingStateAI MovingStateAI { get; private set; }
    private void Awake()
    {
        managerAI = GetComponent<ManagerAI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        finiteStateMachineAI = new FiniteStateMachine();
        dependencyAI = new DependencyAI(managerAI, finiteStateMachineAI, this);

        IdleStateAI = new IdleStateAI(dependencyAI);
        MovingStateAI = new MovingStateAI(dependencyAI);

        finiteStateMachineAI.InitializeState(IdleStateAI);
    }

    // Update is called once per frame
    void Update()
    {
        finiteStateMachineAI.Update();
    }

    private void FixedUpdate()
    {
        finiteStateMachineAI.FixedUpdate();
    }
}
