using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody doggoRb;
    private SpriteRenderer mySpriteRenderer;
    public float doggoSpeed;
    public float doggoAutoSpeed = 1.25f;

    //public float flipRate;

    // Start is called before the first frame update
    void Start()
    {
        doggoRb = GetComponent<Rigidbody>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        //turns off the spriter renderer for flipped/left facing doggo
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ControlDoggo();
        
        //The code greyed out below is to get the sprite to face the camera at all times. Currently does not work.

        /*transform.localPosition = Vector3.zero;
        transform.LookAt(Camera.main.transform.position, Camera.main.transform.up);

        transform.position += Camera.main.transform.right * offset.x;
        transform.position += Camera.main.transform.up * offset.y;
        transform.position += Camera.main.transform.forward * offset.z;*/
    }

    void ControlDoggo()
    {
        //Moves the doggo at a constant pace alongside the camera (currently breaks leash physics)
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, doggoAutoSpeed);

        //Controls for moving the dog
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
            //mySpriteRenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A) && mySpriteRenderer != null)
        {
            transform.position -= transform.right * doggoSpeed * Time.deltaTime;
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = false;
            //mySpriteRenderer.flipX = true;
        }
        
    }
}
