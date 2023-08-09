using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackCombination
{
    [SerializeField] private string attackName;
    [field: SerializeField] public InputEnum InputModifier01 { get; set; }
    [field: SerializeField] public InputEnum InputModifier02 { get; set; }
    [field: SerializeField] public InputEnum InputAttack { get; set; }
    [field: SerializeField] public AttackEnum Attack { get; set; }
}
