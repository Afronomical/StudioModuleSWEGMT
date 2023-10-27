
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feeding : MonoBehaviour
{
    public int currentHunger;
    public int minHunger = 0;
    public KeyCode healKey = KeyCode.E; // The key to trigger healing
    private bool canHeal = false; // To check if the player is inside the healing zone
    public List<AICharacter> aiCharacters; // Use a list to store references to AI characters
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
            foreach (AICharacter aiChar in aiCharacters)
            {
                if (aiChar.currentState == AICharacter.States.Downed)
                {
                    AudioManager.Manager.PlayVFX("PlayerFeed");
                    //AudioManager.Manager.PlayVFX("PlayerFeed");
                    currentHunger += 1;
                    hungerBarSlider.SetHunger(currentHunger);

                    // Call a method in the PlayerDeath script to increase player health
                    playerDeath.FeedAttack();
                }
            }
        }
    }
}








