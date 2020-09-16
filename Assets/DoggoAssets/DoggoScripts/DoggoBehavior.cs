using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoBehavior : MonoBehaviour
{
    [Range(-3, 3)] public static int walkerAttitude = 3;

    public GameObject plop;

    private GameOver gameOverRef;
    private LevelSpawner levelSpawnerRef;
    private MoveCamera moveCameraRef;
    private PlayerMovement playerMovementRef;
    private ObstacleSpawner obstacleSpawnerRef;

    public static bool walkingDog = true;
    public static bool isAnimating = false;
    public static bool noJump = false;
    public static bool isColliding = false;

    private float animationDelay = 1.0f;

    public AudioSource audioSource;
    public AudioClip goodSound;
    public AudioClip badSound;

    [Range(0.0f, 1.0f)] public float goodSoundVolume;
    [Range(0.0f, 1.0f)] public float badSoundVolume;

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
            // If it's specifically a plops
            if (other.gameObject.name == "DoggoPlops(Clone)")
            {
                // Trigger the DoggFighting Animation
                StartCoroutine("DoggoFighting");
                // Destroy the plops object after set period of time and make it kinetic so it doesn't fall through the floor
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                Destroy(other.gameObject, animationDelay);
            }

            // Stops doggo from being able to jump while animation triggers
            noJump = true;
            // Turns off jump sprites (if collided while jumping)
            gameObject.transform.Find("DoggoJumpParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.Find("DoggoJumpParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;

            // Triggers doggo fighting animation coroutine
            StartCoroutine("DoggoFighting");

            // Turns the box collider off to prevent multiple collisions
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.GetComponent<CapsuleCollider>().enabled = false;

            // Destroy the bad object with a delay
            //Destroy(other.gameObject, animationDelay);

            // Destroy the bad object immediately
            Destroy(other.gameObject);

            // Plays the a little soundclip
            audioSource.PlayOneShot(badSound, badSoundVolume);

            // Increase Bad Boi Points by one
            ScoreHolder.badBoiPoints++;
            // Lower the walker's attitude only if it's greater than -3 (health, essentially)
            if (walkerAttitude > -3)
            {
                walkerAttitude--;
                Debug.Log(walkerAttitude);
            }

            // Triggers the "bad boi" text bubble upon collision with bad objects and then hides it after a set number of seconds
            GameObject.Find("BadBoiText").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("GoodBoiText").GetComponent<SpriteRenderer>().enabled = false;
            Invoke("WalkerTextOff", 3f);
        }

        // If the object being collided with is considered "good" behavior
        if (other.gameObject.tag == "Good")
        {
            // Stops doggo from being able to jump while animation triggers
            noJump = true;

            // And if the object being collided with is a fire hydrant
            if (other.gameObject.name == "FireHydrant(Clone)")
            {
                // Triggers doggo peeing animation coroutine
                StartCoroutine("DoggoPeeing");
            }

            if (other.gameObject.name == "Tree(Clone)")
            {
                // Triggers doggo peeing animation coroutine
                StartCoroutine("DoggoBusiness");
                // Drop a plop at the tree (drops from too high atm)
                //Instantiate(plop, other.gameObject.transform.position - transform.forward * 1, transform.rotation);
            }

            // Turns the box collider off to prevent multiple collisions
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            if (other.gameObject.name == "TennisBall(Clone)")
            {
                // Triggers doggo fetching animation coroutine
                StartCoroutine("DoggoFetching");
                Destroy(other.gameObject);
            }

            // Destroy the good object after set period of time
            //Destroy(other.gameObject, animationDelay);

            // Plays the a little soundclip
            audioSource.PlayOneShot(goodSound, goodSoundVolume);

            // Increase Good Boi Points by one
            ScoreHolder.goodBoiPoints++;
            // Raises the walker's attitude only if it's less than than 3 (health, essentially)
            if (walkerAttitude < 3)
            {
                walkerAttitude++;
                Debug.Log(walkerAttitude);
            }

            // Triggers the "good boi" text bubble upon collision with good objects and then hides it after a set number of seconds
            GameObject.Find("GoodBoiText").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("BadBoiText").GetComponent<SpriteRenderer>().enabled = false;
            Invoke("WalkerTextOff", 3f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Triggers dog playing in the water animation if stepping on water
        if (other.gameObject.tag == "Water")
        {
            // Stops doggo from being able to jump while animation triggers
            noJump = true;
            // Starts the animation for dog playing in water
            StartCoroutine("DoggoSplashing");
            // Destroys the box coliders so you can't trigger more than one (used multiple for shaping)
            Destroy(other.gameObject);
            // Plays the a little soundclip
            audioSource.PlayOneShot(badSound, badSoundVolume);
            // Increase Bad Boi Points by one
            ScoreHolder.badBoiPoints++;
            // Lower the walker's attitude only if it's greater than -3 (health, essentially)
            if (walkerAttitude > -3)
            {
                walkerAttitude--;
                Debug.Log(walkerAttitude);
            }
            // Triggers the "bad boi" text bubble upon collision with bad objects and then hides it after a set number of seconds
            GameObject.Find("BadBoiText").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("GoodBoiText").GetComponent<SpriteRenderer>().enabled = false;
            Invoke("WalkerTextOff", 3f);
        }

        // Ends the game if you reach your home and have more badboi points than good
        if (other.gameObject.tag == "Checkpoint" && walkerAttitude < 0)
        {
            gameOverRef.EndGame();
        }

        // Continues the game when reaching home as a good boi, increasing difficulty
        else if (other.gameObject.tag == "Checkpoint" && walkerAttitude >= 0)
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
            if (obstacleSpawnerRef.maxTime > 0.4f)
            {
                obstacleSpawnerRef.maxTime -= 0.4f;
            }

            // Makes Obstacle Spawner move a little faster
            if (obstacleSpawnerRef.spawnerSpeed < 4.0f)
            {
                obstacleSpawnerRef.spawnerSpeed += 0.5f;
            }

            // Makes buildings spawn a bit faster
            if (levelSpawnerRef.spawnTime > 1.0f)
            {
                levelSpawnerRef.spawnTime -= 0.5f;
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
    
    // Coroutine that swaps the dog sprite to the fighting animation
    IEnumerator DoggoFighting()
    {
        // Set isAnimating bool to true
        isAnimating = true;
        // Set isColliding bool to true
        isColliding = true;
        // Turn off all sprites
        TurnOffDoggoSprites();
        // Turn on Fighting Sprite
        gameObject.transform.Find("DoggoFightingParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("DoggoFightingParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

        // As long as the dog is animating
        while (isAnimating == true)
        {
            // Wait for a given amount of time
            yield return new WaitForSeconds(animationDelay);
            // Turn off all sprites
            TurnOffDoggoSprites();
            // Return Doggo to nuetral standing
            gameObject.transform.Find("DoggoSpriteParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.Find("DoggoSpriteParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

            noJump = false;
            isColliding = false;
            isAnimating = false;
        }
    }

    // Coroutine that swaps the dog sprite to the peeing animation
    IEnumerator DoggoPeeing()
    {
        // Set isPeeing bool to true
        isAnimating = true;
        // Set isColliding bool to true
        isColliding = true;
        // Turn off all sprites
        TurnOffDoggoSprites();
        // Turn on Peeing Sprite
        gameObject.transform.Find("DoggoPeeingParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("DoggoPeeingParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

        // As long as the dog is peeing
        while (isAnimating == true)
        {
            // Wait for a given amount of time before changing doggo sprite
            yield return new WaitForSeconds(animationDelay);
            // Turn off all sprites
            TurnOffDoggoSprites();
            // Return Doggo to nuetral standing
            gameObject.transform.Find("DoggoSpriteParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.Find("DoggoSpriteParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

            noJump = false;
            isColliding = false;
            isAnimating = false;
        }
    }

    // Coroutine that swaps the dog sprite to the business animation
    IEnumerator DoggoBusiness()
    {
        // Set isAnimating bool to true
        isAnimating = true;
        // Set isColliding bool to true
        isColliding = true;
        // Turn off all sprites
        TurnOffDoggoSprites();
        // Turn on Business Sprite
        gameObject.transform.Find("DoggoBusinessParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("DoggoBusinessParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

        // As long as the dog is doing his biz
        while (isAnimating == true)
        {
            // Wait for a given amount of time
            yield return new WaitForSeconds(animationDelay);
            // Turn off all sprites
            TurnOffDoggoSprites();
            // Return Doggo to nuetral standing
            gameObject.transform.Find("DoggoSpriteParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.Find("DoggoSpriteParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            // Drop a plop behind you, no matter where in the world space you are
            Instantiate(plop, transform.position - transform.forward * 0.5f, transform.rotation);

            noJump = false;
            isColliding = false;
            isAnimating = false;
        }
    }

    // Coroutine that swaps the dog sprite to the splashing animation
    IEnumerator DoggoSplashing()
    {
        // Set isAnimating bool to true
        isAnimating = true;
        // Set isColliding bool to true
        isColliding = true;
        // Turn off all sprites
        TurnOffDoggoSprites();
        // Turn on Splashing Sprite
        gameObject.transform.Find("DoggoSplashParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("DoggoSplashParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

        // As long as the dog is animating
        while (isAnimating == true)
        {
            // Wait for a given amount of time
            yield return new WaitForSeconds(animationDelay);
            // Turn off all sprites
            TurnOffDoggoSprites();
            // Return Doggo to nuetral standing
            gameObject.transform.Find("DoggoSpriteParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.Find("DoggoSpriteParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

            noJump = false;
            isColliding = false;
            isAnimating = false;
        }
    }

    // Coroutine that swaps the dog sprite to the fetching animation
    IEnumerator DoggoFetching()
    {
        // Set isAnimating bool to true
        isAnimating = true;
        // Set isColliding bool to true
        isColliding = true;
        // Turn off all sprites
        TurnOffDoggoSprites();
        // Turn on Splashing Sprite
        gameObject.transform.Find("DoggoFetchParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("DoggoFetchParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

        // As long as the dog is animating
        while (isAnimating == true)
        {
            // Wait for a given amount of time
            yield return new WaitForSeconds(animationDelay);
            // Turn off all sprites
            TurnOffDoggoSprites();
            // Return Doggo to nuetral standing
            gameObject.transform.Find("DoggoSpriteParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.Find("DoggoSpriteParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

            noJump = false;
            isColliding = false;
            isAnimating = false;
        }
    }

    void TurnOffDoggoSprites()
    {
        gameObject.transform.Find("DoggoSpriteParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoSpriteParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoSpriteParent").GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoSpriteParent").GetChild(3).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoFightingParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoFightingParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoPeeingParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoPeeingParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoBusinessParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoBusinessParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoJumpParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoJumpParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoSplashParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoSplashParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoFetchParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoFetchParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
    }
}