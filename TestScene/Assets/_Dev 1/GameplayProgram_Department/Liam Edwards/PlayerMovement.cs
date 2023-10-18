using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Declare Variables 
    public float speed;
    public float sprintSpeed;
    public float jumpSpeed;
    private Rigidbody2D rb;
    //Update Method  

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerMovement();
    }



    public void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * sprintSpeed, Input.GetAxis("Vertical") * sprintSpeed);
        }
        else
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        }


    }

}


