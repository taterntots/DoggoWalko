using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float frequency = 1.0f; // Speed of sine movement
    public float magnitude = 1.0f; // Size of sine movement, its the amplitude of the side curve
    public float speed = 1.0f; // Speed of the ememy/object
    public float twirlSpeed = 8; // Speed of the object's rotation

    public bool floating;
    public bool zigzag;
    public bool jumping;
    public bool twirling;

    private float minFreq = 1.0f;
    private float maxFreq = 2.0f;

    Vector3 axis;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        // Initialization for determining enemy movement (floating or zigzag)
        if (floating == true)
        {
            axis = transform.up;
        }
        else if (zigzag == true)
        {
            axis = transform.right;
        }

        // Initializes speed of enemy
        pos = new Vector3(0, 0, -speed);

        // Initializes a random frequency for the enemy
        SetRandomFrequency();
    }

    // Update is called once per frame
    void Update()
    {
        // If the enemy/obstacle has jumping toggled on
        if (jumping)
        {
            // Moves the object at a constant speed (different than other objects because gravity gets wonky otherwise)
            transform.Translate(Vector3.back * speed * Time.fixedDeltaTime, Space.Self);
        }
        else
        {
            // Moves the enemy towards the player. Allows for zigzagging using the frequency and magnitute variables
            GetComponent<Rigidbody>().velocity = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude; // y = A sin(B(x)) , here A is Amplitude, and axis * magnitude is acting as amplitude. Amplitude means the depth of the sin curve

            // Should make enemies look at the camera, but just turns them upside down and immobile
            //transform.LookAt(Camera.main.transform.position, -Vector3.up);
        }

        // If the enemy/obstacle has twirling toggled on
        if (twirling)
        {
            // Rotates the object a certain amount of degrees per second around y-axis
            transform.Rotate(0, twirlSpeed, 0 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ignores collisions with other enemies or good objects (like trees and hydrants)
        if (collision.gameObject.tag == "Good" || collision.gameObject.tag == "Bad")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider>(), GetComponent<BoxCollider>());
            Physics.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider>(), GetComponent<CapsuleCollider>());
        }
    }

    // Sets the random frequency between minTime and maxTime (used in obstaclespawner)
    public void SetRandomFrequency()
    {
        frequency = Random.Range(minFreq, maxFreq);
    }
}
