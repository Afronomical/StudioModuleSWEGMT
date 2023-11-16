using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttackState : StateBaseClass
{
    //Dash attack
    public int attackDamage = 10;
    public float dashDelay = 2f;
    private float dashSpeed = 50f;
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
        //transform.GetComponentInChildren<AIAnimationController>().ChangeAnimationState(AIAnimationController.AnimationStates.SwordAttack); // Change to boss dash animation
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
        if (currentDelay <= 0)
        {
            StartCoroutine(Dash());
            //playerDeath.RemoveHealth(attackDamage);
            currentDelay = 2;
        }

        //this was moved to HuntState
        //character.transform.position = Vector2.MoveTowards(character.transform.position, character.player.transform.position, speed * Time.deltaTime / 2);
    }

    private IEnumerator Dash() // dash attack mechanic
    {
        isDashing = true;

        //Records players location
        //float lastKnownX = character.player.transform.position.x;
        //float lastKnownY = character.player.transform.position.y;

        //Prepares to dash - needs to stop looking for the players location
        yield return new WaitForSeconds(dashWindUp);
        Vector3 dirVector = lastKnownPos - transform.position;
        dirVector.Normalize();
        rb.velocity = dirVector * dashSpeed; //Actual dash
        dashTrail.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        dashTrail.emitting = false; //End of dash
    }
}
