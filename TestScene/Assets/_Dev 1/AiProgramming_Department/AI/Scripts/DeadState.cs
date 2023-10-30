using NUnit.Framework.Internal.Filters;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeadState : StateBaseClass
{
    private AIAnimationController animController;
    private Animator anim;

    private void Start()
    {
        animController = transform.GetComponentInChildren<AIAnimationController>();
        anim = transform.GetComponentInChildren<Animator>();
        animController.ChangeAnimationState(AIAnimationController.AnimationStates.SwordAttack);
    }


    public override void UpdateLogic()
    {
        //Disables the specific character when health is 0
        if (animController != null && anim != null)
        {
            if (!animController.IsAnimationPlaying(anim, AIAnimationController.AnimationStates.Death))
            {
                character.gameObject.SetActive(false);
                Debug.Log("Character Disabled");
            }
        }
    }
}
