using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody doggoRb;
    private DoggoBehavior doggoBehaviorRef;

    public float doggoSpeed = 2.5f;
    public static float doggoAutoSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Define some components / grab references
        doggoRb = GetComponent<Rigidbody>();
        doggoBehaviorRef = GameObject.FindWithTag("Player").GetComponent<DoggoBehavior>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Allows for movement of the doggo
        ControlDoggo();
    }

    void ControlDoggo()
    {
        // Moves the doggo at a constant pace alongside the camera
        transform.Translate(Vector3.forward * doggoAutoSpeed * Time.fixedDeltaTime, Space.Self);

        // Controls for moving the dog
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.forward * doggoSpeed * Time.deltaTime;

            // Swaps to DoggoBack Sprite when moving upwards
            if (DoggoBehavior.isAnimating == false)
            {
                if (Invincibility.isSuper == true)
                {
                    GameObject.Find("DoggoSuperSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoSuperFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoSuperBackSprite").GetComponent<SpriteRenderer>().enabled = true;
                    GameObject.Find("DoggoSuperBackFlipSprite").GetComponent<SpriteRenderer>().enabled = true;

                    GameObject.Find("DoggoSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoBackSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoBackFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    GameObject.Find("DoggoSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoBackSprite").GetComponent<SpriteRenderer>().enabled = true;
                    GameObject.Find("DoggoBackFlipSprite").GetComponent<SpriteRenderer>().enabled = true;

                    GameObject.Find("DoggoSuperSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoSuperFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoSuperBackSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoSuperBackFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= transform.forward * doggoSpeed * Time.deltaTime;

            // Swaps to DoggoFront Sprite when moving backwards
            if (DoggoBehavior.isAnimating == false)
            {
                if (Invincibility.isSuper == true)
                {
                    GameObject.Find("DoggoSuperSprite").GetComponent<SpriteRenderer>().enabled = true;
                    GameObject.Find("DoggoSuperFlipSprite").GetComponent<SpriteRenderer>().enabled = true;
                    GameObject.Find("DoggoSuperBackSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoSuperBackFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    GameObject.Find("DoggoSprite").GetComponent<SpriteRenderer>().enabled = true;
                    GameObject.Find("DoggoFlipSprite").GetComponent<SpriteRenderer>().enabled = true;
                    GameObject.Find("DoggoBackSprite").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("DoggoBackFlipSprite").GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * doggoSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= transform.right * doggoSpeed * Time.deltaTime;
        }
    }
}
