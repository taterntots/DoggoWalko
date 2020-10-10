using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // Create an array of objects we want to have spawnable
    public GameObject[] spawnObjects;
    private LevelSelector levelSelectorRef;

    public float maxTime;
    public float minTime;

    // Current time
    private float timer;
    // The time to spawn the object
    private float spawnTime;
    public bool isSpawning = true;
    // Completely stops all spawners
    public static bool stopSpawning = false;
    public bool isEnemySpawner = false;

    // Allows for adjustment to spawner speed
    Animator spawnerMovement;
    [Range(1.0f, 4.0f)] public float spawnerSpeed;

    void Start()
    {
        // Activates spawners
        isSpawning = true;
        stopSpawning = false;
        // Grab the Animator attached to the spawner
        spawnerMovement = gameObject.GetComponent<Animator>();
        // Grabs reference to the LevelSelector script component
        levelSelectorRef = GameObject.FindWithTag("GameController").GetComponent<LevelSelector>();
        // At the start of game, sets a random time between our max and min, and resets our timer to zero
        SetRandomTime();
        timer = 0;
    }

    void OnGUI()
    {
        // Create a Label in Game view for the Slider
        //GUI.Label(new Rect(0, 25, 40, 60), "Speed");
        // Create a horizontal Slider to control the speed of the Animator. Drag the slider to 1 for normal speed.
        //spawnerSpeed = GUI.HorizontalSlider(new Rect(45, 25, 200, 60), spawnerSpeed, 1.0F, 4.0F);
        // Make the speed of the Animator match the Slider value
        spawnerMovement.speed = spawnerSpeed;
    }

    void FixedUpdate()
    {
        // Counts up starting from zero
        timer += Time.deltaTime;

        // Check if it's the right time to spawn the object, only if the spawner is activated
        if (timer >= spawnTime && isSpawning == true && stopSpawning == false)
        {
            // Spawn an object and reset the timer to something random between our defined range
            SpawnObject();
            SetRandomTime();
        }
    }

    // Spawns the object and resets the time
    void SpawnObject()
    {
        // Picks a random obstacle/enemy to spawn
        Instantiate(RandomObject(), transform.position, RandomObject().transform.rotation);
        // Reset our timer back to zero
        timer = 0;
    }

    // Sets the random time between minTime and maxTime. Also sets random jumpforce for tennis balls
    void SetRandomTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

    // Randomizes the enemy chosen between all prefabs in the array
    private GameObject RandomObject()
    {
        // Only spawns rat enemy until first checkpoint
        if (levelSelectorRef.level == 1 && isEnemySpawner)
        {
            return spawnObjects[0];
        }
        // Randomizes between rat and kitty enemies after second checkpoint
        else if (levelSelectorRef.level == 2 && isEnemySpawner)
        {
            return spawnObjects[Random.Range(0, 2)];
        }
        // Randomizes between rat, kitty, and mailman enemies after third checkpoint
        else if (levelSelectorRef.level == 3 && isEnemySpawner)
        {
            return spawnObjects[Random.Range(0, 3)];
        }
        // Randomizes between all enemies after fourth checkpoint
        else
        {
            return spawnObjects[Random.Range(0, spawnObjects.GetLength(0))];
        }
    }
}