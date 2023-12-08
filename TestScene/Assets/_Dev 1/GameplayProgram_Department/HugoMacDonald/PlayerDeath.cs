using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{

    public EndScreen deathscreenCanvas;
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
    
    private float parryTime = 0.2f;
    
    [HideInInspector] public bool recParryAttack;

    public Vector3 offset = new Vector3(0,5,0);

    private Animator animator;
    private PlayerAnimationController animationController;
    private bool isDead = false;
    private bool isDamaged = false;

    public BoxCollider2D[] boxColliders;
    public void SetIsDead(bool isDead) {  this.isDead = isDead; }
    private playerAttack PlayerAttack;
    private Feeding feeding;

    private void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
        healthBarScript.setMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        animationController = GetComponent<PlayerAnimationController>();
        boxColliders = GetComponents<BoxCollider2D>();
        PlayerAttack = GetComponent<playerAttack>();
        feeding = GetComponent<Feeding>();
    }

    private void Update()
    {
        if (healthBarScript == null)
            healthBarScript = FindAnyObjectByType<NewHealthBarScript>();

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
            Invoke(nameof(Die), animator.GetCurrentAnimatorClipInfo(0).Length);
            //if (!animationController.IsAnimationPlaying(animator, PlayerAnimationController.AnimationStates.Death))
            //{
            //    Die();
            //}
            

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

        StartCoroutine(delayedRemoveHealth(damage));

        //if (!gameObject.GetComponent<playerAttack>().parrying)
        //{
        //    if (!isInvincible)
        //    {
        //        isDamaged = true;
        //        AudioManager.Manager.PlaySFX("PlayerTakeDamage");
        //        currentHealth -= damage;
        //        healthBarScript.SetHealth(currentHealth);
        //        showFloatingText(damage);


        //        // Apply invincibility
        //        isInvincible = true;
        //        invincibilityTimer = invincibilityDuration;



        //    }
        //}
        //else
        //{
        //    Debug.Log("has parried");
        //}

    }

    IEnumerator delayedRemoveHealth(int dam)
    {
        if (!feeding.IsBiteIconActive())
            PlayerAttack.GetParrySword().SetActive(true);

        recParryAttack = true;
        yield return new WaitForSeconds(parryTime);
        PlayerAttack.GetParrySword().SetActive(false);
        if (gameObject.GetComponent<playerAttack>().parrying)
        {
            Debug.Log("parried");
            gameObject.GetComponent<playerAttack>().parrying = false;
            
        }
        else
        {
            if (!isInvincible)
            {

                isDamaged = true;
                AudioManager.Manager.PlaySFX("PlayerTakeDamage");
                currentHealth -= dam;
                healthBarScript.SetHealth(currentHealth);
                showFloatingText(dam);
                GameObject.FindWithTag("MainCamera").GetComponent<cameraFollow>().StartShake(0.3f, 1.5f);


                // Apply invincibility
                isInvincible = true;
                invincibilityTimer = invincibilityDuration;
            }
        }
        recParryAttack = false;
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
        currentHealth += 15; // You can adjust the value as needed
        healthBarScript.SetHealth(currentHealth);
    }

    private void Die()
    {
        //if (animator != null)
        //{
        //    //animator.SetTrigger("Die"); // Make sure your Animator has a "Die" trigger.
        //}

       // gameObject.SetActive(false);
        //Instantiate(...);              //spawn "YOU DIED" ui
        Invoke("deathAfterDelay", 1f);
    }
    private void deathAfterDelay()
    {
        AudioManager.Manager.StopMusic("LevelMusic");
        AudioManager.Manager.StopMusic("BossMusic");
        AudioManager.Manager.stopAllInGameSFX();

        CanvasManager.Instance.deathScreenCanvas.ShowUI();
        foreach (BoxCollider2D boxCollider in boxColliders)
        {
            if (boxCollider.isTrigger)
                boxCollider.enabled = false;
        }
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

        Destroy(go, 1f); 
    }

    public bool GetIsDead()
    {
        return isDead;
    }


}
