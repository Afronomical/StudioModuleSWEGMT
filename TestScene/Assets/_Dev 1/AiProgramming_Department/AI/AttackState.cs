using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateBaseClass
{
    public int attackDamage = 10;
    public float attackDelay = 2;
    
    //Gameplay Programmers Script for the Player Health
    private PlayerDeath playerDeath;  
    public ReferenceManager refMan;
    public override void UpdateLogic()
    {
       //Set the reference for the refMan variable
        if(refMan == null) 
        {
            GameObject refmanObject = GameObject.Find("Reference Manager");
            refMan = refmanObject.GetComponent<ReferenceManager>();
        }
        //Set the reference for the playerDeath variable
        if (playerDeath == null)
        {
            playerDeath = character.player.GetComponent<PlayerDeath>();
        }
        
        Debug.Log("Is attacking");

        //Checks if the delay timer has hit 0, if so, it will damage the player and reset the delay timer to x amount
        if (refMan.aiattackDelay <= 0)
        {
            refMan.StartCount = false;
            playerDeath.SetHealth(attackDamage);
            refMan.aiattackDelay = attackDelay;
            refMan.StartCount = true;
        }

        //this was moved to HuntState
        //character.transform.position = Vector2.MoveTowards(character.transform.position, character.player.transform.position, speed * Time.deltaTime / 2);


    }

    


}
