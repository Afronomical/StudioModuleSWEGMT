using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRise : MonoBehaviour
{
   
    public float maxScale;
    public float minScale;
    public float scaleSpeed; // How Fast The Sun Light Moves
    private bool canBurn = false;
    private float damageDelay = 1; // The delay Btween Damage While In The Sun Light
    public float nightTime; 
    public CountdownTimer countdownTimer;


    public void Start()
    {   
        transform.position = new Vector3(52, 0, 0.5f); // Sets Pos to Off The Screen
        canBurn = true;
        gameObject.GetComponent<Renderer>().enabled = false;
    }



    // Update is called once per frame
    void Update()
    {

        
        nightTime = countdownTimer.timeRemaining; // Links Up The "nightTime" Variable To The Countdown Clock


        if (nightTime <= 0) // Checks For When The Timer Reaches 0
        {
            
            gameObject.GetComponent<Renderer>().enabled = true; // When Countdown Reaches 0 SunLight Object Will Activate

            if (transform.localScale.x <= maxScale && transform.localScale.y <= maxScale)
            {
               transform.localScale += new Vector3(scaleSpeed, scaleSpeed, 0) * Time.deltaTime;
            }
            else
            {
               transform.localScale = new Vector3(minScale, minScale, 0);
            }
            if (transform.GetComponent<Collider2D>().IsTouching(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>()) && canBurn == true) // When Player Touches Sun Light And canBurn = true 
            {   
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>().SunRiseDamage(); 
                StartCoroutine(sunDamage()); 
            }



        }

        
    }

    private IEnumerator sunDamage()
    {
        canBurn = false;
        yield return new WaitForSeconds(damageDelay);
        canBurn = true;
    }
}