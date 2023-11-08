using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Declare Variables
    private Rigidbody2D rb;

    public float speed;
    public float sprintSpeed;
    [SerializeField] float batFormSpeed = 10f;
    [SerializeField] float dodgeSpeed = 10f;
    [SerializeField] float dodgeDuration = 1f;
    [SerializeField] float dodgeCooldown;

    private Animator animator;
    private PlayerAnimationController animationController;
    private PlayerDeath playerDeath;

    bool isDodging;
    bool canDodge;
    bool canBatForm;
    bool inBatForm;
   
    public GameObject playerMesh;
    public GameObject batMesh;
    public BoxCollider2D hitBox;

    void Start()
    {
        AudioManager.Manager.PlayMusic("LevelMusic");
        rb = GetComponent<Rigidbody2D>();
        canDodge = true;
        canBatForm = true;
        playerMesh.SetActive(true);
        batMesh.SetActive(false);
        hitBox = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        animationController = GetComponent<PlayerAnimationController>();
        playerDeath = GetComponent<PlayerDeath>();
    }

    void Update()
    {
        AnimateMovement();

        if (!playerDeath.IsDead())
        {
            if (isDodging)
            {
                return;
            }
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * sprintSpeed, Input.GetAxisRaw("Vertical") * sprintSpeed);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && canDodge)
            {
                AudioManager.Manager.PlaySFX("PlayerDodge");
                animationController.ChangeAnimationState(PlayerAnimationController.AnimationStates.Dash);
                StartCoroutine(Dodge());
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl) && canBatForm)
            {
                StartCoroutine(BatForm());
            }
            else 
            {
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
            }
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


    private IEnumerator Dodge()
    {

        canDodge = false;
        isDodging = true;
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * dodgeSpeed, Input.GetAxisRaw("Vertical") * dodgeSpeed);
        yield return new WaitForSeconds(dodgeDuration);
        isDodging = false;

        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true;
        
    }

    private IEnumerator BatForm()
    {

        canBatForm = false;
        speed = batFormSpeed;
        playerMesh.SetActive(false); // hides Player Skin
        batMesh.SetActive(true); // shows bat skin
        hitBox.size = new Vector2(0.5f, 0.5f);
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
        yield return new WaitForSeconds(5); // Bat Form duration
        speed = 3;
        playerMesh.SetActive(true); // shows player skin
        batMesh.SetActive(false); // hides bat skin
        hitBox.size = new Vector2(1.15f, 1.15f);
        yield return new WaitForSeconds(15);// Bat Form ability cool down
        canBatForm = true;


    }

}

