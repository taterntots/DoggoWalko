using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite2 : MonoBehaviour
{
    // Time taken to complete a full rotation during lerp
    [SerializeField] private float timeTakenDuringLerp = 0.3f;
    // The angle at which we want to reposition our sprite on rotate
    private int angle;
    // Bool to determine if we have completed a full sprite flip
    private bool isFlippingSprite;
    // The start and finish positions for our rotation
    private Quaternion currentRotation;
    private Quaternion targetRotation;
    // The Time.deltaTime value when we started the interpolation
    private float timeStartedLerping;
    // Determines whether the lerp should apply to the player or other objects
    public bool isPlayer;
    
    private void Update()
    {
        // Determines the degrees of rotation based on key pressed (triggers even when animating with an object)
        if (isPlayer && (DoggoBehavior.isAnimating == false || DoggoBehavior.isAnimating == true))
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                PlayerRotator(-180);
                StartLerping();
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                PlayerRotator(0);
                StartLerping();
            }
        }
    }

    void FixedUpdate()
    {
        // Keeps doggo rotating on key press without interruption via lerping
        if (isFlippingSprite)
        {
            // Helps us determine the percentage of timeTakenDuringLerp
            float timeSinceStarted = Time.time - timeStartedLerping;
            float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

            // Our actual lerp!
            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, percentageComplete);

            // Lerp complete if percentage gets to 100
            if (percentageComplete >= 1.0f)
            {
                isFlippingSprite = false;
            }
        }
    }

    // Function to begin a lerp and set some variables important to the lerp
    public void StartLerping()
    {
        isFlippingSprite = true;
        timeStartedLerping = Time.time;

        currentRotation = transform.rotation;
        targetRotation = Quaternion.Euler(transform.rotation.x, angle, transform.rotation.z);
    }

    // Function to determine the degrees rotated and activate lerping
    public void PlayerRotator(int a)
    {
        angle = a;
    }
}