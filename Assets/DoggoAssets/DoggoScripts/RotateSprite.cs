using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    public float rotationSpeed = 0.5f;
    public bool isSpriteFlipped = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Possible solution that doesn't work greata
    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Allows for rotation of the doggo
        //RotateDoggoSprite();
        if (Input.GetKeyDown(KeyCode.A) && !isSpriteFlipped)
        {
            isSpriteFlipped = true;
            StartCoroutine(RotateMe(Vector3.up * 180, 0.2f));
        }
        if (Input.GetKeyDown(KeyCode.D) && isSpriteFlipped)
        {
            isSpriteFlipped = false;
            StartCoroutine(RotateMe(Vector3.up * -180, 0.2f));
        }
    }

    void RotateDoggoSprite()
    {
        // Controls for moving the dog
        if (Input.GetKey(KeyCode.W))
        {
            // ROTATION CODE HERE
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // ROTATION CODE HERE
        }

        if (Input.GetKey(KeyCode.D))
        {
            // transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            // transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * rotationSpeed);           
        }
    }
}
