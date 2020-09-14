using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpForce = 2.0f;
    public bool isGrounded;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;

    public bool isObstacle;
    public bool isPlayer;

    Rigidbody Rb;

    void Awake()
    {
        // Assigns the rigidbod component to a variable
        Rb = GetComponent<Rigidbody>();
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
        // Runs jumping animation funtion when not grounded
        DoggoJumping();

        // If it's an obstacle (like the ball) or enemy, auto jump the moment the object touches the ground
        if (isObstacle && isGrounded)
        {
            isGrounded = false; // Important to be considered grounded when touching walls
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;
        }
        // Applies force to player jumps when pressing the spacebar
        if (isPlayer && Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false; // Important to be considered grounded when touching walls
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;        }

        // Controls fall speed, making jumps more fluid feeling
        if (Rb.velocity.y < 0)
        {
            Rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            isGrounded = false;
        }
        else if (Rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        {
            Rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            isGrounded = false;
        }
    }

    void DoggoJumping()
    {
        if (isGrounded == false)
        {
            // Turn off all sprites other than the jumping one
            gameObject.transform.Find("DoggoJumpParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.Find("DoggoJumpParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            //GameObject.Find("DoggoSpriteParent").SetActive(false);
            gameObject.transform.Find("DoggoSpriteParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.Find("DoggoSpriteParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.Find("DoggoSpriteParent").GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.Find("DoggoSpriteParent").GetChild(3).GetComponent<SpriteRenderer>().enabled = false;
            //gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            //gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
            //gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;

            //GameObject.Find("DoggoJumpParent").SetActive(true);
        }
        else if (isGrounded == true)
        {
            // When the doggo is grounded, return the doggo sprite to its original form (done jumping)
            gameObject.transform.Find("DoggoJumpParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.Find("DoggoJumpParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            //GameObject.Find("DoggoSpriteParent").SetActive(true);
            gameObject.transform.Find("DoggoSpriteParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.Find("DoggoSpriteParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.Find("DoggoSpriteParent").GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.Find("DoggoSpriteParent").GetChild(3).GetComponent<SpriteRenderer>().enabled = false;
            //gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            //gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
            //gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;

            //GameObject.Find("DoggoJumpParent").SetActive(false);
        }
    }
}
