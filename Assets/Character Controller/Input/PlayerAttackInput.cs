using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackInput : MonoBehaviour
{
    [SerializeField] private float attackDelayTime;
    private List<AttackInputProperties> expirableAttackInputList = new List<AttackInputProperties>();

    [field: SerializeField] public AttackInputProperties LightAttack { private set; get; }
    [field: SerializeField] public AttackInputProperties HeavyAttack { private set; get; }
    [field: SerializeField] public AttackInputProperties CombinedAttack { private set; get; }
    [field: SerializeField] public AttackCombination[] CombinationSet { get; private set; }
    public bool AttackButtonActive { get; private set; }
    public AttackInputProperties CurrentAttackInput { get; private set; }

    public static event Action<AttackInputProperties> OnAttackInputPressed;
    

    private void Start()
    {
        expirableAttackInputList.Clear();

        expirableAttackInputList.Add(LightAttack);
        expirableAttackInputList.Add(HeavyAttack);
        expirableAttackInputList.Add(CombinedAttack);
    }

    public void ResetAttackInput()
    {
        AttackButtonActive = false;

        LightAttack.IsActive = false;
        HeavyAttack.IsActive = false;
        CombinedAttack.IsActive = false;
    }

    public void DeactivateAttack(AttackInputProperties attackInput)
    {
        attackInput.IsActive = false;
    }

    private void ExpiringInputs()
    {
        foreach (AttackInputProperties input in expirableAttackInputList)
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

    public void OnLightAttackPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //AttackButtonActive = true;

            LightAttack.IsActive = true;
            //LightAttack.InputStartTime = Time.time;

            StopAllCoroutines();
            StartCoroutine(AttackDelay(attackDelayTime, LightAttack));
        }
    }

    public void OnHeavyAttackPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //AttackButtonActive = true;

            HeavyAttack.IsActive = true;
            //HeavyAttack.InputStartTime = Time.time;

            StopAllCoroutines();
            StartCoroutine(AttackDelay(attackDelayTime, HeavyAttack));
        }
    }

    private IEnumerator AttackDelay(float attackDelayTime, AttackInputProperties attack)
    {
        if ((attack == HeavyAttack && LightAttack.IsActive) || (attack == LightAttack && HeavyAttack.IsActive))
        {
            attack = CombinedAttack;
            attack.IsActive = true;
        }
        yield return new WaitForSecondsRealtime(attackDelayTime);

        AttackButtonActive = true;

        CurrentAttackInput = attack;

        attack.InputStartTime = Time.time;

        OnAttackInputPressed?.Invoke(attack);
    }

    private void Update()
    {
        ExpiringInputs();
    }
}
