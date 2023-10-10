using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBaseClass
{
    public AICharacter character;
    //public GameObject player;

    public virtual void UpdateLogic()
    {
        //handles the logic for each individual state
        //need to liaise with leads
    }
}
