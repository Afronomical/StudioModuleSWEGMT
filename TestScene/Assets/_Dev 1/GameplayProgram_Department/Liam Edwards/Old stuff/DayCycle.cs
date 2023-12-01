using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public Gradient colorGradient; // A gradient to hold the color

    public float changeSpeed; // The speed at which the colour changes
    //private float colourChange = 0f; // A float to hold the current colour
    public float timeInSun = 0; // A float to calculate the amount of time the player has spent in the sun
    public float nightTime; // The amount of time remaining in the night
    public int sunBurn; //
    public float damageDelay = 1.5f; // Num of seconds between the delay in damage whrn the player is in the sun

    public bool canBurn = true; // Bool to check if the player can take damage

    public CountdownTimer countdownTimer;
    public PlayerDeath playerDeath;
    
    

    public void Start()
    {
        countdownTimer = CanvasManager.Instance.countdownTimer;

        canBurn = true;
        gameObject.transform.localScale = new Vector2(1000, 1000); // Sets Size Of SunLight Game Object (width, height) Needs to cover the whole level
        transform.position = new Vector3(0, 0, 0); // Sets Pos to Off The Screen

        //Color currentColour = colorGradient.Evaluate(colourChange);
        //GetComponent<Renderer>().material.color = currentColour;
        
    }


    // Update is called once per frame
    void Update()
    {
       
        nightTime = countdownTimer.timeRemaining; // Links Up The "nightTime" Variable To The Countdown Clock

        if (nightTime <= 0) // Checks For When The Timer Reaches 
        {            
            ColourChange();
            timeInSun += Time.deltaTime;
            if (transform.GetComponent<Collider2D>().IsTouching(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>()))
            {                              
                if (timeInSun < 5 && canBurn == true)
                {
                    sunBurn = 1;
                    StartCoroutine(sunDamage());
                }
                else if (timeInSun >= 5 && timeInSun < 15 && canBurn == true)
                {
                    sunBurn = 3;
                    StartCoroutine(sunDamage());
                }
                else if (timeInSun >= 15 && timeInSun < 30 && canBurn == true)
                {
                    sunBurn = 5;
                    StartCoroutine(sunDamage());
                }               
            }                
        }                              
    }

    private IEnumerator sunDamage()
    {
        canBurn = false;
        playerDeath.currentHealth = playerDeath.currentHealth - sunBurn;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>().SunRiseDamage(); // Calls The Function "SunRiseDamage" From "playerDeath" Script
        //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>().showFloatingText(sunBurn);
        yield return new WaitForSeconds(damageDelay);
        canBurn = true;
    }

    public void ColourChange()
    {
        // Update the colourChange value
        //colourChange += Time.deltaTime / changeSpeed;

        // Get the colour from the gradient
        //Color currentColor = colorGradient.Evaluate(colourChange);

        // Set the colour of the object
        //GetComponent<Renderer>().material.color = currentColor;
    }
}


          
