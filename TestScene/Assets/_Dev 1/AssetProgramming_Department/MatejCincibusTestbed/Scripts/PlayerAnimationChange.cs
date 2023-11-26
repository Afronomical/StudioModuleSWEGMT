using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationChange : MonoBehaviour
{
    private Animator animator;
    private PlayerAnimationController animationController;

    // References to other Scripts found on Player:
    private PlayerController playerController;
    private PlayerDeath playerDeath;
    private Feeding feeding;
    private playerAttack PlayerAttack;

    // Player Variables:
    Vector2 movementInput;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animationController = GetComponent<PlayerAnimationController>();

        playerController = GetComponent<PlayerController>();
        playerDeath = GetComponent<PlayerDeath>();
        feeding = GetComponent<Feeding>();
        PlayerAttack = GetComponent<playerAttack>();
    }

    private void Update()
    {
        GetInputAxis();
    }

    private void LateUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            animator.SetFloat("MovementX", movementInput.x);
            animator.SetFloat("MovementY", movementInput.y);
        }

        if (!animationController.IsAnimationPlaying(animator, PlayerAnimationController.AnimationStates.SlashAttack) &&
           !animationController.IsAnimationPlaying(animator, PlayerAnimationController.AnimationStates.Dash) &&
           !animationController.IsAnimationPlaying(animator, PlayerAnimationController.AnimationStates.Hurt) &&
           !animationController.IsAnimationPlaying(animator, PlayerAnimationController.AnimationStates.Death) &&
           !animationController.IsAnimationPlaying(animator, PlayerAnimationController.AnimationStates.Feed) && !playerDeath.GetIsDead())
        { 
            if (movementInput != Vector2.zero)
            {
                animationController.ChangeAnimationState(PlayerAnimationController.AnimationStates.Walk);
            }
            else
            {
                animationController.ChangeAnimationState(PlayerAnimationController.AnimationStates.Idle);
            }
        }

    }

    private void GetInputAxis()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
    }
}
