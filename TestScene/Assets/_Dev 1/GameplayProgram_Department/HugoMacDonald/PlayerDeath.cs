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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected");
        if (collision.gameObject.CompareTag("Hunter"))
        {
            currentHealth -= damageAmount;
            healthBarScript.setHealth(currentHealth);
            Debug.Log(currentHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
        }
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
            animator.SetTrigger("Die"); // Make sure your Animator has a "Die" trigger.
        }
        gameObject.SetActive(false);
    }
}


