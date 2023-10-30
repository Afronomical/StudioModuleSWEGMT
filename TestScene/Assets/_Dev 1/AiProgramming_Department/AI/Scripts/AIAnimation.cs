using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AIAnimation : MonoBehaviour
{
    private Transform characterTransform;
    private Animator anim;
    private AI_AnimationController animController;
    private AICharacter characterScript;
    public Transform faceTowards;

    void Start()
    {
        characterTransform = transform.parent;
        anim = GetComponent<Animator>();
        characterScript = characterTransform.GetComponent<AICharacter>();
        animController = GetComponent<AI_AnimationController>();
    }

    void LateUpdate()
    {
        Vector3 dir = faceTowards.position - transform.position;
        anim.SetFloat("MovementX", dir.x);
        anim.SetFloat("MovementY", dir.y);
   
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -characterTransform.rotation.z));



        if (!animController.IsAnimationPlaying(anim, AI_AnimationController.AnimationStates.Hurt))  // If he isn't playing the hurt animation
        {
            switch (characterScript.currentState)
            {
                case AICharacter.States.Idle:
                    animController.ChangeAnimationState(AI_AnimationController.AnimationStates.Walk);
                    //if (characterTransform.GetComponent<IdleState>().moving)
                    break;
                case AICharacter.States.Run:
                    animController.ChangeAnimationState(AI_AnimationController.AnimationStates.Walk);
                    break;
                case AICharacter.States.Patrol:
                    animController.ChangeAnimationState(AI_AnimationController.AnimationStates.Walk);
                    break;
                case AICharacter.States.Hunt:
                    animController.ChangeAnimationState(AI_AnimationController.AnimationStates.Walk);
                    break;
                case AICharacter.States.Downed:
                    animController.ChangeAnimationState(AI_AnimationController.AnimationStates.Downed);
                    break;
                case AICharacter.States.Attack:
                    animController.ChangeAnimationState(AI_AnimationController.AnimationStates.SwordAttack);
                    break;
                case AICharacter.States.Shoot:
                    animController.ChangeAnimationState(AI_AnimationController.AnimationStates.BowAttack);
                    break;
                case AICharacter.States.Dead:
                    animController.ChangeAnimationState(AI_AnimationController.AnimationStates.Death);
                    break;
            }
        }
    }
}
