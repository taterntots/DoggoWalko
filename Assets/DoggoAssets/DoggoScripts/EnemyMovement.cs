using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float frequency = 1.0f; // Speed of sine movement
    public float magnitude = 1.0f; // Size of sine movement, its the amplitude of the side curve
    public float speed = 1.0f; // Speed of the ememy/object

    Vector3 axis;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        // Intialization for zigzag parameters
        axis = transform.right;
        pos = new Vector3(0, 0, -speed);
    }

    // Update is called once per frame
    void Update()
    {
        // Moves the enemy towards the player. Allows for zigzagging using the frequency and magnitute variables
        GetComponent<Rigidbody>().velocity = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude; // y = A sin(B(x)) , here A is Amplitude, and axis * magnitude is acting as amplitude. Amplitude means the depth of the sin curve

        // Should make enemies look at the camera, but just turns them upside down and immobile
        //transform.LookAt(Camera.main.transform.position, -Vector3.up);
    }
}
