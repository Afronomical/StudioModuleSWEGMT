using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoffinInteraction : MonoBehaviour
{
    private BoxCollider2D coffinArea;
    private Feeding hungerCheck;
    private bool canPlay = false;
    

    //private TMP_Text tooltip;

    public TMP_Text closedCoffin;
    public TMP_Text stillHungry;
    public TMP_Text openCoffin;
    public TMP_Text useCoffin;
    public int hungerThreshold = 5; //This might need to be moved into 

    public ToolTipManager toolTipManager;
    public GameObject bottomScreenToolTip;
    public GameObject topScreenToolTip;
    private float tooltipDisplayDuration = 6.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        coffinArea = GetComponent<BoxCollider2D>();
        hungerCheck = GameObject.Find("PlayerPrefab1").GetComponent<Feeding>();

        ToolTipManager.ShowTopToolTip_Static("This Hunger Bar needs filling! Get out there and eat!", tooltipDisplayDuration);
        //Coffin closed
        coffinArea.isTrigger = false;
        //tooltip = GameObject.Find("Coffin Closed Text").GetComponent<TMP_Text>();
        //tooltip.enabled = false;

        closedCoffin.enabled = false;
        stillHungry.enabled = false;
        openCoffin.enabled = false;
        useCoffin.enabled = false;
        ToolTipManager.HideBottomToolTip_Static();
        
    }

    //When coffin is closed
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player") && hungerCheck.currentHunger == 0)
        {
            //Display a message to the player that you cant sleep yet
            //closedCoffin.enabled = true;
            //print("Can't sleep yet, I must feast first!");

            ToolTipManager.ShowBottomToolTip_Static("Can't Sleep yet, I must eat first");
        }
        else if(collision.collider.CompareTag("Player") && hungerCheck.currentHunger > 0 && hungerCheck.currentHunger < hungerThreshold)
        {
            stillHungry.enabled = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            closedCoffin.enabled = false; //Remove tooltip off screen
            stillHungry.enabled = false;
            ToolTipManager.HideBottomToolTip_Static();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Check hunger meter for threshold
        if(hungerCheck.currentHunger >= hungerThreshold) 
        {
            //Open if this is met - make the current box collider into a trigger instead
            coffinArea.isTrigger = true;
            
           // openCoffin.enabled = true;
            //print("Coffin opened, I can finally rest!");
            ToolTipManager.ShowBottomToolTip_Static("Coffin Opened, I can finally Rest!");
            
        }
        else
        {
            openCoffin.enabled = false;

        }
    }

    //When coffin is open
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && hungerCheck.currentHunger >= hungerThreshold)
        {
            //Display tooltip
            //openCoffin.text = null;
            useCoffin.enabled = true;
            ToolTipManager.ShowBottomToolTip_Static("Use the coffin");
        }

        //Press a button to sleep
        if(Input.GetKeyDown(KeyCode.R))
        {
            //Sleep - go to next level
            AudioManager.Manager.PlaySFX("CoffinOpen");
            print("Sleeping...");
            ToolTipManager.ShowBottomToolTip_Static("Sleeping...");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (useCoffin !=  null)
            useCoffin.enabled = false;
        ToolTipManager.HideBottomToolTip_Static();
    }

    
}
