using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "SOAssetsMenu/Skill Data")]
public class SkillData : ScriptableObject, ISkillAttack
{
    [SerializeField] private string attackName;
    [SerializeField] private AttackEnum attackInput;
    [SerializeField] private AttackType attackType;
    [SerializeField] private bool isAttackUnlocked;
    [SerializeField] private SkillData[] nextSkillChain;
    [Space(10)]
    [SerializeField] private float attackDamage;
    [Space(10)]
    [SerializeField] private Vector2 attackMoveForce;
    [SerializeField] private float attackMoveDistance;
    [SerializeField] private float attackMoveDuration;
    [Space(10)]
    [SerializeField] private Vector2 attackKnockForce;
    [SerializeField] private float attackKnockDistance;
    [SerializeField] private float attackKnockDuration;

    public string AttackName { get => attackName; set => attackName = value; }
    public AttackEnum AttackInput { get => attackInput; set => attackInput = value; }
    public AttackType AttackType { get => attackType; set => attackType = value; }
    public bool IsAttackUnlocked { get => isAttackUnlocked; set => isAttackUnlocked = value; }
    public SkillData[] NextSkillChain { get => nextSkillChain; set => nextSkillChain = value; }
    //Damage
    public float AttackDamage { get => attackDamage; set => attackDamage = value; }
    //Moving Force
    public Vector2 AttackMoveForce { get => attackMoveForce; set => attackMoveForce = value; }
    public float AttackMoveDistance { get => attackMoveDistance; set => attackMoveDistance = value; }
    public float AttackMoveDuration { get => attackMoveDuration; set => attackMoveDuration = value; }
    //knock Force
    public Vector2 AttackKnockForce { get => attackKnockForce; set => attackKnockForce = value; }
    public float AttackKnockDistance { get => attackKnockDistance; set => attackKnockDistance = value; }
    public float AttackKnockDuration { get => attackKnockDuration; set => attackKnockDuration = value; }

    private void OnValidate()
    {
        attackMoveForce.x = attackMoveDistance / attackMoveDuration;
        attackKnockForce.x = attackKnockDistance / attackKnockDuration;
    }
}
