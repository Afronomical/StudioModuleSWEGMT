using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateBaseClass
{
    public float speed = 5f;


    //function should be responsible for dealing damage to the player
    public override void UpdateLogic()
    {
        //Debug.Log("Is attacking");

        //this was moved to HuntState
        //character.transform.position = Vector2.MoveTowards(character.transform.position, character.player.transform.position, speed * Time.deltaTime / 2);

        
    }

    private void DealDamage()
    {
        
    }
}
