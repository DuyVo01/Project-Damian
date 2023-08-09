using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputCombineProperties
{
    public bool isInCombine { get; set; }
    public float InputInCombineStartTime { get; set; }
    public float InputInCombineExpiredTime { get; set; }
}
