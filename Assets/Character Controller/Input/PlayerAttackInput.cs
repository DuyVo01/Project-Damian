using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackInput : MonoBehaviour
{
    [SerializeField] private float attackDelayTime;
    private Queue<InputProperties> activeInputModifiers = new Queue<InputProperties>();

    [Header("Modifier Input")]
    [SerializeField] private InputProperties jumpModifier;

    [Space(10)]
    [Header("Attack Input")]
    [SerializeField] private InputProperties lightAttack;
    [SerializeField] private InputProperties heavyAttack;
    [SerializeField] private InputProperties combinedAttack;

    [Space(10)]
    [Header("Attack List")]
    [SerializeField] private AttackCombination[] combinationSet;
    public AttackCombination CurrentAttackInput { get; private set; }

    public static event Action<Attack> OnAttackInputPressed;


    private void Start()
    {
        activeInputModifiers.Clear();
    }

    public void DeactivateAttack(InputProperties attackInput)
    {
        attackInput.IsActive = false;
    }

    private void AddModifiers(InputProperties modifier)
    {
        if (activeInputModifiers.Count == 2)
        {
            return;
        }

        activeInputModifiers.Enqueue(modifier);
    }

    private void ExpiringModifiers()
    {
        if (activeInputModifiers.Count == 0)
        {
            return;
        }

        InputProperties modifier = activeInputModifiers.Peek();

        if (Time.time - modifier.InputStartTime > modifier.InputExpiredTime)
        {
            activeInputModifiers.Dequeue();
        }
    }

    public void OnJumpModifier(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpModifier.InputStartTime = Time.time;
            jumpModifier.IsActive = true;
            AddModifiers(jumpModifier);
        }
    }

    public void OnLightAttackPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {

            lightAttack.IsActive = true;

            StopAllCoroutines();
            StartCoroutine(AttackStart(attackDelayTime, lightAttack));
        }
    }

    public void OnHeavyAttackPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            heavyAttack.IsActive = true;

            StopAllCoroutines();
            StartCoroutine(AttackStart(attackDelayTime, heavyAttack));
        }
    }

    private IEnumerator AttackStart(float attackDelayTime, InputProperties attack)
    {
        if ((attack == heavyAttack && lightAttack.IsActive) || (attack == lightAttack && heavyAttack.IsActive))
        {
            heavyAttack.IsActive = false;
            lightAttack.IsActive = false;

            attack = combinedAttack;
            attack.IsActive = true;
        }
        yield return new WaitForSecondsRealtime(attackDelayTime);

        PlayerInfor.Instance.IsAttacking = true;

        CurrentAttackInput = ReadInput(activeInputModifiers, attack);

        attack.InputStartTime = Time.time;

        OnAttackInputPressed?.Invoke(CurrentAttackInput.Attack);
    }

    protected AttackCombination ReadInput(Queue<InputProperties> activeInputModifiers, InputProperties attackInput)
    {
        AttackCombination attack = new AttackCombination();

        attack.InputModifier01 = InputType.noInput;
        attack.InputModifier02 = InputType.noInput;
        attack.Attack = Attack.noAttack;
        attack.InputAttack = attackInput.InputType;

        activeInputModifiers.Reverse();

        if (activeInputModifiers.Count == 1)
        {
            attack.InputModifier02 = activeInputModifiers.Dequeue().InputType;
        }
        if (activeInputModifiers.Count == 2)
        {
            attack.InputModifier01 = activeInputModifiers.Dequeue().InputType;
        }

        foreach (AttackCombination attackCombination in combinationSet)
        {
            if (attack.InputModifier01 != InputType.noInput)
            {
                if (attackCombination.InputModifier01 != attack.InputModifier01)
                {
                    continue;
                }
            }

            if (attack.InputModifier02 != InputType.noInput)
            {
                if (attackCombination.InputModifier02 != attack.InputModifier02)
                {
                    continue;
                }
            }

            if (attackCombination.InputAttack != attack.InputAttack)
            {
                continue;
            }

            DeactivateAttack(attackInput);
            activeInputModifiers.Clear();
            attack = attackCombination;
            break;
        }

        Debug.Log(attack.InputAttack);
        Debug.Log(attack.InputModifier01);
        Debug.Log(attack.InputModifier02);
        Debug.Log(attack.Attack);
        return attack;
    }

    private void Update()
    {
        ExpiringModifiers();
    }
}
