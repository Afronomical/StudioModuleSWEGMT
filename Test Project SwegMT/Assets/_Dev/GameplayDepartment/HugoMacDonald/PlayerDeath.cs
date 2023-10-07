using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    // temporary example health variables for test purposes, dedicated health script will be made in future
    public int maxHealth = 100;
    public int currentHealth;
    private int damageAmount = 10; // example for test purpsoses
    private Animator animator; // Reference to the Animator component. added as there will be an animation needed for death in future

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>(); // Get the Animator component.
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected"); 
        //detect collision with melee radius, maybe to be changed to trigger enter instead.
        if (collision.gameObject.CompareTag("enemyMeleeRadius"))
        {
            // remove dmage amount from current health
            currentHealth -= damageAmount;
            Debug.Log(currentHealth);
            if (currentHealth <= 0)
            {
                //call die function
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