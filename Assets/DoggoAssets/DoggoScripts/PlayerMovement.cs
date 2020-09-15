using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody doggoRb;

    public float doggoSpeed;
    public float doggoAutoSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Define some components
        doggoRb = GetComponent<Rigidbody>();

        // Turns off the spriter renderer for all dog animations (flipped dog, for example)
        gameObject.transform.Find("DoggoSpriteParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("DoggoSpriteParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("DoggoSpriteParent").GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoSpriteParent").GetChild(3).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("DoggoJumpSprite").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("DoggoJumpFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Allows for movement of the doggo
        ControlDoggo();
    }

    void ControlDoggo()
    {
        // Moves the doggo at a constant pace alongside the camera (currently breaks leash physics)
        transform.Translate(Vector3.forward * doggoAutoSpeed * Time.fixedDeltaTime, Space.Self);

        // Controls for moving the dog
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * doggoSpeed * Time.deltaTime;

            // Swaps to DoggoBack Sprite when moving upwards
            if (DoggoBehavior.isAnimating == false)
            {
                GameObject.Find("DoggoSprite").GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find("DoggoFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find("DoggoBackSprite").GetComponent<SpriteRenderer>().enabled = true;
                GameObject.Find("DoggoBackFlipSprite").GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * doggoSpeed * Time.deltaTime;

            // Swaps to DoggoFront Sprite when moving backwards
            if (DoggoBehavior.isAnimating == false)
            {
                GameObject.Find("DoggoSprite").GetComponent<SpriteRenderer>().enabled = true;
                GameObject.Find("DoggoFlipSprite").GetComponent<SpriteRenderer>().enabled = true;
                GameObject.Find("DoggoBackSprite").GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find("DoggoBackFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
            }
        }
      
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * doggoSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * doggoSpeed * Time.deltaTime;
        }
    }
}
