using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public GameObject[] buildings;
    public GameObject home;
    public GameObject fireHydrant;

    private Timer timerRef;
    private bool spawnHome = false;
    private int spawnCount;
    private GameObject currentSpawn;
    public int homeSpawn;

    public float spawnTime;
    private float zHydrant;
    private int startingBuildingCount;
    private float zScenePos = -7.378f;
    //private float zScenePos = 28.378f;

    // Start is called before the first frame update
    void Start()
    {
        timerRef = GameObject.FindWithTag("GameController").GetComponent<Timer>();
        // Set our walkingDog bool back to true, otherwise the level spawner won't work when restarting after gameover
        DoggoBehavior.walkingDog = true;
        // Destroys any objects tagged as buildings (to get the randomizer working for the starting buildings)
        DestroyTheThing.DestroyAll("Building");
        // Calls the function to procedurally generate houses
        StartCoroutine("LevelSpawn");
    }

    IEnumerator LevelSpawn()
    {
        while (startingBuildingCount <= 6)
        {
            // Queue up the next random building to be spawned
            currentSpawn = randomBuilding();
            // Chooses a random building from the building array
            Instantiate(currentSpawn, new Vector3(-3.6497f, 3.5141f, zScenePos), transform.rotation);
            // Runs a function that decides what object to drop depending on building color
            DropObject();
            // Repositions six units on the z for the next building spawn
            zScenePos += 6;
            // Increase our counter to ensure only six buildings spawn at startup in total
            startingBuildingCount++;
        }

        // As long as the dog has more positive than negative actions, the level will continue to spawn terrain. Once a gameover has been reached, spawning should cease
        while (DoggoBehavior.walkingDog == true)
        {
            // The interval of time between building spawns
            yield return new WaitForSeconds(spawnTime);
            // Queue up the next random building to be spawned
            currentSpawn = randomBuilding();
            // Chooses a random building from the building array
            Instantiate(currentSpawn, new Vector3(-3.6497f, 3.5141f, zScenePos), transform.rotation);
            // Runs a function that decides what object to drop depending on building color
            DropObject();
            // Increase the count of buildings spawned so we can keep track of when to drop the doggo home later
            spawnCount++;
            // Repositions six units on the z for the next building spawn
            zScenePos += 6;
        }
    }

    void Update()
    {
        // Plops a yellow house (home) after a determined number of randombuilding spawns
        if (spawnCount == homeSpawn && spawnHome == false)
        {
            StopCoroutine("LevelSpawn");
            Instantiate(home, new Vector3(-3.6497f, 3.5141f, zScenePos), transform.rotation);
            zScenePos += 6;
            print("no place like home");
            spawnCount = 0;
            spawnHome = true;
        }

        if (spawnHome == true)
        {
            StartCoroutine("LevelSpawn");
            spawnHome = false;
        }
    }

    // Randomizes the building chosen between all prefabs in the array
    private GameObject randomBuilding()
    {
        return buildings[Random.Range(0, buildings.GetLength(0))];
    }

    // Decides what objects to drop depending on house color
    private void DropObject()
    {
        // If the house being spawned is red
        if (currentSpawn == buildings[2])
        {
            // Drop a fire hydrant randomly on the Z axis by the edge of the sidewalk
            zHydrant = Random.Range(zScenePos - 2.5f, zScenePos + 2.5f);
            Instantiate(fireHydrant, new Vector3(-0.864f, 0.908f, zHydrant), transform.rotation);
        }
    }
}
