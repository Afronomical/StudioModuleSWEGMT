
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
        if (canHeal && Input.GetKeyDown(healKey))
        {
            

            // Assuming your PlayerDeath script is attached to the same GameObject as this script
            PlayerDeath playerDeath = GetComponent<PlayerDeath>();

            if (playerDeath != null)
            {
                // Update the currentHealth variable in the PlayerDeath script
                playerDeath.currentHealth += healAmount;

                // Debug.Log the updated currentHealth
                Debug.Log(playerDeath.currentHealth);
            }
        }
    }
}






