using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "SOAssetsMenu/Attack Data")]
public class AttackData : ScriptableObject, IAttackCombo
{
    [SerializeField] private string attackName;
    [SerializeField] private AttackEnum attackInput;
    [SerializeField] private AttackType attackType;
    [SerializeField] private bool isAttackUnlocked;
    [SerializeField] private float attackDamage;
    [SerializeField] private AttackData[] nextAttacks;

    public string AttackName { get => attackName; set => attackName = value; }
    public AttackEnum AttackInput { get => attackInput; set => attackInput = value; }
    public AttackType AttackType { get => attackType; set => attackType = value; }
    public bool IsAttackUnlocked { get => isAttackUnlocked; set => isAttackUnlocked = value; }
    public float AttackDamage { get => attackDamage; set => attackDamage = value; }
    public AttackData[] NextAttacks { get => nextAttacks; set => nextAttacks = value; }
}
