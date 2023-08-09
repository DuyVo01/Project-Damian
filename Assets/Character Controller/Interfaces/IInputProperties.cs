using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputProperties
{
    public bool IsActive { get; set; }
    public float InputStartTime { get; set; }
    public float InputExpiredTime { get; set; }
    public InputEnum InputType { get; set; }
}
