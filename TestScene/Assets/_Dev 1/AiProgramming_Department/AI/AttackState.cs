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

    public AttackState() 
    {
        //When a character goes to the attack state, this will delay the attack by x amount
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

        //Checks if the delay timer has hit 0, if so, it will damage the player and reset the delay timer to x amount
        if (currentDelay <= 0)
        {
            playerDeath.SetHealth(attackDamage);
            currentDelay = 2;
        }
        
        //this was moved to HuntState
        //character.transform.position = Vector2.MoveTowards(character.transform.position, character.player.transform.position, speed * Time.deltaTime / 2);
    }
}
