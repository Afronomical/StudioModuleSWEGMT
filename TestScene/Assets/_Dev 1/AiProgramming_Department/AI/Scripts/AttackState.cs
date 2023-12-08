using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Pair programmed: Adam (Driver) and Aaron (Navigator)
public class AttackState : StateBaseClass
{
    public int attackDamage = 10;
    public float attackDelay = 2;
    private float currentDelay;

    //Gameplay Programmers Script for the Player Health
    private ReferenceManager referenceManager;
    private PlayerDeath playerDeath;
    private Animator slashEffect;

    private void Start()
    {
        transform.GetComponentInChildren<AIAnimationController>().ChangeAnimationState(AIAnimationController.AnimationStates.SwordAttack);
        gameObject.transform.GetChild(2).GetChild(0).GetComponent<Animator>().SetTrigger("IsAttacking");  // <-----------------------------------  Error Here
        playerDeath = character.player.GetComponent<PlayerDeath>();
        GetComponent<AICharacter>().isMoving = false;
        GetComponent<AICharacter>().walkingParticles.Stop();
        GetComponent<AICharacter>().runParticles.Stop();
        //transform.GetComponentInChildren<AIAnimationController>().ChangeAnimationState(AIAnimationController.AnimationStates.SwordAttack);
        currentDelay = 0.2f;
        AudioManager.Manager.PlaySFX("NPC_MeleeAttack");
    }

    //public AttackState() 
    //{
    //    //When a character goes to the attack state, this will delay the attack by x amount
    //}
    
    public override void UpdateLogic()
    {
        //Set the reference for the playerDeath variable
        if (playerDeath == null)
        {
            playerDeath = character.player.GetComponent<PlayerDeath>();
        }

        //Counts down the delay
        currentDelay -= Time.deltaTime;

        Vector3 vectorToTarget = Quaternion.Euler(0, 0, 90) * (character.player.transform.position - transform.position);  // Direction towards the target location
        transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);

        //Checks if the delay timer has hit 0, if so, it will damage the player and reset the delay timer to x amount
        if (currentDelay <= 0)
        {
            if (Vector2.Distance(character.player.transform.position, transform.position) <= 1.25f)
                playerDeath.RemoveHealth(attackDamage);
            currentDelay = 2;

            if (character.characterType == AICharacter.CharacterTypes.Boss)
            {
                character.isAttacking = false;
                Destroy(this);
            }
        }
    }
}
