
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feeding : MonoBehaviour
{
    public int healAmount = 10; // Amount of healing to provide
    public KeyCode healKey = KeyCode.E; // The key to trigger healing
    private bool canHeal = false; // To check if the player is inside the healing zone
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("healingZone")) // Make sure the player enters the zone
        {
            canHeal = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("healingZone")) // Player left the zone
        {
            canHeal = false;
        }
    }

    private void Update()
    {
        if (canHeal && Input.GetKeyDown(healKey)) // canheal is true (player inside the zone) and e key is being pressed down
        {
            

            // currently references to the death script but soon there will be a dedicated script for health
            PlayerDeath playerDeath = GetComponent<PlayerDeath>();

            if (playerDeath != null)
            {
                // Update the currentHealth variable in the PlayerDeath script
                playerDeath.currentHealth += healAmount;

                //debug the health and make sure its updating
                Debug.Log(playerDeath.currentHealth);
            }
        }
    }
}






