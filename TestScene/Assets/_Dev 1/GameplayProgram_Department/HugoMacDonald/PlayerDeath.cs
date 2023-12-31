using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{

    public DeathScreen deathscreenCanvas;
    private KnockBack knockBack;
    public int maxHealth = 100;
    public int currentHealth;
    //private int damageAmount = 10;
    public NewHealthBarScript healthBarScript;
    public bool godMode;
    //public float SetHealth;
    public bool isInvincible = false;
    public float invincibilityDuration = 2.0f; // Adjust the duration as needed
    private float invincibilityTimer = 0.0f;
    public GameObject floatingText;
    public int feedHealAmount = 5;
    public int sunDamage = 5;
    

    public Vector3 offset = new Vector3(0,5,0);

    private Animator animator;
    private PlayerAnimationController animationController;
    private bool isDead = false;
    private bool isDamaged = false;

    private void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
        healthBarScript.setMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        animationController = GetComponent<PlayerAnimationController>();
    }

    private void Update()
    {
        IsDamaged();        
        

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
        
        if (currentHealth <= 0 && !isDead)
        {
            AudioManager.Manager.PlaySFX("PlayerDeath");
            isDead = true;
            animationController.ChangeAnimationState(PlayerAnimationController.AnimationStates.Death);
            Invoke("Die", animator.GetCurrentAnimatorClipInfo(0).Length);
            //if (!animationController.IsAnimationPlaying(animator, PlayerAnimationController.AnimationStates.Death))
            //{
            //}
            //Die();
            
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

        //prevents over healing
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
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
    
    public void RemoveHealth(int damage)
    {
        if (!isInvincible)
        {
            isDamaged = true;
            AudioManager.Manager.PlaySFX("PlayerTakeDamage");
            currentHealth -= damage;
            healthBarScript.SetHealth(currentHealth);
            showFloatingText(damage);
            

            // Apply invincibility
            isInvincible = true;
            invincibilityTimer = invincibilityDuration;

        }
    }

    private void IsDamaged()
    {
        if (isDamaged)
        {
            animationController.ChangeAnimationState(PlayerAnimationController.AnimationStates.Hurt);

            if (!animationController.IsAnimationPlaying(animator, PlayerAnimationController.AnimationStates.Hurt)) 
            {
                isDamaged = false;
            }
        }
    }

    public void FeedAttack()
    {
        // Implement your logic to increase health when the player is fed
        currentHealth += feedHealAmount; // You can adjust the value as needed
        healthBarScript.SetHealth(currentHealth);
    }

    private void Die()
    {
        //if (animator != null)
        //{
        //    //animator.SetTrigger("Die"); // Make sure your Animator has a "Die" trigger.
        //}

        gameObject.SetActive(false);
        //Instantiate(...);              //spawn "YOU DIED" ui
        Invoke("deathAfterDelay", 1);
    }
    private void deathAfterDelay()
    {
        AudioManager.Manager.StopMusic("LevelMusic");
        //deathscreenCanvas.ShowUI();
        AudioManager.Manager.PlayMusic("GameOver");
       
        //SceneManager.LoadScene("MainMenu");
    }

    public void SunRiseDamage() // Deals Damage While The Player Is In Sun Light
    {
        
        animationController.ChangeAnimationState(PlayerAnimationController.AnimationStates.Hurt);
        currentHealth = currentHealth - sunDamage;
        healthBarScript.SetHealth(currentHealth);
    }

    void showFloatingText(int damage)
    {

        Vector3 spawnPos = offset; 
        
        var go = Instantiate(floatingText, spawnPos, Quaternion.identity, transform);
        go.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
    }

    public bool IsDead()
    {
        return isDead;
    }
}
