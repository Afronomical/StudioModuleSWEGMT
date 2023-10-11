using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeadState : StateBaseClass
{
    
    //public DeathState()
    //{
       
    //}


    public override void UpdateLogic()
    {
        //Disables the specific character when health is 0
        character.gameObject.SetActive(false);
        Debug.Log("Character Disabled");

    }
}
