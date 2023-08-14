using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxHandler : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private IAttack currentAttack;
    private void Start()
    {
        AttackHandler.OnGetAttack += GetCurrentAttack;
    }

    private void GetCurrentAttack(IAttack currentAttack)
    {
        this.currentAttack = currentAttack;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == null)
        {
            return;
        }

        IHurtBox damagableObject = collision.GetComponent<IHurtBox>();
        if (damagableObject == null)
        {
            return;
        }

        damagableObject.Damage(currentAttack.AttackDamage);
        damagableObject.KnockBack(currentAttack.AttackKnockDuration, currentAttack.AttackKnockDistance, currentAttack.AttackKnockForce);
    }
}
