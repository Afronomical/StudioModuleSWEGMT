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
    }


    public override void UpdateLogic()
    {
        //Disables the specific character when health is 0
        if (animController != null && anim != null)
        {
            character.gameObject.GetComponent<CircleCollider2D>().enabled = false; //Player can't push AI whilst they're death animation is playing
            Invoke("DisableAfterDeath", 1);
        }
    }

    private void DisableAfterDeath()
    {
        if (!animController.IsAnimationPlaying(anim, AIAnimationController.AnimationStates.Death))
        {
            if (!AISpawnManager.instance.deadEnemies.Contains(gameObject) && character.characterType != AICharacter.CharacterTypes.Boss)
                AISpawnManager.instance.deadEnemies.Add(gameObject);
            character.gameObject.SetActive(false);
        }
    }
}
