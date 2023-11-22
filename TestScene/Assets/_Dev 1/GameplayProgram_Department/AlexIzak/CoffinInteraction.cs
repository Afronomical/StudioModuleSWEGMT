using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoffinInteraction : MonoBehaviour
{
    private BoxCollider2D coffinArea;
    private Feeding feeding;
   
    

    //private TMP_Text tooltip;

    public TMP_Text closedCoffin;
    public TMP_Text stillHungry;
    public TMP_Text openCoffin;
    public TMP_Text useCoffin;
    public string areaToMove = "Spawn";
    public int hungerThreshold = 5; //This might need to be moved into 

    //private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        coffinArea = GetComponent<BoxCollider2D>();
        feeding = PlayerController.Instance.GetComponent<Feeding>();
        //feeding = GameObject.Find("PlayerPrefab1").GetComponent<Feeding>();

        //Coffin closed
        coffinArea.isTrigger = false;
        //tooltip = GameObject.Find("Coffin Closed Text").GetComponent<TMP_Text>();
        //tooltip.enabled = false;

        closedCoffin.enabled = false;
        stillHungry.enabled = false;
        openCoffin.enabled = false;
        useCoffin.enabled = false;

        //gameManager = FindAnyObjectByType<GameManager>();
        //ToolTipManager.HideBottomToolTip_Static();
        
    }

    //When coffin is closed
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If you just started the game - do this
        if(collision.collider.CompareTag("Player") && feeding.currentHunger == 0) 
        {
            //Display a message to the player that you cant sleep yet
            closedCoffin.enabled = true;
            print("Can't sleep yet, I must feast first!");
        }
        //If player ate at least 1 human - do this
        else if(collision.collider.CompareTag("Player") && feeding.currentHunger > 0 && feeding.currentHunger < hungerThreshold)
        {
            stillHungry.enabled = true;
        }
        //If hunger satiated - do this
        else if(collision.collider.CompareTag("Player") && feeding.currentHunger >= hungerThreshold)
        {
            //Display tooltip
            openCoffin.text = null;
            useCoffin.enabled = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //If hunger satiated - do this
        //if (collision.collider.CompareTag("Player") && hungerCheck.currentHunger == hungerThreshold)
        //{
        //    //Display tooltip
        //    openCoffin.enabled = false;
        //    useCoffin.enabled = true;
        //}

        //Press a button to sleep
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Sleep - go to next level
            SceneManager.LoadScene(areaToMove);
            //print("Sleeping...");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            closedCoffin.enabled = false; //Remove tooltip off screen
            stillHungry.enabled = false;

            if (useCoffin != null)
                useCoffin.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (areaToMove != GameManager.Instance.nextLevel)
        {
            areaToMove = GameManager.Instance.nextLevel;
        }

        //Check hunger meter for threshold
        if(feeding.currentHunger >= hungerThreshold && openCoffin != null) 
        {
            //Open if this is met
            openCoffin.enabled = true;
            //print("Coffin opened, I can finally rest!");
            //ToolTipManager.ShowBottomToolTip_Static("Coffin Opened, I can finally Rest!");
            
        }

        //Press a button to sleep
        if (Input.GetKeyDown(KeyCode.R) && useCoffin.enabled)
        {
            //Sleep - go to next level
            SceneManager.LoadScene(areaToMove);
            PlayerController.Instance.GetPlayerDeath().currentHealth = PlayerController.Instance.GetPlayerDeath().maxHealth;
            PlayerController.Instance.GetFeeding().currentHunger = 0;
            CanvasManager.Instance.hungerBarUI.SetHunger(0);
            useCoffin.enabled = false;
            //print("Sleeping...");
        }
    }

    //When coffin is open
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if(collision.CompareTag("Player") && hungerCheck.currentHunger >= hungerThreshold)
    //    {
    //        //Display tooltip
    //        openCoffin.text = null;
    //        useCoffin.enabled = true;
    //    }

    //    //Press a button to sleep
    //    if(Input.GetKeyDown(KeyCode.R))
    //    {
    //        //Sleep - go to next level
    //        SceneManager.LoadScene("Spawn");
    //        //print("Sleeping...");
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (useCoffin !=  null)
    //        useCoffin.enabled = false;
    //}
}
