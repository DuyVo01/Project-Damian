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
    [SerializeField] private SkillData[] skills;

    public delegate void AttackDataEventHandler(IAttack attackProperties);
    public static event AttackDataEventHandler OnGetAttack;

    private AttackData currentAttack;
    private AttackData[] nextAttacks;
    private int currentAttackIndex;

    private SkillData currentSkill;


    private void Start()
    {
        currentAttackIndex = 0;

        currentAttack = null;
        currentSkill = null;
    }

    public IAttack GetAttack(AttackEnum attackInput)
    {
        if (attackInput == AttackEnum.lightComboAttack || attackInput == AttackEnum.heavyComboAttack)
        {
            return GetNormalAttack(attackInput);
        }
        else
        {
            return GetSKillAttack(attackInput);
        }
    }

    public AttackData GetNormalAttack(AttackEnum attackInput)
    {
        currentAttack = null;
        if (currentAttackIndex == 0)
        {
            if (lightAttack.AttackInput == attackInput)
            {
                currentAttack = lightAttack;
            }
            else if (heavyAttack.AttackInput == attackInput)
            {
                currentAttack = heavyAttack;
            }
        }
        else
        {
            foreach (AttackData attack in nextAttacks)
            {
                if (attack.AttackInput == attackInput)
                {
                    currentAttack = attack;
                }
            }
        }

        if (currentAttack != null)
        {
            OnGetAttack?.Invoke(currentAttack);
            nextAttacks = currentAttack.NextAttacks;
        }

        currentAttackIndex++;

        return currentAttack;
    }

    private SkillData GetSKillAttack(AttackEnum attackInput)
    {
        foreach (SkillData skill in skills)
        {
            if (skill.AttackInput == attackInput)
            {
                currentSkill = skill;
                break;
            }
        }

        if (currentSkill != null)
        {
            OnGetAttack?.Invoke(currentSkill);
        }
        return currentSkill;
    }

    public void SKillContinue()
    {
        currentSkill = currentSkill.NextSkillChain[0];
        OnGetAttack?.Invoke(currentSkill);
    }

    public void ResetAttackData()
    {
        currentAttackIndex = 0;
        currentAttack = null;
    }

    public void ResetSkillData()
    {
        currentSkill = null;
    }
}