using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public ParticleSystem runningDust;

    //Declare Variables
    private Rigidbody2D rb;

    public float speed;
    public float sprintSpeed;
    public float stamina;
    public float maxStamina = 100;
    public float staminaDrainSpeed = 20;
    public float staminaRegenSpeed = 20;
    public float dodgeStaminaCost = 33.333f;
    
    
    [SerializeField] float dodgeSpeed = 10f;
    [SerializeField] float dodgeDuration = 1f;
    [SerializeField] float dodgeCooldown = 0.5f;
    [SerializeField] TrailRenderer dashTrail;

    private Animator animator;
    private PlayerAnimationController animationController;
    private PlayerDeath playerDeath;

    bool isDodging;
    bool canDodge;   
    bool isSprinting;
    
    public PlayerDeath GetPlayerDeath() => playerDeath;
    public Feeding GetFeeding() => GetComponent<Feeding>();

    public GameObject playerMesh;
    public GameObject batMesh;
    private GameObject hud;
    public BoxCollider2D hitBox;
    public StaminaBar staminaBarSlider;


    //bool canBatForm;
    //bool inBatForm;
    // [SerializeField] float batFormSpeed = 10f;

    private void Awake()
    {
        /*else if (SceneManager.GetActiveScene().name== )
        {
            
            AudioManager.Manager.PlayMusic("LevelMusic");
            AudioManager.Manager.StopMusic("Spawn");
        }*/
        
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Spawn")
        {
            AudioManager.Manager.StopMusic("LevelMusic");
            AudioManager.Manager.PlayMusic("Spawn");

        }
        else
        {
            AudioManager.Manager.StopMusic("Spawn");
            AudioManager.Manager.PlayMusic("LevelMusic");
        }
        
        rb = GetComponent<Rigidbody2D>();

        canDodge = true;
        
        //canBatForm = true;
        //playerMesh.SetActive(true);
        //batMesh.SetActive(false);

        hitBox = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        animationController = GetComponent<PlayerAnimationController>();
        playerDeath = GetComponent<PlayerDeath>();
        
        stamina = maxStamina;
        staminaBarSlider.SetStamina(stamina);
    }

    void Update()
    {
        

        if (stamina != 100 && isSprinting == false)
        {
            StartCoroutine(StaminaRegen());
        }



        // Check if the player is not dead
        if (!playerDeath.GetIsDead())
        {
            // Handle movement if not dead
            HandleLivingMovement();
        }
        else
        {
            // Stop player movement after death
            StopPlayerMovement();
        }

        void HandleLivingMovement()
        {
            // Check if currently dodging
            if (isDodging)
            {
                // If dodging, do nothing
                return;
            }

            // Check for sprinting
            if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
            {
                HandleSprinting();
                CreateDust();
            }
            // Check for dodging
            else if (Input.GetKeyDown(KeyCode.Space) && canDodge && stamina > dodgeStaminaCost)
            {
                HandleDodge();
            }
            else
            {
                // Walk if not sprinting or dodging
                HandleWalking();
            }
        }

        void HandleSprinting()
        {
            isSprinting = true;
            staminaRegenSpeed = 0;

            // Calculate velocity for sprinting
            Vector2 sprintVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * sprintSpeed, Input.GetAxisRaw("Vertical") * sprintSpeed);
            rb.velocity = sprintVelocity;

            // Update stamina and UI
            stamina -= Time.deltaTime * staminaDrainSpeed;
            staminaBarSlider.SetStamina(stamina);
        }

        void HandleDodge()
        {
            // Play dodge sound
            AudioManager.Manager.PlaySFX("PlayerDodge");

            // Change animation state to Dash
            animationController.ChangeAnimationState(PlayerAnimationController.AnimationStates.Dash);

            // Perform dodge coroutine
            StartCoroutine(Dodge());

            // Update stamina and state
            stamina -= dodgeStaminaCost;
            isSprinting = true;
            staminaRegenSpeed = 20;
        }

        void HandleWalking()
        {
            // Calculate velocity for walking
            Vector2 walkVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
            rb.velocity = walkVelocity;

            // Update state
            isSprinting = false;
            staminaRegenSpeed = 20;
        }

        void StopPlayerMovement()
        {
            // Stop player movement after death
            rb.velocity = Vector2.zero;
        }
    }
    private IEnumerator Dodge() // dodge mechanic
    {
        canDodge = false;
        isDodging = true;


        Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputVector.Normalize();
        rb.velocity = inputVector * dodgeSpeed;
        dashTrail.emitting = true;
        yield return new WaitForSeconds(dodgeDuration);
        isDodging = false;
        dashTrail.emitting = false;
        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true;
        isSprinting = false;

    }


    private IEnumerator StaminaRegen()
    {
        isSprinting = false;
        yield return new WaitForSeconds(1.25f);
        if (stamina < 100)
        {
            stamina += Time.deltaTime * staminaRegenSpeed;
            stamina = Mathf.Clamp(stamina, 0f, 100f);
            staminaBarSlider.SetStamina(stamina);

        }
    }

    private void CreateDust()
    {
        runningDust.Play();
    }


    public void SetHUDReference(GameObject hudReference)
    {
        hud = hudReference;
        Debug.Log("HUD reference set in PlayerController!");
    }



    // Batform Code //


    //else if (Input.GetKeyDown(KeyCode.LeftControl) && canBatForm)
    //{
    //    StartCoroutine(BatForm());
    //}


    // private IEnumerator BatForm()
    // {

    //     canBatForm = false;
    //    speed = batFormSpeed;
    //    playerMesh.SetActive(false); // hides Player Skin
    //    batMesh.SetActive(true); // shows bat skin
    //    hitBox.size = new Vector2(0.5f, 0.5f);
    //   rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
    //  yield return new WaitForSeconds(5); // Bat Form duration
    //    speed = 3;
    //   playerMesh.SetActive(true); // shows player skin
    //   batMesh.SetActive(false); // hides bat skin
    //    hitBox.size = new Vector2(1.15f, 1.15f);
    //    yield return new WaitForSeconds(15);// Bat Form ability cool down
    //    canBatForm = true;


    // }

}



