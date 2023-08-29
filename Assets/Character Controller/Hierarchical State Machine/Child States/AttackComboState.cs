using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AttackComboState : IState
{ 
    private bool isAttackEnd;
    private bool isAttackStart;
    private float attackMoveTime;
    private IAttack currentAttack;

    protected SharedStateDependency dependency;
    protected AttackHandler attackHandler;

    public delegate void StateEventHandler(string attackName);
    public static event StateEventHandler OnAttackStateEvent;

    protected Queue<IAttack> executeLists = new Queue<IAttack>();

    public AttackComboState(SharedStateDependency dependency, AttackHandler attackHandler)
    {
        this.dependency = dependency;
        this.attackHandler = attackHandler;
    }

    public void Enter()
    {
        AttackAnimationEvents.OnAttackEnd += AttackAnimationEnd;
        StopMoving();

        executeLists.Enqueue(PlayerInfor.Instance.currentAttackToExecute.Dequeue());
        currentAttack = executeLists.Dequeue();

        if (currentAttack != null)
        {
            Debug.Log(currentAttack.AttackName);
            OnAttackStateEvent(currentAttack.AttackName);
            isAttackEnd = false;
            isAttackStart = true;
        }
    }

    public void Exit()
    {
        attackHandler.ResetAttackData();

        AttackAnimationEvents.OnAttackEnd -= AttackAnimationEnd;
    }

    public void FixedUpdate()
    {
        if(currentAttack == null)
        {
            return;
        }

        if (isAttackStart)
        {
            AttackMoving();
            isAttackStart = false;
            attackMoveTime = 0;
        }

        attackMoveTime += Time.deltaTime;
        if(attackMoveTime > currentAttack.AttackMoveDuration)
        {
            StopMoving();
        }
    }

    public void StateCheckTransition()
    {
        if (isAttackEnd && executeLists.Count == 0)
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.IdleState);
        }
    }

    public void Update()
    {
        ExecuteAttack();
        StateCheckTransition();
    }

    private void ExecuteAttack()
    {
        if (isAttackEnd && executeLists.Count > 0)
        {
            currentAttack = executeLists.Dequeue();
            if (currentAttack != null)
            {
                Debug.Log(currentAttack.AttackName);
                OnAttackStateEvent(currentAttack.AttackName);
                isAttackEnd = false;
                isAttackStart = true;
            }
        }
    }

    protected void StopMoving()
    {
        dependency.PlayerManager.AddForceToPlayer(-dependency.PlayerManager.GetPlayerSpeed().x * Vector2.right, 1);
    }

    protected void AttackMoving()
    {
        if (currentAttack == null)
        {
            return;
        }

        Vector2 movingForce = currentAttack.AttackMoveForce;

        if (!dependency.PlayerData.isFacingRight)
        {
            movingForce.x = -movingForce.x;
        }

        dependency.PlayerManager.AddForceToPlayer(movingForce, 1);
    }

    protected void AttackAnimationEnd()
    {
        isAttackEnd = true;
        executeLists.Enqueue(PlayerInfor.Instance.GetAttackFromQueue());
    }
}