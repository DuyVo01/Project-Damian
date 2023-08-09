using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObject : MonoBehaviour, IHurtBox
{
    public void Damage(float damageAmount)
    {
        Debug.Log(damageAmount);
    }
    public void KnockBack(Vector2 knockDirection, Vector2 knockForce)
    {

    }
}
