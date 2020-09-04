using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpForce = 2.0f;
    public bool isGrounded;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;

    Rigidbody doggoRb;

    void Awake()
    {
        // Assigns the rigidbod component to a variable
        doggoRb = GetComponent<Rigidbody>();
    }

    // Keeps grounded true when on the ground / sidewalk (for jumping)
    void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag == ("Ground"))
        {
            isGrounded = true;
        }
    }

  
    // Update is called once per frame
    void FixedUpdate()
    {
        // Applies force to our jumps when pressing the spacebar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false; // Important to be considered grounded when touching walls
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;
           
        }

        // Controls fall speed, making our jumps more fluid feeling
        if (doggoRb.velocity.y < 0)
        {
            doggoRb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            isGrounded = false;
        }
        else if (doggoRb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        {
            doggoRb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            isGrounded = false;
        }
    }
}
