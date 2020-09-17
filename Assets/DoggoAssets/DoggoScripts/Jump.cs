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
    public float animationDelay = 0.5f;

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
    void Update()
    {
        // Runs jumping animation function when not grounded
        //DoggoJumping();

        // If it's an obstacle (like the ball) or enemy, auto jump the moment the object touches the ground
        if (isObstacle && isGrounded)
        {
            isGrounded = false; // Important to be considered grounded when touching walls
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;
        }
        // Applies force to player jumps when pressing the spacebar or J Key
        if (isPlayer && (Input.GetKey(KeyCode.J) || Input.GetKeyDown(KeyCode.Space)) && isGrounded && DoggoBehavior.noJump == false)
        {
            isGrounded = false; // Important to be considered grounded when touching walls
            StartCoroutine("DoggoJumping2"); // Starts animation for jumping
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;
        }

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

    // Function not working properly. For some reason, it eventually bugs out with detecting the ground incorrectly
    void DoggoJumping()
    {
        if (isGrounded == false && DoggoBehavior.isAnimating == true)
        {
            // Turn off all sprites other than the jumping one
            GameObject.Find("DoggoJumpSprite").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("DoggoJumpFlipSprite").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("DoggoSprite").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("DoggoFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("DoggoBackSprite").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("DoggoBackFlipSprite").GetComponent<SpriteRenderer>().enabled = false;

            DoggoBehavior.isAnimating = false;
        }

        if (isGrounded == true)
        {
            GameObject.Find("DoggoJumpSprite").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("DoggoJumpFlipSprite").GetComponent<SpriteRenderer>().enabled = false;;
            GameObject.Find("DoggoSprite").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("DoggoFlipSprite").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("DoggoBackSprite").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("DoggoBackFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Coroutine that swaps the dog sprite to the fighting animation
    IEnumerator DoggoJumping2()
    {
        // Set isAnimating bool to true
        DoggoBehavior.isAnimating = true;
        // Turn off all sprites other than the jumping ones
        GameObject.Find("DoggoJumpSprite").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("DoggoJumpFlipSprite").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("DoggoSprite").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("DoggoFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("DoggoBackSprite").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("DoggoBackFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
       
        // As long as the dog is animating
        while (DoggoBehavior.isAnimating == true)
        {
            
            
            // Wait for a given amount of time
            yield return new WaitForSeconds(animationDelay);

            // If the doggo collides with a good or bad thing while in the air, turn off all sprites other than collision animation
            if (DoggoBehavior.isColliding == true)
            {
                GameObject.Find("DoggoJumpSprite").GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find("DoggoJumpFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find("DoggoSprite").GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find("DoggoFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find("DoggoBackSprite").GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find("DoggoBackFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                // Once the time has passed, return the doggo sprite to its original form (done jumping)
                GameObject.Find("DoggoJumpSprite").GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find("DoggoJumpFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find("DoggoSprite").GetComponent<SpriteRenderer>().enabled = true;
                GameObject.Find("DoggoFlipSprite").GetComponent<SpriteRenderer>().enabled = true;
                GameObject.Find("DoggoBackSprite").GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find("DoggoBackFlipSprite").GetComponent<SpriteRenderer>().enabled = false;

                DoggoBehavior.isAnimating = false;
            }
        }
    }
}
