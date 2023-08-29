using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InputProperties: IInputProperties
{ 
    public bool IsActive { get; set; }
    public float InputStartTime { get; set; }
    [field: SerializeField] public float InputExpiredTime { get; set; }
    [field: SerializeField] public InputType InputType { get; set; }
}
