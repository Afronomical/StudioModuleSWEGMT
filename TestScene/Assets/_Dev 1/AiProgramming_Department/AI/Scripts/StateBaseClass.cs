using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBaseClass : MonoBehaviour
{
    public AICharacter character;

    public virtual void UpdateLogic()
    {
        //handles the logic for each individual state
        //need to liaise with leads
    }
}
