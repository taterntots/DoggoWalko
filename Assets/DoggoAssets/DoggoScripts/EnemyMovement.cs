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
    public bool moveLeft;
    public bool moveRight;
    public bool leftAndRight;
    public bool isCar;

    private float minFreq = 1.0f;
    private float maxFreq = 2.0f;

    Vector3 axis;
    Vector3 pos;
    Vector3 pos2;

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
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.Self);
        }
        // If the enemy object has moveLeft toggled on
        else if (moveLeft)
        {
            // Moves the car at a constant speed left (different than other objects because gravity gets wonky otherwise)
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.Self);
        }
        // If the enemy object has moveRight toggled on
        else if (moveRight)
        {
            // Moves the car at a constant speed right (different than other objects because gravity gets wonky otherwise)
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
        }
        // If the enemy object has zigzag toggled on
        else if (zigzag)
        {
            // Moves the enemy towards the player. Allows for zigzagging using the frequency and magnitute variables
            GetComponent<Rigidbody>().velocity = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude; // y = A sin(B(x)) , here A is Amplitude, and axis * magnitude is acting as amplitude. Amplitude means the depth of the sin curve

            // Allows their sprites to rotate when at the apex of their zigzag
            pos2 = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude; // Grabs the everchanging x value of the vector

            if (pos2.x > 0)
            {
                gameObject.GetComponent<RotateSprite2>().PlayerRotator(180);
            }
            else if (pos2.x < 0)
            {
                gameObject.GetComponent<RotateSprite2>().PlayerRotator(0);
            }
        }
        else
        {
            // Otherwise, simply move the enemy in a straight line towards the player at a given speed
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.Self);

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
        if (collision.gameObject.tag == "Good" || collision.gameObject.tag == "Bad" || collision.gameObject.tag == "Dead")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider>(), GetComponent<BoxCollider>());
            Physics.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider>(), GetComponent<CapsuleCollider>());
            Physics.IgnoreCollision(collision.gameObject.GetComponent<CapsuleCollider>(), GetComponent<CapsuleCollider>());
        }
        // Ignores street colliders if gameobject is a car
        if (isCar && (collision.gameObject.tag == "StreetCollider" || collision.gameObject.tag == "BuildingCollider"))
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider>(), GetComponent<BoxCollider>());
            Physics.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider>(), GetComponent<CapsuleCollider>());
        }

        // Allows enemies to move from left to right on collision with walls within the confines of the gameplay area (stay on sidewalk)
        if (leftAndRight && collision.gameObject.tag == "BuildingCollider")
        {
            gameObject.GetComponent<RotateSprite2>().PlayerRotator(-180);

        }
        else if (leftAndRight && collision.gameObject.tag == "StreetCollider")
        {
            gameObject.GetComponent<RotateSprite2>().PlayerRotator(0);
        }
    }

    // Sets the random frequency between minTime and maxTime (used in obstaclespawner)
    public void SetRandomFrequency()
    {
        frequency = Random.Range(minFreq, maxFreq);
    }
}
