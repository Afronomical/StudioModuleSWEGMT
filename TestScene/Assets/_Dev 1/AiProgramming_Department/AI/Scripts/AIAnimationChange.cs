using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AIAnimationChange : MonoBehaviour
{
    private Transform characterTransform;
    private Animator anim;
    private AIAnimationController animController;
    private AICharacter characterScript;
    public Transform faceTowards;

    //Slash Effect Anim
    //public Animator slashEffect;

    private bool characterAttacking = false;

    

    void Start()
    {
        characterTransform = transform.parent;
        anim = GetComponent<Animator>();
        characterScript = characterTransform.GetComponent<AICharacter>();
        animController = GetComponent<AIAnimationController>();
    }

    void LateUpdate()
    {
        Vector3 dir = faceTowards.position - transform.position;
        anim.SetFloat("MovementX", dir.x);
        anim.SetFloat("MovementY", dir.y);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -characterTransform.rotation.z));

        //Debug.Log(slashEffect.parameters.GetValue(0));

        if (!animController.IsAnimationPlaying(anim, AIAnimationController.AnimationStates.Hurt) &&
            !animController.IsAnimationPlaying(anim, AIAnimationController.AnimationStates.SwordAttack))  // If he isn't playing the hurt animation
        {
            switch (characterScript.currentState)
            {
                case AICharacter.States.Idle:
                    if (characterScript.isMoving)
                        animController.ChangeAnimationState(AIAnimationController.AnimationStates.Walk);
                    else
                        animController.ChangeAnimationState(AIAnimationController.AnimationStates.Idle);
                    break;
                case AICharacter.States.Run:
                    if (characterScript.isMoving)
                        animController.ChangeAnimationState(AIAnimationController.AnimationStates.Walk);
                    else
                        animController.ChangeAnimationState(AIAnimationController.AnimationStates.Idle);
                    break;
                case AICharacter.States.Patrol:
                    if (characterScript.isMoving)
                        animController.ChangeAnimationState(AIAnimationController.AnimationStates.Walk);
                    else
                        animController.ChangeAnimationState(AIAnimationController.AnimationStates.Idle);
                    break;
                case AICharacter.States.Hunt:
                    animController.ChangeAnimationState(AIAnimationController.AnimationStates.Walk);
                    break;
                case AICharacter.States.Downed:
                    animController.ChangeAnimationState(AIAnimationController.AnimationStates.Downed);
                    break;
                case AICharacter.States.Attack:
                    if (!characterAttacking)
                    {
                        animController.ChangeAnimationState(AIAnimationController.AnimationStates.SwordAttack);
                        //slashEffect.SetTrigger("SlashEffect");
                        
                        characterAttacking = true;
                    }
                    else
                    {
                        if (!animController.IsAnimationPlaying(anim, AIAnimationController.AnimationStates.SwordAttack))
                        {
                            characterAttacking = false;
                        }
                    }
                    break;
                case AICharacter.States.Shoot:
                    animController.ChangeAnimationState(AIAnimationController.AnimationStates.BowAttack);
                    break;
                case AICharacter.States.Dead:
                    animController.ChangeAnimationState(AIAnimationController.AnimationStates.Death);
                    break;
                case AICharacter.States.SpecialAttack:
                    animController.ChangeAnimationState(AIAnimationController.AnimationStates.BowAttack);
                    break;
                case AICharacter.States.Reload:
                    animController.ChangeAnimationState(AIAnimationController.AnimationStates.Reload);
                    break;
                case AICharacter.States.None:
                    animController.ChangeAnimationState(AIAnimationController.AnimationStates.Idle);
                    break;
                case AICharacter.States.Alerted:
                    animController.ChangeAnimationState(AIAnimationController.AnimationStates.Alerted);
                    break;


            }
        }
    }
}
