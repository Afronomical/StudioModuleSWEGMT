using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateBaseClass
{
    public int attackDamage = 10;
    public float attackDelay = 2;
    private float currentAttackDelay;
    
    //Gameplay Programmers Script for the Player Health
    private PlayerDeath playerDeath;  
    
    
    
    public override void UpdateLogic()
    {
        //Set the reference for the playerDeath variable
        if (playerDeath == null)
        {
            playerDeath = character.player.GetComponent<PlayerDeath>();
        }
        
        Debug.Log("Is attacking");

        //Delays the Attacks
        currentAttackDelay = currentAttackDelay - Time.deltaTime;

        //Checks if the delay timer has hit 0, if so, it will damage the player and reset the delay timer to x amount
        if (currentAttackDelay <= 0)
        {
            playerDeath.SetHealth(attackDamage);
            currentAttackDelay = attackDelay;
        }

        //this was moved to HuntState
        //character.transform.position = Vector2.MoveTowards(character.transform.position, character.player.transform.position, speed * Time.deltaTime / 2);


    }



}
