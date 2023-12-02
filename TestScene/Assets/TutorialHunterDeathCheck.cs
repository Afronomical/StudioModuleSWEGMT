using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHunterDeathCheck : MonoBehaviour
{
    private AICharacter character;
    private float HunterHealth;
    public GameObject HunterBeatenBoxCollider;

    private void Start()
    {
        character = GetComponent<AICharacter>();    
    }


    private void Update()
    {
        if(character != null)
        {
             HunterHealth = character.health; 
        }
        if(HunterHealth <= 0)
        {
            ///enable progression 
            HunterBeatenBoxCollider.SetActive(false);
        }

        
    }
}
