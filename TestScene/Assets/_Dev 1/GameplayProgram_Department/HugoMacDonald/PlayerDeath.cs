using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private int damageAmount = 10;
    private Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
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
        if (animator != null)
        {
            animator.SetTrigger("Die"); // Make sure your Animator has a "Die" trigger.
        }

        gameObject.SetActive(false);
    }
}