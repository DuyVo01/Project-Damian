using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    public string AttackName { get; set; }
    public AttackEnum AttackInput { get; set; }
    public AttackType AttackType { get; set; }
    public bool IsAttackUnlocked { get; set; }
    public float AttackDamage { get; set; }
}
