using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    public string AttackName { get; set; }
    public Attack AttackInput { get; set; }
    public AttackType AttackType { get; set; }
    public bool IsAttackUnlocked { get; set; }
    public AttackData[] NextAttacks { get; set; }

    //Attack Damage
    public float AttackDamage { get; set; }

    //Moving Force
    public Vector2 AttackMoveForce { get; set; }
    public float AttackMoveDistance { get; set; }
    public float AttackMoveDuration { get; set; }

    //Knock Enemy Force
    public Vector2 AttackKnockForce { get; set; }
    public float AttackKnockDistance { get; set; }
    public float AttackKnockDuration { get; set; }
}
