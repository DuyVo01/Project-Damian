using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHurtBox: IDamagable
{
    public void KnockBack(Vector2 knockDirection, Vector2 knockForce);
}
