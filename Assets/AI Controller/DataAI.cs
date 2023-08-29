using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AI Data", menuName = "AI ScriptableObject/AI Data")]
public class DataAI : ScriptableObject
{
    public float idleStateDuration;
    public float movingStateDuration;
}
