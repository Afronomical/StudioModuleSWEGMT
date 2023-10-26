using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : StateBaseClass
{
    public int attackDamage = 20;
    public float attackDelay = 5;
    private float currentDelay;

    //Gameplay Programmers Script for the Player Health
    private ReferenceManager referenceManager;
    private PlayerDeath playerDeath;

    private void Start()
    {
        playerDeath = referenceManager.GetPlayer().GetComponent<PlayerDeath>();
    }
    public ShootState()
    {
        //When a character goes to the attack state, this will delay the attack by x amount
        currentDelay = 0.2f;
    }

    public override void UpdateLogic()
    {
        //change colour to indicate state change
        this.GetComponent<SpriteRenderer>().color = Color.cyan;

        //Set the reference for the playerDeath variable
        if (playerDeath == null)
        {
            playerDeath = character.player.GetComponent<PlayerDeath>();
        }

        Debug.Log("AI is attacking");

        //Counts down the delay
        currentDelay -= Time.deltaTime;

        //Checks if the delay timer has hit 0, if so, it will damage the player and reset the delay timer to x amount
        if (currentDelay <= 0)
        {
            playerDeath.SetHealth(attackDamage);
            currentDelay = 2;
        }
    }
}
