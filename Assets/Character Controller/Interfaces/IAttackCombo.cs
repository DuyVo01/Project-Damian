using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackCombo : IAttack
{
    public AttackData[] NextAttacks { get; set; }
}
