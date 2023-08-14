using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObject : MonoBehaviour, IHurtBox
{
    public Rigidbody2D objectRb { get; private set; }
    private float knockDuration;
    private void Awake()
    {
        objectRb = GetComponent<Rigidbody2D>();
    }
    public void Damage(float damageAmount)
    {
        Debug.Log(damageAmount);
    }
    public void KnockBack(float knockDuration, float knockDistance, Vector2 knockForce)
    {
        objectRb.velocity = Vector2.zero;
        objectRb.AddForce(knockForce, ForceMode2D.Impulse);
        this.knockDuration = knockDuration;
    }

    private void FixedUpdate()
    {
        knockDuration -= Time.deltaTime;

        if(knockDuration <= 0)
        {
            objectRb.velocity = Vector2.zero;
            knockDuration = 0;
        }
    }
}
