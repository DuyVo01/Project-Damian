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
    [SerializeField] private float attackDamage;
    [SerializeField] private SkillData[] nextSkillChain;

    public string AttackName { get => attackName; set => attackName = value; }
    public AttackEnum AttackInput { get => attackInput; set => attackInput = value; }
    public AttackType AttackType { get => attackType; set => attackType = value; }
    public bool IsAttackUnlocked { get => isAttackUnlocked; set => isAttackUnlocked = value; }
    public float AttackDamage { get => attackDamage; set => attackDamage = value; }
    public SkillData[] NextSkillChain { get => nextSkillChain; set => nextSkillChain = value; }
}
