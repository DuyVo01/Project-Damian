using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class StateObserver : MonoBehaviour
{
    private Rigidbody2D playerRB;
    [SerializeField] private Animator animController;
    private PlayerMovementInput playerInput;
    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();

        playerInput = GetComponent<PlayerMovementInput>();
    }
    // Start is called before the first frame update
    void Start()
    {
        IdleState.OnAnimationEvent += IdleAnimationHandler;
        MovingState.OnAnimationEvent += MovingAnimationHandler;
        JumpState.OnAnimationEvent += JumpAnimationHandler;
        FallingState.OnAnimationEvent += FallAnimationHandler;
        WallSlideState.OnAnimationEvent += SlideAnimationHandler;

        AttackState.OnAttackStateEvent += AttackAnimationHandler;
        SkillState.OnSkillStateEvent += AttackAnimationHandler;
    }

    private void IdleAnimationHandler(bool isActiveAnimation)
    {
        //animController.SetBool("IdleAnim", isActiveAnimation);
        animController.CrossFade("HeroKnight_Idle", 0, 0);
    }

    private void MovingAnimationHandler(bool isActiveAnimation)
    {
        //animController.SetBool("MovingAnim", isActiveAnimation);
        animController.CrossFade("HeroKnight_Run", 0, 0);
    }
    private void JumpAnimationHandler(bool isActiveAnimation)
    {
        //animController.SetBool("MovingAnim", isActiveAnimation);
        animController.CrossFade("HeroKnight_Jump", 0, 0);
    }

    private void FallAnimationHandler(bool isActiveAnimation)
    {
        //animController.SetBool("MovingAnim", isActiveAnimation);
        animController.CrossFade("HeroKnight_Fall", 0, 0);
    }
    private void SlideAnimationHandler(bool isActiveAnimation)
    {
        //animController.SetBool("MovingAnim", isActiveAnimation);
        animController.CrossFade("HeroKnight_WallSlide", 0, 0);
    }

    private void AttackAnimationHandler(string attackName)
    {
        if(attackName == null)
        {
            return;
        }

        int hasAttackName = Animator.StringToHash(attackName);
        animController.Play(hasAttackName, -1, 0);
    }

    private void Update()
    {
        animController.SetFloat("MovingBlend", playerInput.MovementVector.x);
    }
}