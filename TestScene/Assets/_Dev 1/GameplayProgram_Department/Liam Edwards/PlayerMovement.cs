using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Declare Variables 
    public float speed;
    public float sprintSpeed;
    private Rigidbody2D rb;
    [SerializeField] float dodgeSpeed = 10f;
    [SerializeField] float dodgeDuration = 1f;
    [SerializeField] float dodgeCooldown;
    bool isDodging;
    bool canDodge;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canDodge = true;
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

}

