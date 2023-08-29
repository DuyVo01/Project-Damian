using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependencyAI
{
    public ManagerAI ManagerAI { get; set; }
    public FiniteStateMachine FiniteStateMachineAI { get; set; }
    public StateManagerAI StateManagerAI { get; set; }

    public DependencyAI(ManagerAI managerAI, FiniteStateMachine finiteStateMachineAI, StateManagerAI stateManagerAI)
    {
        ManagerAI = managerAI;
        FiniteStateMachineAI = finiteStateMachineAI;
        StateManagerAI = stateManagerAI;
    }
}
