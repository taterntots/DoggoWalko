﻿using System.Collections;
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

    public float lessLeash;
    public float moreLeash;

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
        //Grabs reference to the GameOver script
        gameOverRef = GameObject.FindWithTag("GameController").GetComponent<GameOver>();
        //Grabs reference to the LevelSpawner script
        levelSpawnerRef = GameObject.FindWithTag("GameController").GetComponent<LevelSpawner>();
        //Grabs reference to the MoveCamera script
        moveCameraRef = GameObject.FindWithTag("MainCamera").GetComponent<MoveCamera>();
        //Grabs reference to the PlayerMovement script
        playerMovementRef = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        //Grabs reference to the ObstacleSpawner script
        obstacleSpawnerRef = GameObject.FindWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>();

        //Keeps the "good" and "bad boi" animations from triggering at game start (essentially hides them)
        WalkerTextOff();
        Debug.Log(levelSpawnerRef.spawnTime);
    }

  void OnCollisionEnter(Collision other)
    {
        //if the object the dog is colliding with is considered "bad" behavior, then destroy the game object (the dog) this script is attached to.
        if (other.gameObject.tag == "Bad")
        {
            //Destroy the bad object
            Destroy(other.gameObject);

            //Plays the a little soundclip
            audioSource.PlayOneShot(badSound, badSoundVolume);

            //Increase Bad Boi Points by one
            ScoreHolder.badBoiPoints++;

            //Triggers the "bad boi" text bubble upon collision with bad objects and then hides it after a set number of seconds
            GameObject.Find("BadBoiText").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("GoodBoiText").GetComponent<SpriteRenderer>().enabled = false;
            Invoke("WalkerTextOff", 3f);

            //code for reducing leash slack
            //GameObject.Find("DoggoBarrierFront").GetComponent<Transform>().transform.Translate(0, 0, lessLeash);
        }

        if (other.gameObject.tag == "Good")
        {
            //Destroy the good object
            Destroy(other.gameObject);

            //Plays the a little soundclip
            audioSource.PlayOneShot(goodSound, goodSoundVolume);

            //Increase Good Boi Points by one
            ScoreHolder.goodBoiPoints++;

            //Triggers the "good boi" text bubble upon collision with good objects and then hides it after a set number of seconds
            GameObject.Find("GoodBoiText").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("BadBoiText").GetComponent<SpriteRenderer>().enabled = false;
            Invoke("WalkerTextOff", 3f);

            //code for increasing leash slack
            //GameObject.Find("DoggoBarrierFront").GetComponent<Transform>().transform.Translate(0, 0, moreLeash);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Ends the game if you reach your home and have more badboi points than good
        if (other.gameObject.tag == "Checkpoint" && ScoreHolder.badBoiPoints > ScoreHolder.goodBoiPoints)
        {
            gameOverRef.EndGame();
        }
        //Continues the game when reaching home as a good boi, increasing difficulty
        else if (other.gameObject.tag == "Checkpoint" && ScoreHolder.badBoiPoints <= ScoreHolder.goodBoiPoints)
        {
            //Max out camera speed at 8
            if (moveCameraRef.cameraSpeed < 8)
            {
                //Makes the walker (aka the game) move faster
                moveCameraRef.cameraSpeed += 0.5f;
                //Makes the dog walk faster to match the cameraspeed
                playerMovementRef.doggoAutoSpeed += 0.5f;
            }
            //Makes enemies spawn more frequently, maxing out the spawn time between 0.4 and 1 second
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
                //Makes buildings spawn a bit faster
                levelSpawnerRef.spawnTime -= 0.5f;
            }
            //Destroys the checkpoint so you can't accidentally trigger more than once
            Destroy(other.gameObject);
        }
    }

    //function for activating the good/bad boi text
    void WalkerTextOff()
    {
        GameObject.Find("GoodBoiText").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("BadBoiText").GetComponent<SpriteRenderer>().enabled = false;
    }
}
