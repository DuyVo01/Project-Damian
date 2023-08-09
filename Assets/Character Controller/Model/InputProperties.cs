using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InputProperties: IInputProperties, IInputCombineProperties
{
    public bool IsActive { get; set; }
    public bool isInCombine { get; set; }
    public float InputStartTime { get; set; }
    public float InputInCombineStartTime { get; set; }
    [field: SerializeField] public float InputExpiredTime { get; set; }
    [field: SerializeField] public float InputInCombineExpiredTime { get; set; }
    [field: SerializeField] public InputEnum InputType { get; set; }
}
