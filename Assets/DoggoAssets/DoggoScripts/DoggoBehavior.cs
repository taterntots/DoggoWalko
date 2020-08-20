using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoBehavior : MonoBehaviour
{
    private GameOver gameOverRef;
    private LevelSpawner levelSpawnerRef;
    private MoveCamera moveCameraRef;
    private PlayerMovement playerMovementRef;
    private ObstacleSpawner obstacleSpawnerRef;

    public static bool walkingDog = true;
    public static bool isPeeing = false;
    public static bool isAnimating = false;

    private float animationDelay = 1.0f;

    public AudioSource audioSource;
    public AudioClip goodSound;
    public AudioClip badSound;

    [Range(0.0f, 1.0f)] public float goodSoundVolume;
    [Range(0.0f, 1.0f)] public float badSoundVolume;

    //[System.NonSerialized] public int goodBoiPoints = 0;
    //[System.NonSerialized] public int badBoiPoints = 0;


    // Start is called before the first frame update
    void Start()
    {
        // Grabs reference to the GameOver script
        gameOverRef = GameObject.FindWithTag("GameController").GetComponent<GameOver>();
        // Grabs reference to the LevelSpawner script
        levelSpawnerRef = GameObject.FindWithTag("GameController").GetComponent<LevelSpawner>();
        // Grabs reference to the MoveCamera script
        moveCameraRef = GameObject.FindWithTag("MainCamera").GetComponent<MoveCamera>();
        // Grabs reference to the PlayerMovement script
        playerMovementRef = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        // Grabs reference to the ObstacleSpawner script
        obstacleSpawnerRef = GameObject.FindWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>();

        // Keeps the "good" and "bad boi" animations from triggering at game start (essentially hides them)
        WalkerTextOff();
    }

  void OnCollisionEnter(Collision other)
    {
        // If the object the dog is colliding with is considered "bad" behavior
        if (other.gameObject.tag == "Bad")
        {
            // Triggers doggo eating animation coroutine
            StartCoroutine("DoggoEating");

            // Turns the box collider off to prevent multiple collisions
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.GetComponent<CapsuleCollider>().enabled = false;

            // Destroy the bad object
            //Destroy(other.gameObject, animationDelay);

            // Plays the a little soundclip
            audioSource.PlayOneShot(badSound, badSoundVolume);

            // Increase Bad Boi Points by one
            ScoreHolder.badBoiPoints++;

            // Triggers the "bad boi" text bubble upon collision with bad objects and then hides it after a set number of seconds
            GameObject.Find("BadBoiText").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("GoodBoiText").GetComponent<SpriteRenderer>().enabled = false;
            Invoke("WalkerTextOff", 3f);
        }

        // If the object being collided with is considered "good" behavior
        if (other.gameObject.tag == "Good")
        {
            // And if the object being collided with is a fire hydrant
            if (other.gameObject.name == "FireHydrant(Clone)")
            {
                // Triggers doggo peeing animation coroutine
                StartCoroutine("DoggoPeeing");
            }

            if (other.gameObject.name == "Tree(Clone)")
            {
                // Triggers doggo peeing animation coroutine
                StartCoroutine("DoggoPlopping");
            }

            // Turns the box collider off to prevent multiple collisions
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            // Destroy the good object after set period of time
            Destroy(other.gameObject, animationDelay);

            // Plays the a little soundclip
            audioSource.PlayOneShot(goodSound, goodSoundVolume);

            // Increase Good Boi Points by one
            ScoreHolder.goodBoiPoints++;

            // Triggers the "good boi" text bubble upon collision with good objects and then hides it after a set number of seconds
            GameObject.Find("GoodBoiText").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("BadBoiText").GetComponent<SpriteRenderer>().enabled = false;
            Invoke("WalkerTextOff", 3f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Ends the game if you reach your home and have more badboi points than good
        if (other.gameObject.tag == "Checkpoint" && ScoreHolder.badBoiPoints > ScoreHolder.goodBoiPoints)
        {
            gameOverRef.EndGame();
        }

        // Continues the game when reaching home as a good boi, increasing difficulty
        else if (other.gameObject.tag == "Checkpoint" && ScoreHolder.badBoiPoints <= ScoreHolder.goodBoiPoints)
        {
            // Max out camera speed at 8
            if (moveCameraRef.cameraSpeed < 8)
            {
                // Makes the walker (aka the game) move faster
                moveCameraRef.cameraSpeed += 0.5f;
                // Makes the dog walk faster to match the cameraspeed
                playerMovementRef.doggoAutoSpeed += 0.5f;
            }

            // Makes enemies spawn more frequently, maxing out the spawn time between 0.4 and 1 second
            if (obstacleSpawnerRef.minTime > 0.4f)
            {
                obstacleSpawnerRef.minTime -= 0.2f;
            }
            if (obstacleSpawnerRef.maxTime > 1.0f)
            {
                obstacleSpawnerRef.maxTime -= 0.2f;
            }
            if (levelSpawnerRef.spawnTime > 0.5f)
            {
                // Makes buildings spawn a bit faster
                levelSpawnerRef.spawnTime -= 0.5f;
            }

            // Makes Obstacle Spawner move a little faster
            if (obstacleSpawnerRef.spawnerSpeed < 4.0f)
            {
                obstacleSpawnerRef.spawnerSpeed += 0.5f;
            }

            // Destroys the checkpoint so you can't accidentally trigger more than once
            Destroy(other.gameObject);
        }
    }

    // Function for activating the good/bad boi text
    void WalkerTextOff()
    {
        GameObject.Find("GoodBoiText").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("BadBoiText").GetComponent<SpriteRenderer>().enabled = false;
    }

    // Coroutine that swaps the dog sprite to the peeing animation
    IEnumerator DoggoPeeing()
    {
        // Set isPeeing bool to true
        isPeeing = true;
        // Turn off all sprites other than the peeing one
        GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;

        // As long as the dog is peeing
        while (isPeeing == true)
        {
            // Wait for a given amount of time
            yield return new WaitForSeconds(animationDelay);
            // Once the time has passed, return the doggo sprite to its original form (done peeing)
            GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;

            isPeeing = false;
        }
    }

    // Coroutine that swaps the dog sprite to the peeing animation
    IEnumerator DoggoPlopping()
    {
        // Set isPeeing bool to true
        isAnimating = true;
        // Turn off all sprites other than the plopping one
        GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = true;

        // As long as the dog is plopping
        while (isAnimating == true)
        {
            // Wait for a given amount of time
            yield return new WaitForSeconds(animationDelay);
            // Once the time has passed, return the doggo sprite to its original form (done peeing)
            GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;

            isAnimating = false;
        }
    }

    // Coroutine that swaps the dog sprite to the eating animation
    IEnumerator DoggoEating()
    {
        // Set isAnimating bool to true
        isAnimating = true;
        // Turn off all sprites other than the eating one
        GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;

        // As long as the dog is animating
        while (isAnimating == true)
        {
            // Wait for a given amount of time
            yield return new WaitForSeconds(animationDelay);
            // Once the time has passed, return the doggo sprite to its original form (done peeing)
            GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;
            isAnimating = false;
        }
    }
}