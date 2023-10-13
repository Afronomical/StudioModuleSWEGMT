using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateBaseClass
{
    public float speed = 5f;

    public override void UpdateLogic()
    {
        Debug.Log("Is attacking");
        character.transform.position = Vector3.MoveTowards(character.transform.position, character.player.transform.position, speed * Time.deltaTime);
    }
}
