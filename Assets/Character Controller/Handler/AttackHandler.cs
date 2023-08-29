using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] private AttackData lightAttack;
    [SerializeField] private AttackData heavyAttack;
    [SerializeField] private AttackData[] skillAttacks;

    public delegate void AttackDataEventHandler(IAttack attackProperties);
    public static event AttackDataEventHandler OnGetAttack;

    private AttackData currentComboAttack;
    private AttackData[] nextAttacks;
    private int currentComboIndex;

    private AttackData currentSkillAttack;

    
    private void Start()
    {
        currentComboIndex = 0;

        PlayerAttackInput.OnAttackInputPressed += GetAttack;
    }

    public void GetAttack(Attack attackInput)
    {
        if (attackInput == Attack.lightComboAttack || attackInput == Attack.heavyComboAttack)
        {
            PlayerInfor.Instance.AddAttackToExecuteQueue(GetNormalAttack(attackInput));
        }
        else
        {
            PlayerInfor.Instance.AddAttackToExecuteQueue(GetSkillAttack(attackInput));
        }
    }

    public AttackData GetNormalAttack(Attack attackInput)
    {
        currentComboAttack = null;
        if (currentComboIndex == 0)
        {
            if (lightAttack.AttackInput == attackInput)
            {
                currentComboAttack = lightAttack;
            }
            else if (heavyAttack.AttackInput == attackInput)
            {
                currentComboAttack = heavyAttack;
            }
        }
        else
        {
            foreach (AttackData attack in nextAttacks)
            {
                if (attack.AttackInput == attackInput)
                {
                    currentComboAttack = attack;
                }
            }
        }

        if (currentComboAttack != null)
        {
            OnGetAttack?.Invoke(currentComboAttack);
            nextAttacks = currentComboAttack.NextAttacks;
        }

        currentComboIndex++;

        return currentComboAttack;
    }

    private AttackData GetSkillAttack(Attack attackInput)
    {
        currentSkillAttack = null;

        foreach (AttackData skill in skillAttacks)
        {
            if (skill.AttackInput == attackInput)
            {
                currentSkillAttack = skill;
                break;
            }
        }

        if (currentSkillAttack != null)
        {
            OnGetAttack?.Invoke(currentSkillAttack);
        }
        return currentSkillAttack;
    }

    public AttackData SkillNextStage()
    {
        currentSkillAttack = currentSkillAttack.NextAttacks[0];
        OnGetAttack?.Invoke(currentSkillAttack);

        return currentSkillAttack;
    }

    public void ResetAttackData()
    {
        currentComboIndex = 0;
        currentComboAttack = null;
        currentSkillAttack = null;
    }
}