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
        //Wait();
        //Debug.Log("Is patrolling");
        //float rand1 = Random.Range(-100, 100);
        //float rand2 = Random.Range(-100, 100);

        ////while (waitingTime > 0.01)
        ////{
        //    hunter.transform.position = Vector3.MoveTowards(hunter.transform.position, new Vector3(rand1, 1, rand2), speed * Time.deltaTime);
        
        ////}

    }

    //private IEnumerator Wait()
    //{
    //    yield return new WaitForSeconds(5);
    //}

}
