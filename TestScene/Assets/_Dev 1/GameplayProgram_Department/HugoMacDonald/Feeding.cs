using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feeding : MonoBehaviour
{
    public int currentHunger;
    public int minHunger = 0;
    public KeyCode healKey = KeyCode.E; // The key to trigger healing
    private bool canHeal = false; // To check if the player is inside the healing zone
    private AICharacter currentTarget = null; // Store the current AI character being healed
    public HungerBar hungerBarSlider;
    public PlayerDeath playerDeath; // Reference to the PlayerDeath script

    private void Start()
    {
        currentHunger = minHunger;
        hungerBarSlider.SetMinHunger(minHunger);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("feedingZone")) // Make sure the player enters the zone
        {
            AICharacter aiCharacter = other.GetComponentInParent<AICharacter>();
            if (aiCharacter != null && aiCharacter.currentState == AICharacter.States.Downed)
            {
                canHeal = true;
                currentTarget = aiCharacter;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("feedingZone")) // Player left the zone
        {
            canHeal = false;
            currentTarget = null;
        }
    }

    private void Update()
    {
        if (canHeal && Input.GetKeyDown(healKey) && currentTarget != null)
        {
            // Feed on the current AI character in the feeding zone when it's downed
            currentHunger += 1;
            hungerBarSlider.SetHunger(currentHunger);

            // Call a method in the PlayerDeath script to increase player health
            playerDeath.FeedAttack();
        }
    }
}














