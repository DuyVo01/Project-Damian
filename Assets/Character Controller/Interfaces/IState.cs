using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface IState 
{ 
    public void Enter();
    public void Exit();
    public void Update();
    public void FixedUpdate();
    public void StateCheckTransition();
}
