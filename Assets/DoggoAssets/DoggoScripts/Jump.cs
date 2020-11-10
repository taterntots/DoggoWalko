using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpForce;
    public bool isGrounded;

    public float fallMultiplier;
    public float lowJumpMultiplier;

    public bool isObstacle;
    public bool isPlayer;
    public float animationDelay;

    public AudioSource audioSource;
    public AudioClip jumpSound;
    [Range(0.0f, 3.0f)] public float jumpSoundVolume;

    Rigidbody Rb;
    public DoggoBehavior doggoBehaviorRef;

    void Start()
    {
        // Assigns some components and references to variables
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
        // If it's an obstacle (like the ball) or enemy, auto jump the moment the object touches the ground
        if (isObstacle && isGrounded)
        {
            Rb.velocity = Vector3.up * jumpForce;
        }

        // Applies force to player jumps when pressing the spacebar
        if (isPlayer && (Input.GetKeyDown(KeyCode.Space)) && isGrounded && DoggoBehavior.noJump == false)
        {
            StartCoroutine("DoggoJumping"); // Starts animation for jumping
            Rb.AddForce(transform.up * jumpForce * 100);
            // Plays a little soundclip
            audioSource.PlayOneShot(jumpSound, jumpSoundVolume);
        }

        // Controls fall speed, making jumps more fluid feeling
        if (Rb.velocity.y < 0)
        {
            Rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            //isGrounded = false;
        }
        else if (Rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        {
            Rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            isGrounded = false;
        }
    }

    // Coroutine that swaps the dog sprite to the fighting animation
    IEnumerator DoggoJumping()
    {
        // Set isAnimating bool to true
        DoggoBehavior.isAnimating = true;
        // Turn off all sprites
        doggoBehaviorRef.TurnOffDoggoSprites();
        //
        if (Invincibility.isSuper == true)
        {
            GameObject.Find("DoggoSuperJumpSprite").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("DoggoSuperJumpFlipSprite").GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GameObject.Find("DoggoJumpSprite").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("DoggoJumpFlipSprite").GetComponent<SpriteRenderer>().enabled = true;
        }

        // As long as the dog is animating
        while (DoggoBehavior.isAnimating == true)
        {
            // Wait for a given amount of time
            yield return new WaitForSeconds(animationDelay);
            // If the doggo is in super mode
            if (Invincibility.isSuper == true)
            {
                if (DoggoBehavior.isColliding == true)
                {
                    GameObject.Find("DoggoSuperJumpSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoSuperJumpFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoSuperSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoSuperFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoSuperBackSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoSuperBackFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    // Once the time has passed, return the doggo sprite to its original super form (done jumping)
                    GameObject.Find("DoggoSuperJumpSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoSuperJumpFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoSuperSprite").GetComponent<SpriteRenderer>().enabled = true;
                    GameObject.Find("DoggoSuperFlipSprite").GetComponent<SpriteRenderer>().enabled = true;
                    GameObject.Find("DoggoSuperBackSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoSuperBackFlipSprite").GetComponent<SpriteRenderer>().enabled = false;

                    DoggoBehavior.isAnimating = false;
                }
            }
            else
            {
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
}