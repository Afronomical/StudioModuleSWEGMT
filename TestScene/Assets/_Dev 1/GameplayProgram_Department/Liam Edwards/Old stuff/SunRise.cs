using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRise : MonoBehaviour
{
   
    public float scaleSpeed = 3; // How Fast The Sun Light Moves
    private bool canBurn = false;
    private float damageDelay = 1; // The delay Between Damage While In The Sun Light
    public float nightTime; // The amount of time remaining in the night
    public CountdownTimer countdownTimer;


    public void Start()
    {

        gameObject.transform.localScale = new Vector2(0.1f, 100); // Sets Size Of SunLight Game Object (width, height)
        transform.position = new Vector3(52, 0, 0.5f); // Sets Pos to Off The Screen
        canBurn = true;
        gameObject.GetComponent<Renderer>().enabled = false; // Disables Sun Light

    }




    // Update is called once per frame
    void Update()
    {
                
        nightTime = countdownTimer.timeRemaining; // Links Up The "nightTime" Variable To The Countdown Clock


        if (nightTime <= 0) // Checks For When The Timer Reaches 0
        {
            
            gameObject.GetComponent<Renderer>().enabled = true; // When Countdown Reaches 0 SunLight Object Will Activate
            transform.localScale += new Vector3(scaleSpeed, scaleSpeed, 0) * Time.deltaTime; // gradually increase the local scale of the object over time.

            if (transform.GetComponent<Collider2D>().IsTouching(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>()) && canBurn == true) // When Player Touches Sun Light And canBurn = true 
            {   
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>().SunRiseDamage(); // Calls The Function "SunRiseDamage" From "playerDeath" Script
                StartCoroutine(sunDamage()); 
            }

        }
               
    }

    private IEnumerator sunDamage()
    {
        canBurn = false;
        yield return new WaitForSeconds(damageDelay);
        canBurn = true;
        AudioManager.Manager.PlaySFX("PlayerTakeDamage");
    }
}