using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttackState : StateBaseClass
{
    //Dash attack
    public int attackDamage = 10;
    public float dashDelay = 2f;
    private float dashSpeed = 15f;
    private float currentDelay;
    private bool isDashing;
    private Rigidbody2D rb;

    private float dashWindUp = 1.5f;
    private float dashDuration = 1f;
    private TrailRenderer dashTrail;
    Vector3 lastKnownPos;

    //Gameplay Programmers Script for the Player Health
    private ReferenceManager referenceManager;
    private PlayerDeath playerDeath;

    private void Start()
    {
        playerDeath = character.player.GetComponent<PlayerDeath>();
        rb = GetComponent<Rigidbody2D>();
        dashTrail = gameObject.GetComponent<TrailRenderer>();
        transform.GetComponentInChildren<AIAnimationController>().ChangeAnimationState(AIAnimationController.AnimationStates.SwordAttack); // Change to boss dash animation
    }

    public DashAttackState()
    {
        //When a character goes to the dash attack state, this will delay the attack by x amount
        currentDelay = 0.2f;
    }

    public override void UpdateLogic()
    {
        //Set the reference for the playerDeath variable
        if (playerDeath == null)
        {
            playerDeath = character.player.GetComponent<PlayerDeath>();
        }

        //Counts down the delay
        currentDelay -= Time.deltaTime;

        lastKnownPos = character.player.transform.position;
        

        //Checks if the delay timer has hit 0, if so, it will damage the player and reset the delay timer to x amount
        if (currentDelay <= 0 && isDashing == false)
        {
            StartCoroutine(Dash());
            //playerDeath.RemoveHealth(attackDamage);
            currentDelay = 2;
        }
    }

    private IEnumerator Dash() // dash attack mechanic
    {
        isDashing = true;

        //Prepares to dash - needs to stop looking for the players location
        yield return new WaitForSeconds(dashWindUp);
        Vector3 dirVector = lastKnownPos - transform.position;
        //dirVector.Normalize();
        rb.velocity = dirVector * dashSpeed; //Actual dash
        AudioManager.Manager.PlaySFX("BossDash");
        dashTrail.emitting = true;
        if(dirVector.magnitude < 2)
        {
            playerDeath.RemoveHealth(attackDamage);
        }
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        dashTrail.emitting = false; //End of dash
        GetComponent<BossStateMachineController>().reloadCountdown++;
        character.isAttacking = false;
    }
}
