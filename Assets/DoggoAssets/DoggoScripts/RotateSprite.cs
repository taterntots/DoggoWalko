using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    /*
    public float rotationSpeed = 0.5f;
    public bool isSpriteFlipped = false;

    // Possible solution that doesn't work great
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
    */

    private float timeToRotate = 0.5f;
    private bool isFacingLeft = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) && isFacingLeft == false)
        {
            //transform.localRotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
            StartCoroutine("RotatorLeft");
        }
        else if (Input.GetKey(KeyCode.D) && isFacingLeft)
        {
            StartCoroutine("RotatorRight");
        }
    }

    IEnumerator RotatorLeft()
    {
        Vector3 movementDirection = -Vector3.right;
        //Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.right, -transform.right);
        Quaternion currentRotation = transform.localRotation;
        for (float i = 0; i < 1.0f; i += Time.deltaTime / timeToRotate)
        {
            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, i);
            isFacingLeft = true;
            yield return null;
        }
    }

    IEnumerator RotatorRight()
    {
        Vector3 movementDirection = -Vector3.right;
        //Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
        Quaternion targetRotation = Quaternion.FromToRotation(-Vector3.right, transform.right);
        Quaternion currentRotation = transform.localRotation;
        for (float i = 0; i < 1.0f; i += Time.deltaTime / timeToRotate)
        {
            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, i);
            isFacingLeft = false;
            yield return null;
        }
    }
}