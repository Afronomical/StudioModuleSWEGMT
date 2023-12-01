using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feeding : MonoBehaviour
{
    public int currentHunger;
    public int minHunger = 0;
    public KeyCode healKey = KeyCode.E; // The key to trigger healing
    public KeyCode feedKey = KeyCode.F; // The key to trigger feeding
    private bool canHeal = false; // To check if the player is inside the healing zone
    private bool canFeed = false; // Flag to track whether the player is inside the feeding zone
    private bool hasFed = false; // Flag to track whether the player has fed
    private AICharacter currentTarget = null; // Store the current AI character being healed
    public HungerBar hungerBarSlider;
    public GameObject flashingCanvas;
    public PlayerDeath playerDeath; // Reference to the PlayerDeath script

    private Animator animator;
    private PlayerAnimationController animationController;
    public ToolTipManager toolTipManager;
    public float durationTime = 3.0f;
    private float overlapRadius; // Adjust the radius as needed
    private float feedDelay = 0.44f; // Adjust the delay duration
    public bool currentlyFeeding = false;

    public GameObject BloodOnFeed;
    private GameObject biteIcon;

    private void Start()
    {
        biteIcon = transform.GetChild(3).gameObject;
        biteIcon.SetActive(false);
        currentlyFeeding = false;
        overlapRadius = 0.9f;
        currentHunger = minHunger;
        hungerBarSlider.SetMinHunger(minHunger);

        animator = GetComponent<Animator>();
        animationController = GetComponent<PlayerAnimationController>();
    }

    private IEnumerator DelayedFeed()
    {
        yield return new WaitForSeconds(feedDelay);

        canHeal = true;
        canFeed = true;
        hasFed = false; // Reset the flag after the delay
        currentlyFeeding = false;
        biteIcon.SetActive(false);
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, overlapRadius);

        bool foundFeedingZone = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("feedingZone"))
            {
                AICharacter aiCharacter = collider.GetComponentInParent<AICharacter>();
                if (aiCharacter != null && aiCharacter.currentState == AICharacter.States.Downed && !hasFed)
                {
                    canHeal = true;
                    canFeed = true;
                    currentTarget = aiCharacter;
                    foundFeedingZone = true;
                    break; // Exit the loop if a feeding zone is found
                }
            }
        }

        if (!foundFeedingZone)
        {
            canHeal = false;
            canFeed = false;
            currentTarget = null;
        }

        if (canHeal && Input.GetKeyDown(healKey) && currentTarget != null)
        {
            currentlyFeeding = true;
            playerDeath.FeedAttack();
            Instantiate(BloodOnFeed, currentTarget.transform.position, Quaternion.identity);
            // Play Feed SFX
            AudioManager.Manager.PlaySFX("PlayerFeed");
            
            animationController.ChangeAnimationState(PlayerAnimationController.AnimationStates.Feed);
            currentTarget.health -= 1;
            StartCoroutine(DelayedFeed());
        }
        else if (canFeed && Input.GetKeyDown(feedKey) && currentTarget != null)
        {
            // Code for feeding
            biteIcon.SetActive(true);
            currentlyFeeding = true;
            // Play blood VFX
            Instantiate(BloodOnFeed, currentTarget.transform.position, Quaternion.identity);
            // Play Feed SFX
            AudioManager.Manager.PlaySFX("PlayerFeed");
            // Feed on the current AI character in the feeding zone when it's downed
            currentHunger += currentTarget.hungerValue;

            hungerBarSlider.SetHunger(currentHunger);
            // flashingCanvas.SetActive(false);
            currentTarget.health -= 1;

            // Call a method in the PlayerDeath script to increase player health
            
            CanvasManager.Instance.toolTipManager.ShowTopToolTip_Static("TASTY! Let's keep going before Sunlight hits!", durationTime);

            animationController.ChangeAnimationState(PlayerAnimationController.AnimationStates.Feed);

            hasFed = true; // Set the flag to true after feeding
            StartCoroutine(DelayedFeed()); // Start the delay before allowing another feed
        }

        if (foundFeedingZone)
        {
            biteIcon.SetActive(true);
        }

        if (!foundFeedingZone && !currentlyFeeding)
        {
            biteIcon.SetActive(false);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wire sphere in the scene view to represent the overlap circle radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, overlapRadius);
    }
}

