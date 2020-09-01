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
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;
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
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * doggoSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * doggoSpeed * Time.deltaTime;
            // Keeps the doggo sprites in their animation state on collision with enemies
            if (DoggoBehavior.isAnimating == false)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * doggoSpeed * Time.deltaTime;
            // Keeps the doggo sprites in their animation state on collision with enemies
            if (DoggoBehavior.isAnimating == false)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
