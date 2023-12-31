using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertedState : StateBaseClass
{
    private AIAnimationController animController;
    private Animator anim;
    private float alertedTime = 0.5f;  // How long the state will last

    private void Start()
    {
        animController = transform.GetComponentInChildren<AIAnimationController>();
        anim = transform.GetComponentInChildren<Animator>();
        character = GetComponent<AICharacter>();
        character.isMoving = false;
        character.isAttacking = true;
        character.knowsAboutPlayer = true;
        StartCoroutine(Alerted());
    }

    public override void UpdateLogic()
    {

    }

    private IEnumerator Alerted()
    {

        yield return new WaitForSeconds(alertedTime);

        if (character.characterType == AICharacter.CharacterTypes.Boss)
            GetComponent<BossStateMachineController>().ChangePhase();

        character.isAttacking = false;
        character.ChangeState(AICharacter.States.None);  // Return to normal states
    }
}
