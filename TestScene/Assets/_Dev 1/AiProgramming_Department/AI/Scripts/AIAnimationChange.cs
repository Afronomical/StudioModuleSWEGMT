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
                    animController.ChangeAnimationState(AIAnimationController.AnimationStates.SwordAttack);
                    break;
                case AICharacter.States.Shoot:
                    animController.ChangeAnimationState(AIAnimationController.AnimationStates.BowAttack);
                    break;
                case AICharacter.States.Dead:
                    animController.ChangeAnimationState(AIAnimationController.AnimationStates.Death);
                    break;
            }
        }
    }
}
