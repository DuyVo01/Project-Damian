using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackAnimationEvents : MonoBehaviour
{
    public delegate void OnAttackAnimation();
    public static event OnAttackAnimation OnAttackEnd;
    public static event OnAttackAnimation OnSkillStageChange;

    public void OnAttackAnimationEnd()
    {
        OnAttackEnd?.Invoke();
    }

    public void OnSkillAnimationStageChange()
    {
        OnSkillStageChange?.Invoke();
    }
}
