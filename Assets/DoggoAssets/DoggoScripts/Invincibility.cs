using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    private int ballCount = 0;
    public int triggerSuper = 10;
    public int superTime = 10;
    private int leftOrRight = 1;

    public static bool isSuper = false;

    private DoggoBehavior doggoBehaviorRef;

    // Start is called before the first frame update
    void Start()
    {
        doggoBehaviorRef = GameObject.FindWithTag("Player").GetComponent<DoggoBehavior>();
        Debug.Log("ballcount:" + ballCount);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnCollisionEnter(Collision other)
    {
        // If the gameObject being collided with is a tennis Ball
        if (other.gameObject.name == "TennisBallHigh(Clone)" || other.gameObject.name == "TennisBallLow(Clone)")
        {
            // Increase the ball count
            ballCount += 1;
            Debug.Log("ballcount:" + ballCount);
            // Destroy the ball
            Destroy(other.gameObject);

            // If the number of balls caught reachers our trigger number, active the super bool and reset ball count
            if (ballCount == triggerSuper)
            {
                isSuper = true;
                ballCount = -100;
            }

            // If isSuper is triggered, become invincible
            if (isSuper == true)
            {
                // Triggers doggo fetching animation coroutine
                StartCoroutine("SuperStar");
            }
            else
            {
                // Triggers the regular doggo fetching animation coroutine
                StartCoroutine(doggoBehaviorRef.DoggoFetching());
            }
        }

        // When super is activated, hand collisions in the following ways
        if (isSuper == true)
        {
            // If the collision is with a bad object
            if (other.gameObject.tag == "Bad")
            {
                // Turn off enemy movement script so gravity activates
                other.gameObject.GetComponent<EnemyMovement>().enabled = false;
                // Rotate game object upside down
                other.gameObject.transform.Rotate(0, 0, 180);
                // Turn off colliders so they can fall throug walls and floors
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                // Destroy after set amount of time
                Destroy(other.gameObject, 4);

                // Apply force to the rigidbody to send them flying in different directions
                other.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 250);
                leftOrRight = Random.Range(1, 3);
                Debug.Log(leftOrRight);
                if (leftOrRight == 1 || leftOrRight == 2)
                {
                    other.gameObject.GetComponent<Rigidbody>().AddForce(transform.right * -100);
                }
                else
                {
                    other.gameObject.GetComponent<Rigidbody>().AddForce(transform.right * 100);
                }
            }
        }
    }

    // Coroutine that swaps the dog sprite to the peeing animation
    IEnumerator SuperStar()
    {
        // Set isAnimating bool to true
        DoggoBehavior.isAnimating = true;
        // Set isColliding bool to true
        DoggoBehavior.isColliding = true;
        // Turn off all sprites
        doggoBehaviorRef.TurnOffDoggoSprites();
        // Turn on Invincibility Sprites
        gameObject.transform.Find("DoggoSuperParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("DoggoSuperParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

        // As long as the doggo is supered up
        while (isSuper == true)
        {
            // Wait for a given amount of time before losing super power
            yield return new WaitForSeconds(superTime);
            // Turn off all sprites
            doggoBehaviorRef.TurnOffDoggoSprites();
            // Return Doggo to nuetral standing
            gameObject.transform.Find("DoggoSpriteParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.Find("DoggoSpriteParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

            // Reset the ball count
            ballCount = 0;

            // Reset a bunch of bools to turn doggo back to normal
            isSuper = false;
            DoggoBehavior.noJump = false;
            DoggoBehavior.isColliding = false;
            DoggoBehavior.isAnimating = false;
        }
    }
}
