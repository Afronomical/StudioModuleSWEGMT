using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

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
    public bool characterHasDied = false;

    

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
            !animController.IsAnimationPlaying(anim, AIAnimationController.AnimationStates.SwordAttack) && !characterHasDied)  // If he isn't playing the hurt animation
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
                case AICharacter.States.DashAttack:
                case AICharacter.States.SpinAttackBox:
                    if (!characterAttacking)
                    {
                        animController.ChangeAnimationState(AIAnimationController.AnimationStates.SwordAttack);
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
                case AICharacter.States.SpecialAttack:
                case AICharacter.States.SprayShoot1:
                case AICharacter.States.SprayShoot2:
                case AICharacter.States.CircularShoot:
                case AICharacter.States.HomingArrow:
                case AICharacter.States.SprayArrows:
                    animController.ChangeAnimationState(AIAnimationController.AnimationStates.BowAttack);
                    break;
                case AICharacter.States.Dead:
                    if (!characterHasDied)
                    {
                        animController.ChangeAnimationState(AIAnimationController.AnimationStates.Death);
                        AudioManager.Manager.PlaySFX("NPC_Death");
                        characterHasDied = true;
                    }
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
