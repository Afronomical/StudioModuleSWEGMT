using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateBaseClass
{
    public GameObject player = GameObject.FindGameObjectWithTag("Player");
    public GameObject hunter = GameObject.FindGameObjectWithTag("Hunter");
    public float speed = 5f;
    public override void UpdateLogic()
    {
        Debug.Log("Is attacking");
        hunter.transform.position = Vector3.MoveTowards(hunter.transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
