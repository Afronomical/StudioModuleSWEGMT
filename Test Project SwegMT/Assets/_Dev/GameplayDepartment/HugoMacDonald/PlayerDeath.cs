using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private int damageAmount = 10; // example for test purpsoses
    private Animator animator; // Reference to the Animator component.

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>(); // Get the Animator component.
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected"); 
        if (collision.gameObject.CompareTag("enemyMeleeRadius"))
        {
            currentHealth -= damageAmount;
            Debug.Log(currentHealth);
            if (currentHealth <= 0)
            {
                Die();
            }

        }   
      
    }

    private void Die()
    {
        // Trigger the animation
        if (animator != null)
        {
            animator.SetTrigger("Die"); // "Die" is the name of the trigger parameter in the Animator. can be changed
        }

        // Disable the GameObject.
        gameObject.SetActive(false);
    }
}