using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoffinInteraction : MonoBehaviour
{
    private BoxCollider2D coffinArea;
    private Feeding hungerCheck;

    //private TMP_Text tooltip;

    public TMP_Text closedCoffin;
    public TMP_Text openCoffin;
    public TMP_Text useCoffin;
    public int hungerThreshold = 5; //This might need to be moved into 

    // Start is called before the first frame update
    void Start()
    {
        coffinArea = GetComponent<BoxCollider2D>();
        hungerCheck = GameObject.Find("PlayerPrefab1").GetComponent<Feeding>();

        //Coffin closed
        coffinArea.isTrigger = false;
        //tooltip = GameObject.Find("Coffin Closed Text").GetComponent<TMP_Text>();
        //tooltip.enabled = false;

        closedCoffin.enabled = false;
        openCoffin.enabled = false;
        useCoffin.enabled = false;
    }

    //When coffin is closed
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            //Display a message to the player that you cant sleep yet
            closedCoffin.enabled = true;
            print("Can't sleep yet, I must feast first!");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            closedCoffin.enabled = false; //Remove tooltip off screen
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
            openCoffin.enabled = true;
            print("Coffin opened, I can finally rest!");
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
            openCoffin.text = null;
            useCoffin.enabled = true;
        }

        //Press a button to sleep
        if(Input.GetKeyDown(KeyCode.R))
        {
            //Sleep - go to next level

            print("Sleeping...");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        useCoffin.enabled = false;
    }
}
