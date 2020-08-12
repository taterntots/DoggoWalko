using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody doggoRb;
    private SpriteRenderer mySpriteRenderer;

    public float doggoSpeed;
    public float doggoAutoSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Define some components
        doggoRb = GetComponent<Rigidbody>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        // Turns off the spriter renderer for flipped/left facing doggo
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
            GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (Input.GetKey(KeyCode.A) && mySpriteRenderer != null)
        {
            transform.position -= transform.right * doggoSpeed * Time.deltaTime;
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
