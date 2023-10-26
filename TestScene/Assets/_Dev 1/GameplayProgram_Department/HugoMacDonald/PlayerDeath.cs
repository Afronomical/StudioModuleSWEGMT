using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class PlayerDeath : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    //private int damageAmount = 10;
    private Animator animator;
    public HealthBarScript healthBarScript;
    public bool godMode;
    public bool canDamage = true;
    public int sunDamage = 5;
    //public float SetHealth;


    private void Start()
    {   
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        healthBarScript.SetMaxHealth(maxHealth);
        canDamage = true;
    }

    private void Update()
    {

        if (currentHealth <= 0)
        {
            Die();
        }


        //if godmode enabled set health to 100 every tick so is esentailly immortal
        if (godMode)
        {
            currentHealth = maxHealth;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("Collision detected");
    //    if (collision.gameObject.CompareTag("Hunter"))
    //    {
    //        currentHealth -= damageAmount;
    //        healthBarScript.setHealth(currentHealth);
    //        Debug.Log(currentHealth);
    //        if (currentHealth <= 0)
    //        {
    //            Die();
    //        }
    //    }
    //}

    public void SetHealth(int damage)
    {
        currentHealth -= damage;
        healthBarScript.setHealth(currentHealth);
    }

    public void FeedAttack()
    {
        // Implement your logic to increase health when the player is fed
        currentHealth += 20; // You can adjust the value as needed
        healthBarScript.setHealth(currentHealth);
    }

    private void Die()
    {
        if (animator != null)
        {
            //animator.SetTrigger("Die"); // Make sure your Animator has a "Die" trigger.
        }

        gameObject.SetActive(false);
        //Instantiate(...);              //spawn "YOU DIED" ui
        Invoke("deathAfterDelay", 1);

    }
    private void deathAfterDelay()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SunRiseDamage() // Deals Damage While The Player Is In Sun Light
    {
        currentHealth = currentHealth - sunDamage;
        healthBarScript.setHealth(currentHealth);
    }



}


