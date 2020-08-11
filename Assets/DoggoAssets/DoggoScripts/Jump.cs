using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    //public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;

    Rigidbody doggoRb;

    void Awake()
    {
        doggoRb = GetComponent<Rigidbody>();
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        //{
        //doggoRb.AddForce(jump * jumpForce, ForceMode.Impulse);
        //isGrounded = false;
        //}
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;
           
        }

        if (doggoRb.velocity.y < 0)
        {
            doggoRb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            isGrounded = false;
        }
        else if (doggoRb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        {
            doggoRb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            isGrounded = false;
        }
    }
}
