using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Declare Variables 
    public float walkSpeed = 4f; // Variable to store the walking speed
    public float sprintSpeed = 2.25f; // Variable to store the srint speed

    //Update Method  
    void Update()
    {
        PlayerMovement();
    }



    public void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * walkSpeed; // Horizontal input multiplied with Time.deltaTime and walkSpeed to get the horizontal movement.
        float vertical = Input.GetAxis("Vertical") * Time.deltaTime * walkSpeed; // Vertical input multiplied with Time.deltaTime and walkSpeed to get the horizontal movement.


        if (Input.GetKey(KeyCode.LeftShift)) // Multiplies the horizontal and vertical variables by a sprintSpeed value. This will increase the speed of the character, simulating a sprint.
        {
            horizontal *= sprintSpeed;
            vertical *= sprintSpeed;
        }

        transform.Translate(horizontal, 0f, vertical); // Move the object along the x-axis by 'horizontal' units and along the z-axis by 'vertical' units, leaving the y-axis unchanged.
    }
}


