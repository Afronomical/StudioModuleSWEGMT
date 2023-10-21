using System.Collections;
using System.Collections.Generic;


using UnityEngine;

public class PlayerHealthAndDamage : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private int damageAmount = 10;
    private Animator animator;
    public HealthBarScript healthBarScript;
    private bool isInvincible = false;
    private float invincibilityDuration = 1.0f;
    private float invincibilityTimer = 0.0f;
    private bool canTakeDamage = true;
    private float damageCooldown = 2.0f;
    public float playerRadius;
    public float knockbackForce = 5.0f;
    public float knockbackDuration = 0.2f;
    private bool isKnockedBack = false;
    private Vector2 knockbackDirection;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        healthBarScript.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        // Check if the invincibility timer has passed.
        if (isInvincible)
        {
            invincibilityTimer += Time.deltaTime;
            if (invincibilityTimer >= invincibilityDuration)
            {
                isInvincible = false;
                invincibilityTimer = 0.0f;
            }
        }

        // Check if the damage cooldown timer has passed.
        if (!canTakeDamage)
        {
            damageCooldown += Time.deltaTime;
            if (damageCooldown >= 2.0f)
            {
                canTakeDamage = true;
                damageCooldown = 0.0f;
            }
        }

        if (isKnockedBack)
        {
            transform.position += (Vector3)knockbackDirection * knockbackForce * Time.deltaTime;
        }

        // Check if the hunter and villager is inside players radius.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, playerRadius);

        if (colliders.Length > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Hunter") || collider.CompareTag("Villager"))
                {
                    // Check if the player is not currently invincible and can take damage.
                    if (!isInvincible && canTakeDamage)
                    {
                        currentHealth -= damageAmount;
                        healthBarScript.setHealth(currentHealth);
                        Debug.Log(currentHealth);

                        if (currentHealth <= 0)
                        {
                            Die();
                        }
                        else
                        {
                            // Apply knockback when damaged.
                            ApplyKnockback(collider);
                            // Set the player to be invincible for a duration.
                            isInvincible = true;
                            canTakeDamage = false;
                        }
                    }
                }
            }
        }
    }

    private void ApplyKnockback(Collider2D collider)
    {
        if (!isKnockedBack)
        {
            // Convert the player's position to Vector2 to match types.
            Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);

            // Calculate the knockback direction based on the player's position and the attacker's position.
            Vector2 attackerPosition = new Vector2(collider.transform.position.x, collider.transform.position.y);
            knockbackDirection = (playerPosition - attackerPosition).normalized;
            isKnockedBack = true;

            // Reset the knockback after a specified duration.
            StartCoroutine(ResetKnockback());
        }
    }



    // Coroutine to reset the knockback after a duration.
    private IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false;
    }

    private void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(transform.position, playerRadius);
    }
}