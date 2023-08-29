using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerInfor : MonoBehaviour
{
    public bool IsAttacking { get; set; }
    public bool IsGrounded { get; set; }
    public Queue<IAttack> currentAttackToExecute { get; private set; } = new Queue<IAttack>();
    public static PlayerInfor Instance { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Debug.Log(Instance != null);
        IsAttacking = false;
    }

    public void AddAttackToExecuteQueue(IAttack attackToAdd)
    {
        if(currentAttackToExecute.Count == 2)
        {
            return;
        }

        if(attackToAdd == null)
        {
            return;
        }

        currentAttackToExecute.Enqueue(attackToAdd);
    }

    public IAttack GetAttackFromQueue()
    {
        if(currentAttackToExecute.Count == 0)
        {
            return null;
        }

        return currentAttackToExecute.Dequeue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
