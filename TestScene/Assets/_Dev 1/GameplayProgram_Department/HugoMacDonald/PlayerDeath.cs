

using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public int maxHealth = 100;
    
    public int currentHealth;
   
    private int damageAmount = 10;
    private Animator animator;
    public HealthBarScript healthBarScript;
    


    private void Start()
    {
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
        healthBarScript.SetMaxHealth(maxHealth);
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected");
        if (collision.gameObject.CompareTag("enemyMeleeRadius"))
        {
            currentHealth -= damageAmount;
            healthBarScript.setHealth(currentHealth); //
            Debug.Log(currentHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }




    // To Test Health Bar and hunger // remove once enemy attacks player
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth -= 10;
        }
     
    }

    private void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die"); // Make sure your Animator has a "Die" trigger.
        }

        gameObject.SetActive(false);
    }
}