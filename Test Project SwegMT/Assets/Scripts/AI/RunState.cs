using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : StateBaseClass
{
    public GameObject player = GameObject.FindGameObjectWithTag("Player");
    public GameObject villager = GameObject.FindGameObjectWithTag("Villager");
    public float speed = 5f;

    public override void UpdateLogic()
    {
        Debug.Log("Is running");
        Vector3 dist = player.transform.position - villager.transform.position;
        //villager.transform.position = Vector3.Lerp(villager.transform.position, villager.transform.position + new Vector3(1, 0, 1), speed * Time.deltaTime);
        //villager.transform.position = Vector3.MoveTowards(villager.transform.position, villager.transform.position + new Vector3(1, 0, 1), speed * Time.deltaTime);
    }
}
