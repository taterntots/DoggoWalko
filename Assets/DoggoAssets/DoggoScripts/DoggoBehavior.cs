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
    private ObstacleSpawner obstacleSpawnerEnemyRef;
    private ObstacleSpawner obstacleSpawnerTennisBallRef;
    private LevelSelector levelSelectorRef;
    private PlayerMovement playerMovementRef;

    public GameObject ratBoiBeat;

    public static bool walkingDog = true;
    public static bool isAnimating = false;
    public static bool noJump = false;
    public static bool isColliding = false;
    public static bool isTakingDamage = false;

    public static float animationDelay = 1.2f;

    public AudioSource audioSource;
    public AudioClip goodSound;
    public AudioClip badSound;
    public AudioClip barkSound;
    public AudioClip eatSound;
    public AudioClip splashSound;
    public AudioClip fightSound;
    public AudioClip deadSound;
    public AudioClip businessSound;
    public AudioClip peeingSound;
    public AudioClip fetchSound;
    public AudioClip walkerGrumbleSound;
    public AudioClip walkerPraiseSound;

    private float groundedX;
    private float groundedY;
    private float groundedZ;

    [Range(0.0f, 3.0f)] public float goodSoundVolume;
    [Range(0.0f, 3.0f)] public float badSoundVolume;

    // Start is called before the first frame update
    void Start()
    {
        // Grabs reference to the GameOver script
        gameOverRef = GameObject.FindWithTag("GameController").GetComponent<GameOver>();
        // Grabs reference to the LevelSpawner script
        levelSpawnerRef = GameObject.FindWithTag("GameController").GetComponent<LevelSpawner>();
        // Grabs reference to the MoveCamera script
        moveCameraRef = GameObject.FindWithTag("MainCamera").GetComponent<MoveCamera>();
        // Grabs reference to the ObstacleSpawnerEnemy script component
        obstacleSpawnerEnemyRef = GameObject.FindWithTag("ObstacleSpawnerEnemy").GetComponent<ObstacleSpawner>();
        // Grabs reference to the ObstacleSpawnerTennisBall script component
        obstacleSpawnerTennisBallRef = GameObject.FindWithTag("ObstacleSpawnerTennisBall").GetComponent<ObstacleSpawner>();
        // Grabs reference to the level int in the LevelSelector script
        levelSelectorRef = GameObject.FindWithTag("GameController").GetComponent<LevelSelector>();
        // Grabs reference to the level int in the PlayerMovement script
        playerMovementRef = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();

        // Turn off all sprites for dog animations
        TurnOffDoggoSprites();
        // Turn on Regular Dog Sprites
        gameObject.transform.Find("DoggoSpriteParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("DoggoSpriteParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

        // Keeps the "good" and "bad boi" animations from triggering at game start (essentially hides them)
        WalkerTextOff();
        // Keeps dog from wigging out if restarted after crossing finish line and doggo was mid animation
        noJump = false;
        isColliding = false;
        isAnimating = false;
    }

    void OnCollisionEnter(Collision other)
    {
        // If the object the dog is colliding with is considered "bad" behavior
        if (other.gameObject.tag == "Bad" && isTakingDamage == false && Invincibility.isSuper == false)
        {
            // If the doggo collides with a Ratboi
            if (other.gameObject.name == "RatBoi(Clone)")
            {
                // Triggers doggo fighting animation coroutine
                StartCoroutine("DoggoFighting");
                audioSource.PlayOneShot(fightSound, badSoundVolume);
                Destroy(other.gameObject);
            }
            // If the doggo collides with a Kitty
            if (other.gameObject.name == "Kitty(Clone)")
            {
                // Triggers doggo barking animation coroutine
                StartCoroutine("DoggoBarking");
                audioSource.PlayOneShot(barkSound, badSoundVolume);
            }
            // If the doggo collides with a ChocoBoi
            if (other.gameObject.name == "ChocoBoi(Clone)")
            {
                // Triggers doggo eating animation coroutine
                StartCoroutine("DoggoEating");
                audioSource.PlayOneShot(eatSound, badSoundVolume);
            }
            // If the doggo collides with a MailMan
            if (other.gameObject.name == "MailMan(Clone)")
            {
                // Triggers doggo barking animation coroutine
                StartCoroutine("DoggoBarking");
                audioSource.PlayOneShot(barkSound, badSoundVolume);
            }
            // If the doggo collides with a Car
            if (
                other.gameObject.name == "CarLeft(Blue)(Clone)" ||
                other.gameObject.name == "CarLeft(Green)(Clone)" ||
                other.gameObject.name == "CarLeft(Purple)(Clone)" ||
                other.gameObject.name == "CarLeft(Red)(Clone)" ||
                other.gameObject.name == "CarRight(Blue)(Clone)" ||
                other.gameObject.name == "CarRight(Green)(Clone)" ||
                other.gameObject.name == "CarRight(Purple)(Clone)" ||
                other.gameObject.name == "CarRight(Red)(Clone)")
            {
                // Triggers doggo dying animation coroutine
                StartCoroutine("DoggoDead");
                audioSource.PlayOneShot(deadSound, badSoundVolume);
            }
            // Swap sprites for enemy to the appropriate animation
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            other.gameObject.transform.GetChild(1).gameObject.SetActive(true);

            // Declare the object dead by changing tags to prevent multiple collisions
            other.gameObject.tag = "Dead";

            // Stops doggo from being able to jump while animation triggers
            noJump = true;

            // Turns off jump sprites (if collided while jumping)
            gameObject.transform.Find("DoggoJumpParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.Find("DoggoJumpParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;

            // Increase Bad Boi Points by one
            ScoreHolder.badBoiPoints++;
            // Lower the walker's attitude only if it's greater than -3 (health, essentially)
            if (walkerAttitude > -3)
            {
                walkerAttitude--;
            }

            // Triggers the "bad boi" text bubble upon collision with bad objects and then hides it after a set number of seconds
            GameObject.Find("BadBoiText").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("GoodBoiText").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("CheckPointText").GetComponent<SpriteRenderer>().enabled = false;
            Invoke("WalkerTextOff", 3f);
        }

        // Otherwise, ignores collisions with other enemies, water, or good objects (like trees or hydrants) while fighting
        else if (isTakingDamage == true && Invincibility.isSuper == false && (other.gameObject.tag == "Good" || other.gameObject.tag == "Bad" || other.gameObject.tag == "Water"))
        {
            Physics.IgnoreCollision(other.gameObject.GetComponent<CapsuleCollider>(), GetComponent<BoxCollider>());
            Physics.IgnoreCollision(other.gameObject.GetComponent<CapsuleCollider>(), GetComponent<CapsuleCollider>());
            Physics.IgnoreCollision(other.gameObject.GetComponent<BoxCollider>(), GetComponent<CapsuleCollider>());
            Physics.IgnoreCollision(other.gameObject.GetComponent<BoxCollider>(), GetComponent<BoxCollider>());
        }

        // After colliding with an enemy, ignore future collisions of Doggo with that object
        if (other.gameObject.tag == "Dead")
        {
            Physics.IgnoreCollision(other.gameObject.GetComponent<CapsuleCollider>(), GetComponent<BoxCollider>());
            Physics.IgnoreCollision(other.gameObject.GetComponent<CapsuleCollider>(), GetComponent<CapsuleCollider>());
            Physics.IgnoreCollision(other.gameObject.GetComponent<BoxCollider>(), GetComponent<CapsuleCollider>());
            Physics.IgnoreCollision(other.gameObject.GetComponent<BoxCollider>(), GetComponent<BoxCollider>());
        }

        // If the object being collided with is considered "good" behavior
        if (other.gameObject.tag == "Good" && isTakingDamage == false)
        {
            // Stops doggo from being able to jump while animation triggers
            noJump = true;

            // And if the object being collided with is a fire hydrant
            if (other.gameObject.name == "FireHydrant(Clone)")
            {
                // Triggers doggo peeing animation coroutine
                StartCoroutine("DoggoPeeing");
                audioSource.PlayOneShot(peeingSound, goodSoundVolume);
            }

            if (other.gameObject.name == "Tree(Clone)")
            {
                // Triggers doggo plopping animation coroutine
                StartCoroutine("DoggoBusiness");
                audioSource.PlayOneShot(businessSound, goodSoundVolume);
            }

            // Turns the box collider off to prevent multiple collisions
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            // Destroy object after a delay (when offscreen)
            Destroy(other.gameObject, 8);

            // Plays a little soundclip
            //audioSource.PlayOneShot(goodSound, goodSoundVolume);

            // Increase Good Boi Points by one
            ScoreHolder.goodBoiPoints++;
            // Raises the walker's attitude only if it's less than than 3 (health, essentially)
            if (walkerAttitude < 3)
            {
                walkerAttitude++;
            }

            // Triggers the "good boi" text bubble upon collision with good objects and then hides it after a set number of seconds
            GameObject.Find("GoodBoiText").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("BadBoiText").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("CheckPointText").GetComponent<SpriteRenderer>().enabled = false;
            Invoke("WalkerTextOff", 3f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Triggers dog playing in the water animation if stepping on water
        if (other.gameObject.tag == "Water" && isTakingDamage == false && Invincibility.isSuper == false)
        {
            // Stops doggo from being able to jump while animation triggers
            noJump = true;
            // Starts the animation for dog playing in water
            StartCoroutine("DoggoSplashing");
            // Destroys the box coliders so you can't trigger more than one (used multiple for shaping)
            Destroy(other.gameObject);
            // Plays the a little soundclip
            audioSource.PlayOneShot(splashSound, badSoundVolume);
            // Increase Bad Boi Points by one
            ScoreHolder.badBoiPoints++;
            // Lower the walker's attitude only if it's greater than -3 (health, essentially)
            if (walkerAttitude > -3)
            {
                walkerAttitude--;
            }
            // Triggers the "bad boi" text bubble upon collision with bad objects and then hides it after a set number of seconds
            GameObject.Find("BadBoiText").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("GoodBoiText").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("CheckPointText").GetComponent<SpriteRenderer>().enabled = false;
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
            // Increase the level (difficulty) by one (See LevelSelector script for details)
            levelSelectorRef.level += 1;

            // Destroys the checkpoint so you can't accidentally trigger more than once
            Destroy(other.gameObject);

            // Triggers the "CheckPoint" text bubble
            GameObject.Find("BadBoiText").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("GoodBoiText").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("CheckPointText").GetComponent<SpriteRenderer>().enabled = true;
            Invoke("WalkerTextOff", 3f);
        }
    }

    // Function for activating the good/bad boi text
    void WalkerTextOff()
    {
        GameObject.Find("GoodBoiText").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("BadBoiText").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("CheckPointText").GetComponent<SpriteRenderer>().enabled = false;
    }

    // Coroutine that swaps the dog sprite to the fighting animation
    IEnumerator DoggoFighting()
    {
        // Set isAnimating bool to true
        isAnimating = true;
        // Set isColliding bool to true
        isColliding = true;
        // Set isTakingDamage bool to true to prevent more than one enemy giving damage while taking a hit
        isTakingDamage = true;
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

            // Spawn a beat up ratBoi grounded and slightly behind the doggo
            groundedX = transform.position.x;
            groundedY = 0.8434051f;
            groundedZ = transform.position.z - 0.5f;

            Instantiate(ratBoiBeat, new Vector3(groundedX, groundedY, groundedZ), transform.rotation);
            //Instantiate(ratBoiBeat, transform.position - transform.forward * 0.5f, transform.rotation);

            noJump = false;
            isColliding = false;
            isTakingDamage = false;
            isAnimating = false;
        }
    }

    // Coroutine that swaps the dog sprite to the barking animation
    IEnumerator DoggoBarking()
    {
        // Set isAnimating bool to true
        isAnimating = true;
        // Set isColliding bool to true
        isColliding = true;
        // Set isTakingDamage bool to true to prevent more than one enemy giving damage while taking a hit
        isTakingDamage = true;
        // Turn off all sprites
        TurnOffDoggoSprites();
        // Turn on Barking Sprite
        gameObject.transform.Find("DoggoBarkParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("DoggoBarkParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

        // As long as the dog is barking
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
            isTakingDamage = false;
            isAnimating = false;
        }
    }

    // Coroutine that swaps the dog sprite to the eating animation
    IEnumerator DoggoEating()
    {
        // Set isAnimating bool to true
        isAnimating = true;
        // Set isColliding bool to true
        isColliding = true;
        // Set isTakingDamage bool to true to prevent more than one enemy giving damage while taking a hit
        isTakingDamage = true;
        // Turn off all sprites
        TurnOffDoggoSprites();
        // Turn on Eating Sprite
        gameObject.transform.Find("DoggoEatParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("DoggoEatParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

        // As long as the dog is eating
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
            isTakingDamage = false;
            isAnimating = false;
        }
    }

    // Coroutine that swaps the dog sprite to the dead animationw
    IEnumerator DoggoDead()
    {
        // Set isAnimating bool to true
        isAnimating = true;
        // Set isColliding bool to true
        isColliding = true;
        // Set isTakingDamage bool to true to prevent more than one enemy giving damage while taking a hit
        isTakingDamage = true;
        // Stop Doggo from being able to move
        playerMovementRef.doggoSpeed = 0;
        //gameObject.transform.Find("DoggoDeadParent").transform.GetComponent<RotateSprite2>().enabled = false;
        // Turn off all sprites
        TurnOffDoggoSprites();
        // Turn on Dying Sprite
        gameObject.transform.Find("DoggoDeadParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("DoggoDeadParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("DoggoDeadParent").GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("DoggoDeadParent").GetChild(3).GetComponent<SpriteRenderer>().enabled = true;

        // As long as the dog is eating
        while (isAnimating == true)
        {
            // Wait for a given amount of time before changing doggo sprite
            yield return new WaitForSeconds(2.5f);
            // Get Doggo moving again
            playerMovementRef.doggoSpeed = 2.5f;
            gameObject.transform.Find("DoggoDeadParent").transform.GetComponent<RotateSprite2>().enabled = true;
            // Turn off all sprites
            TurnOffDoggoSprites();
            // Return Doggo to nuetral standing
            gameObject.transform.Find("DoggoSpriteParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.Find("DoggoSpriteParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

            noJump = false;
            isColliding = false;
            isTakingDamage = false;
            isAnimating = false;
        }
    }

    // Coroutine that swaps the dog sprite to the peeing animation
    IEnumerator DoggoPeeing()
    {
        // Set isAnimating bool to true
        isAnimating = true;
        // Set isColliding bool to true
        isColliding = true;
        // Turn off all sprites
        TurnOffDoggoSprites();
        // Turn on Peeing Sprite
        if (Invincibility.isSuper == true)
        {
            gameObject.transform.Find("DoggoSuperParent").GetChild(4).GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.Find("DoggoSuperParent").GetChild(5).GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            gameObject.transform.Find("DoggoPeeingParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.Find("DoggoPeeingParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        }

        // As long as the dog is peeing
        while (isAnimating == true)
        {
            // Wait for a given amount of time before changing doggo sprite
            yield return new WaitForSeconds(animationDelay);
            // Turn off all sprites
            TurnOffDoggoSprites();
            // Return Doggo to nuetral standing, which differs a bit if still invincible
            if (Invincibility.isSuper == true)
            {
                gameObject.transform.Find("DoggoSuperParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                gameObject.transform.Find("DoggoSuperParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                gameObject.transform.Find("DoggoSpriteParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                gameObject.transform.Find("DoggoSpriteParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            }

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
        if (Invincibility.isSuper == true)
        {
            gameObject.transform.Find("DoggoSuperParent").GetChild(6).GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.Find("DoggoSuperParent").GetChild(7).GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            gameObject.transform.Find("DoggoBusinessParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.Find("DoggoBusinessParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        }

        // As long as the dog is doing his biz
        while (isAnimating == true)
        {
            // Wait for a given amount of time
            yield return new WaitForSeconds(animationDelay);
            // Turn off all sprites
            TurnOffDoggoSprites();
            // Return Doggo to nuetral standing, which differs a bit if still invincible
            if (Invincibility.isSuper == true)
            {
                gameObject.transform.Find("DoggoSuperParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                gameObject.transform.Find("DoggoSuperParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                gameObject.transform.Find("DoggoSpriteParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                gameObject.transform.Find("DoggoSpriteParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            }
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
        // Set isTaking Damage bool to true to prevent more than one enemy giving damage while taking a hit
        isTakingDamage = true;
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
            isTakingDamage = false;
            isAnimating = false;
        }
    }

    // Coroutine that swaps the dog sprite to the fetching animation
    public IEnumerator DoggoFetching()
    {
        // Turn on Fetching Sprite depending on whether super or not
        if (Invincibility.isSuper == false)
        {
            // Set isAnimating bool to true
            isAnimating = true;
            // Set isColliding bool to true
            isColliding = true;
            // Turn off all sprites
            TurnOffDoggoSprites();
            // Swap sprites for the fetching doggo animation
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
    }

    public void TurnOffDoggoSprites()
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
        gameObject.transform.Find("DoggoBarkParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoBarkParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoEatParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoEatParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoPetParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoPetParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoDeadParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoDeadParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoDeadParent").GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoDeadParent").GetChild(3).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoSuperParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoSuperParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoSuperParent").GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoSuperParent").GetChild(3).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoSuperParent").GetChild(4).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoSuperParent").GetChild(5).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoSuperParent").GetChild(6).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoSuperParent").GetChild(7).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoSuperParent").GetChild(8).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoSuperParent").GetChild(9).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoSuperParent").GetChild(10).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoSuperParent").GetChild(11).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("HeartParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("HeartParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("HeartParent").GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
    }
}