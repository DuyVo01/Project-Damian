using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHurtBox: IDamagable
{
    public void KnockBack(float knockDuration, float knockDistance, Vector2 knockForce);
}
