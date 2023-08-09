using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackProperties
{
    public string attackName;// name of the attack
    public AttackEnum attackInput; // the input from player, indicate the type of attack, it can be either light or heavy attacks
    public AttackType attackType;
    public bool isAttackUnlocked; // flag for future implementation, used for unlock attacks or combos

    [Space(10)]
    public float attackDamage;

    public AttackProperties[] nextAttack;
}