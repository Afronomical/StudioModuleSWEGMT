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
    bool isDodging;
    bool canDodge;
    bool canBatForm;
    bool inBatForm;
   
    public GameObject playerMesh;
    public GameObject batMesh;
    public BoxCollider2D hitBox;



    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        canDodge = true;
        canBatForm = true;
        playerMesh.SetActive(true);
        batMesh.SetActive(false);
        hitBox = GetComponent<BoxCollider2D>();

    }

    void Update()
    {

        if (isDodging)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * sprintSpeed, Input.GetAxis("Vertical") * sprintSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && canDodge)
        {
            StartCoroutine(Dodge());
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && canBatForm)
        {
            StartCoroutine(BatForm());
        }
        else 
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        }

    }




    private IEnumerator Dodge()
    {

        canDodge = false;
        isDodging = true;
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * dodgeSpeed, Input.GetAxis("Vertical") * dodgeSpeed);
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
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        yield return new WaitForSeconds(5); // Bat Form duration
        speed = 3;
        playerMesh.SetActive(true); // shows player skin
        batMesh.SetActive(false); // hides bat skin
        hitBox.size = new Vector2(1.15f, 1.15f);
        yield return new WaitForSeconds(15);// Bat Form ability cool down
        canBatForm = true;


    }

}

