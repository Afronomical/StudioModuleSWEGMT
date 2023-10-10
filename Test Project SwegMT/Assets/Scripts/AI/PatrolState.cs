using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatrolState : StateBaseClass
{
    private GameObject hunter = GameObject.FindGameObjectWithTag("Hunter");
    public float speed = 5f;
    public float waitingTime = 3f;

    public override void UpdateLogic()
    {
        Debug.Log("Is patrolling");

        //while (true)
        //{
        //    hunter.transform.position = Vector3.MoveTowards(hunter.transform.position, new Vector3(Random.Range(0f, 10f), 0, Random.Range(0f, 10f)), speed * Time.deltaTime);
        //    waitingTime -= Time.deltaTime;
        //    if (waitingTime < 0.01)
        //    {
        //        break;
        //    }
        //}

    }

}
