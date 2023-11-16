using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    //Declare Variables
    private Rigidbody2D rb;

    public float speed;
    public float sprintSpeed;
    public float stamina;
    public float maxStamina = 100;
    public float staminaDrainSpeed = 20;
    public float staminaRegenSpeed = 20;
    
    
    [SerializeField] float dodgeSpeed = 10f;
    [SerializeField] float dodgeDuration = 1f;
    [SerializeField] float dodgeCooldown;
    [SerializeField] TrailRenderer dashTrail;

    private Animator animator;
    private PlayerAnimationController animationController;
    private PlayerDeath playerDeath;

    bool isDodging;
    bool canDodge;   
    bool isSprinting;
    


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
        AudioManager.Manager.PlayMusic("LevelMusic");
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
        AnimateMovement();

        if (stamina != 100 && isSprinting == false)
        {
            StartCoroutine(StaminaRegen());
        }

        

        if (!playerDeath.IsDead())
        {
            if (isDodging)
            {
                return;
            }

            if (Input.GetKey(KeyCode.LeftShift) && stamina > 0) // sprint
            {
               
                isSprinting = true;
                staminaRegenSpeed = 0;
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * sprintSpeed, Input.GetAxisRaw("Vertical") * sprintSpeed);
                stamina -= Time.deltaTime * staminaDrainSpeed;
                staminaBarSlider.SetStamina(stamina);

            }
            else if (Input.GetKeyDown(KeyCode.Space) && canDodge) // dodge
            {

                AudioManager.Manager.PlaySFX("PlayerDodge");
                animationController.ChangeAnimationState(PlayerAnimationController.AnimationStates.Dash);
                StartCoroutine(Dodge());
                isSprinting = false;
                staminaRegenSpeed = 20;

            }
            else // walk
            {
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
                isSprinting = false;
                staminaRegenSpeed = 20;
            }
        }
        else
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 0, Input.GetAxisRaw("Vertical") * 0); // stops player moving after death
        }
    }

    private void AnimateMovement()
    {
        Vector2 movementInput = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movementInput != Vector2.zero)
        {
            animator.SetFloat("MovementX", movementInput.x);
            animator.SetFloat("MovementY", movementInput.y);
        }

        if (!animationController.IsAnimationPlaying(animator, PlayerAnimationController.AnimationStates.SlashAttack) &&
            !animationController.IsAnimationPlaying(animator, PlayerAnimationController.AnimationStates.Dash) &&
            !animationController.IsAnimationPlaying(animator, PlayerAnimationController.AnimationStates.Hurt) &&
            !animationController.IsAnimationPlaying(animator, PlayerAnimationController.AnimationStates.Death) &&
            !animationController.IsAnimationPlaying(animator, PlayerAnimationController.AnimationStates.Feed))
        {
            if (!playerDeath.IsDead())
            {
                if (movementInput != Vector2.zero)
                {
                    animationController.ChangeAnimationState(PlayerAnimationController.AnimationStates.Walk);
                }
                else
                {
                    //AudioManager.Manager.PlayVFX("PlayerMove");
                    animationController.ChangeAnimationState(PlayerAnimationController.AnimationStates.Idle);
                }
            }
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

    }


    private IEnumerator StaminaRegen()
    {
        isSprinting = false;
        yield return new WaitForSeconds(1.25f);
        if (stamina < 100)
        {
            stamina += Time.deltaTime * staminaRegenSpeed;
            staminaBarSlider.SetStamina(stamina);
           
        }     
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



