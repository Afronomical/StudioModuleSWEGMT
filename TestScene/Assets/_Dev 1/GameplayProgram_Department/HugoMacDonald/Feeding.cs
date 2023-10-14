
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feeding : MonoBehaviour
{
    public int currentHunger;
    public int minHunger = 0;
    public KeyCode healKey = KeyCode.E; // The key to trigger healing
    private bool canHeal = false; // To check if the player is inside the healing zone
    public AICharacter aiCharacter;
    public AICharacter aiCharacter1;
    public HungerBar hungerBarSlider;



    private void Start()
    {
        
        currentHunger = minHunger;
        hungerBarSlider.SetMinHunger(minHunger);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("feedingZone")) // Make sure the player enters the zone
        {
            canHeal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("feedingZone")) // Player left the zone
        {
            canHeal = false;
        }
    }

    private void Update()
    {
        if (canHeal && Input.GetKeyDown(healKey)) // can heal is true (player inside the zone) and e key is being pressed down
        {
            if ((aiCharacter.currentState == AICharacter.States.Downed || aiCharacter1.currentState == AICharacter.States.Downed) && canHeal && Input.GetKeyDown(healKey))
            {
                
                currentHunger += 1;
                hungerBarSlider.SetHunger(currentHunger); //
                
            }
           



        }
    }
}






