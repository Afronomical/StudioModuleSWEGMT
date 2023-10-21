

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public int maxHealth = 100;
    
    public int currentHealth;
   
    private int damageAmount = 10;
    private float deathLevelDelay = 3f;
    private Animator animator;
    public HealthBarScript healthBarScript;
    public bool godMode;
    


    private void Start()
    {
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
        healthBarScript.SetMaxHealth(maxHealth);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected");
        if (collision.gameObject.CompareTag("Hunter")) //changed from enemyMelee to Hunter
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

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Collision detected");
    //    if (collision.gameObject.CompareTag("Hunter")) //changed from enemyMelee to Hunter
    //    {
    //        currentHealth -= damageAmount;
    //        healthBarScript.setHealth(currentHealth); //
    //        Debug.Log(currentHealth);
    //        if (currentHealth <= 0)
    //        {
    //            Die();
    //        }
    //    }
    //}




    // To Test Health Bar and hunger // remove once enemy attacks player
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    currentHealth -= 10;
        //}


        //if godmode enabled set health to 100 every tick so is esentailly immortal
        if (godMode)
        {
            currentHealth = maxHealth;
        }
     
    }

    private void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die"); // Make sure your Animator has a "Die" trigger.
        }

        gameObject.SetActive(false);
        //Instantiate(...);              //spawn "YOU DIED" ui
        Invoke("deathAfterDelay", deathLevelDelay);

    }
    private void deathAfterDelay()
    {
        SceneManager.LoadScene("MainMenu");

    }
}