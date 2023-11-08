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
    public GameObject flashingCanvas;
    public PlayerDeath playerDeath; // Reference to the PlayerDeath script

    private Animator animator;
    private PlayerAnimationController animationController;
    public ToolTipManager toolTipManager;
    public float durationTime = 3.0f;
    

    private void Start()
    {
        currentHunger = minHunger;
        hungerBarSlider.SetMinHunger(minHunger);

        animator = GetComponent<Animator>();
        animationController = GetComponent<PlayerAnimationController>();
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
            //Play Feed SFX
            AudioManager.Manager.PlaySFX("PlayerFeed");
            // Feed on the current AI character in the feeding zone when it's downed
            currentHunger += currentTarget.hungerValue;
            
            hungerBarSlider.SetHunger(currentHunger);
            //flashingCanvas.SetActive(false);
            currentTarget.health -= 1;

            // Call a method in the PlayerDeath script to increase player health
            playerDeath.FeedAttack();
            ToolTipManager.ShowTopToolTip_Static("TASTY! Let's keep going before Sunlight hits!", durationTime);
        }

        if (Input.GetKey(healKey) && currentTarget != null)
        {
            animationController.ChangeAnimationState(PlayerAnimationController.AnimationStates.Feed);
            
        }
    }

}














