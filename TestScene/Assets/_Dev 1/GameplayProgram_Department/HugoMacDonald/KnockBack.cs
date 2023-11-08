using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private PlayerDeath playerDeath;
    public float knockbackForce = 5.0f;
    public float knockbackDuration = 0.2f;
    private bool isKnockedBack = false;
    private Vector2 knockbackDirection;
    public float playerRadius;
    private SpriteRenderer playerRenderer;
    private Color originalColor; // Store the original player color

    private int previousHealth = 100; // Initialize to a value

    private Transform playerTransform; // Reference to the player's transform

    private void Start()
    {
        playerDeath = GetComponent<PlayerDeath>();
        if (playerDeath == null)
        {
            Debug.LogError("PlayerDeath script not found on the same GameObject.");
        }

        playerRenderer = GetComponent<SpriteRenderer>();
        originalColor = playerRenderer.color;
    }

    private void Update()
    {
        if (isKnockedBack)
        {
            transform.position += (Vector3)knockbackDirection * knockbackForce * Time.deltaTime;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, playerRadius);

        if (colliders.Length > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Hunter") || collider.CompareTag("Villager") || collider.CompareTag("Projectile"))
                {
                    if (playerDeath != null && playerDeath.currentHealth < previousHealth)
                    {
                        ApplyKnockback(collider);

                        // Update the previous health to the current health
                        previousHealth = playerDeath.currentHealth;
                    }
                }
            }
        }
    }

    private void ApplyKnockback(Collider2D collider)
    {
        if (!isKnockedBack)
        {
            Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 attackerPosition = new Vector2(collider.transform.position.x, collider.transform.position.y);
            knockbackDirection = (playerPosition - attackerPosition).normalized;
            isKnockedBack = true;

            // Change the player's color to red.
            playerRenderer.color = Color.red;

            // Reset the knockback and player color after a specified duration.
            StartCoroutine(ResetKnockback());
        }
    }

    private IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(knockbackDuration);

        // Reset the player color to the original color.
        playerRenderer.color = originalColor;
        isKnockedBack = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerRadius);
    }
}