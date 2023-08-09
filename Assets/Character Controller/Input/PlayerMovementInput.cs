using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInput : MonoBehaviour
{
    private List<InputProperties> expirableInputList = new List<InputProperties>();
    public List<InputProperties> CombinableInputList { get; private set; } = new List<InputProperties>();

    // Input properties
    [field: SerializeField] public InputProperties Jump { private set; get; }
    [field: SerializeField] public InputProperties JumpHold { private set; get; }
    [field: SerializeField] public InputProperties Dash { private set; get; }

    //Movement relative variables
    public Vector2 MovementVector { private set; get; }

    private void Start()
    {
        expirableInputList.Clear();

        expirableInputList.Add(Jump);
    }

    private void ExpiringInput()
    {
        foreach (InputProperties input in expirableInputList)
        {
            if (!input.IsActive || input.InputExpiredTime == 0)
            {
                continue;
            }

            if (Time.time - input.InputStartTime > input.InputExpiredTime)
            {
                input.IsActive = false;
            }
        }
    }

    private void ExpiringCombination()
    {
        foreach (InputProperties input in CombinableInputList)
        {
            if (!input.isInCombine || input.InputInCombineExpiredTime == 0)
            {
                continue;
            }

            if (Time.time - input.InputInCombineStartTime > input.InputInCombineExpiredTime)
            {
                input.isInCombine = false;
            }
        }
    }

    private void AddInputToCombinationList(InputProperties inputToAdd)
    {
        CombinableInputList.Add(inputToAdd);

        inputToAdd.isInCombine = true;
        inputToAdd.InputInCombineStartTime = Time.time;


        if (CombinableInputList.Count > 2)
        {
            CombinableInputList.RemoveAt(0);
        }
    }

    public void OnMovementKeyPressed(InputAction.CallbackContext context)
    {
        Vector2 movementVector2D = context.ReadValue<Vector2>();
        MovementVector = movementVector2D;
    }

    public void OnJumpKeyPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpHold.IsActive = true;

            Jump.InputStartTime = Time.time;
            Jump.IsActive = true;

            AddInputToCombinationList(Jump);
        }
        if (context.canceled)
        {
            JumpHold.IsActive = false;
        }
    }

    public void OnDashKeyPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Dash.IsActive = true;
        }
        if (context.canceled)
        {
            Dash.IsActive = false;
        }
    }

    private void Update()
    {
        ExpiringInput();
        ExpiringCombination();
    }
}