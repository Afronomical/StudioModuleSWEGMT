using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    public float knockbackForce = 10.0f;
    public float knockbackDuration = 0.2f;
    private float finalKnockbackForce;

    private bool isKnockedBack = false;
    private Vector2 knockbackDirection;
    private SpriteRenderer enemyRenderer;
    private Color originalColor;

    private void Start()
    {
        enemyRenderer = GetComponent<SpriteRenderer>();
        originalColor = enemyRenderer.color;
    }

    private void Update()
    {
        if (isKnockedBack)
        {
            transform.position += (Vector3)knockbackDirection * finalKnockbackForce * Time.deltaTime;
        }
    }

    public void ApplyKnockback(Vector2 playerPosition, float forceMultiplier = 1.0f)
    {
        if (!isKnockedBack)
        {
            Vector2 attackerPosition = new Vector2(transform.position.x, transform.position.y);
            knockbackDirection = (attackerPosition - playerPosition).normalized;
            isKnockedBack = true;

            // Change the enemy's color to red.
            //enemyRenderer.color = Color.red;

            // Adjust the knockback force based on the forceMultiplier
            finalKnockbackForce = knockbackForce * forceMultiplier;

            // Apply knockback force to the enemy
          

            // Reset the knockback and enemy color after a specified duration.
            StartCoroutine(ResetKnockback());
        }
    }

    private IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(knockbackDuration);

        // Reset the enemy color to the original color.
        //enemyRenderer.color = originalColor;
        isKnockedBack = false;
    }
}