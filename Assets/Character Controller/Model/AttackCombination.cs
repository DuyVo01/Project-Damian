using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackCombination
{
    [SerializeField] private string attackName;
    [field: SerializeField] public InputType InputModifier01 { get; set; }
    [field: SerializeField] public InputType InputModifier02 { get; set; }
    [field: SerializeField] public InputType InputAttack { get; set; }
    [field: SerializeField] public Attack Attack { get; set; }
}
