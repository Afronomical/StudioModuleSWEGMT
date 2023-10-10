using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeadState : StateBaseClass
{
    private GameObject hunter = GameObject.FindGameObjectWithTag("Hunter");
    private GameObject villager = GameObject.FindGameObjectWithTag("Villager");
    public override void UpdateLogic()
    {
        Debug.Log("Is dead");
        //GameObject.Destroy(hunter);
        //GameObject.Destroy(villager);
        //Object.DestroyObject(AICharacter);
        

    }
}
