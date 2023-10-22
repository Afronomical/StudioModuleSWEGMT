using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class HuntState : StateBaseClass
{
    public float speed = 5f;

    public override void UpdateLogic()
    {
        //Debug.Log("Is attacking");
        character.transform.position = Vector2.MoveTowards(character.transform.position, character.player.transform.position, speed * Time.deltaTime / 2);


    }
}
