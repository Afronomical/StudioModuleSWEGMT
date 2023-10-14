
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feeding : MonoBehaviour
{
    public int healAmount = 10; // Amount of healing to provide
    public KeyCode healKey = KeyCode.E; // The key to trigger healing
    private bool canHeal = false; // To check if the player is inside the healing zone
    public AICharacter aiCharacter;
    public AICharacter aiCharacter1;

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
        if (canHeal && Input.GetKeyDown(healKey)) // canheal is true (player inside the zone) and e key is being pressed down
        {
            if ((aiCharacter.currentState == AICharacter.States.Downed || aiCharacter1.currentState == AICharacter.States.Downed) && canHeal && Input.GetKeyDown(healKey))
            {
                Debug.Log("Feed Successfull");
            }
           



        }
    }
}






