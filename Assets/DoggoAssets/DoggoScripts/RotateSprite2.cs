using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite2 : MonoBehaviour
{
    // Time taken to complete a full rotation
    [SerializeField] private float lerpTime = 0.5f;

    private bool isFlippingSprite;
    private float currentLerpTime;
    private int angle;

    void Start()
    {

    }

    private void Update()
    {
        // Keeps doggo rotating on key press without interruption via lerping
        if (isFlippingSprite)
        {
            // Increment timer once per frame
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            // It's lerping time!
            float perc = currentLerpTime / lerpTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, angle, transform.rotation.z), perc);

            // Lerp complete
            if (perc >= 1)
                isFlippingSprite = false;
        }
    }

    void FixedUpdate()
    {
        // Determines the degrees of rotation based on key pressed (triggers weven when animating with an object)
        if (DoggoBehavior.isAnimating == false || DoggoBehavior.isAnimating == true)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                PlayerRotator(-180);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                PlayerRotator(0);
            }
        }
    }

    // Function to determine the degrees rotated and activate lerping
    void PlayerRotator(int a)
    {
        if (!isFlippingSprite || currentLerpTime > (lerpTime / 2))
        {
            angle = a;
            isFlippingSprite = true;
            currentLerpTime = 0;
        }
    }
}
