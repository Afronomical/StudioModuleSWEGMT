using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private KnockBack knockBack;
    public int maxHealth = 100;
    public int currentHealth;
    //private int damageAmount = 10;
    private Animator animator;
    public HealthBarScript healthBarScript;
    public bool godMode;
    //public float SetHealth;
    public bool isInvincible = false;
    public float invincibilityDuration = 2.0f; // Adjust the duration as needed
    private float invincibilityTimer = 0.0f;
    public GameObject floatingText; 




    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        healthBarScript.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (isInvincible)
        {
            // Decrement the invincibility timer
            invincibilityTimer -= Time.deltaTime;

            // Check if invincibility has expired
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
            }
        }

        if (currentHealth <= 0)
        {
            Die();
        }


        //if godmode enabled set health to 100 every tick so is esentailly immortal
        if (godMode)
        {
            currentHealth = maxHealth;
        }
        // If the player is invincible, return early to prevent damage and knockbacks
        if (isInvincible)
        {
            return;
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
        if (!isInvincible)
        {
            currentHealth -= damage;
            healthBarScript.setHealth(currentHealth);
            showFloatingText();
            

            // Apply invincibility
            isInvincible = true;
            invincibilityTimer = invincibilityDuration;

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

    void showFloatingText()
    {
        var go = Instantiate(floatingText, transform.position, Quaternion.identity, transform);
        go.GetComponentInChildren<TextMeshProUGUI>().text = currentHealth.ToString();
    }
}
