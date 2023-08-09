using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackInputProperties : IInputProperties
{
    public bool IsActive { get ; set; }
    public float InputStartTime { get; set ; }
    [field: SerializeField] public float InputExpiredTime { get; set; }
    [field: SerializeField] public InputEnum InputType { get; set; }
}
