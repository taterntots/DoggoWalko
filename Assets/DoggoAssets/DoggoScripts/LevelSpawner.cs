using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public GameObject[] buildings;
    public GameObject home;
    public GameObject crossWalk;
    public GameObject fireHydrant;
    public GameObject tree;
    private GameObject currentSpawn;

    private Timer timerRef;
    private bool spawnHome = false;
    private int homeSpawnCount = 6;
    private bool spawnCrossWalk = false;
    private int crossWalkCount = 6;
    public int homeSpawn;
    public int crossWalkSpawn;

    public float spawnTime;
    private float zBuildingObject;
    private int startingBuildingCount;
    private float zScenePos = -1.378f;

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
        // Quickly spawns the first six buildings on game bootup
        while (startingBuildingCount <= 5)
        {
            // Queue up the next random building to be spawned, as well as random side buildings for crosswalks
            currentSpawn = randomBuilding();
            // Chooses a random building from the building array, as well as side buildings
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
            // Queue up the next random building to be spawned, as well as side buildings
            currentSpawn = randomBuilding();
            // Chooses a random building from the building array, as well as side buildings
            Instantiate(currentSpawn, new Vector3(-3.6497f, 3.5141f, zScenePos), transform.rotation);
            // Runs a function that decides what object to drop depending on building color
            DropObject();
            // Increase the count of buildings spawned so we can keep track of when to drop the doggo home later
            homeSpawnCount++;
            // Increase the count of buildings spawned so we can keep track of when to drop the crosswalk later
            crossWalkCount++;
            // Repositions six units on the z for the next building spawn
            zScenePos += 6;
        }
    }

    void Update()
    {
        // Plops a yellow house (home) after a determined number of randombuilding spawns
        if (homeSpawnCount >= homeSpawn && spawnHome == false)
        {
            StopCoroutine("LevelSpawn");
            Instantiate(home, new Vector3(-3.6497f, 3.5141f, zScenePos), transform.rotation);
            zScenePos += 6;
            print("no place like home");
            homeSpawnCount = 0;
            spawnHome = true;
        }

        if (spawnHome == true)
        {
            StartCoroutine("LevelSpawn");
            spawnHome = false;
        }

        // Plops a CrossWalk after a determined number of randombuilding spawns
        if (crossWalkCount >= crossWalkSpawn && spawnCrossWalk == false)
        {
            StopCoroutine("LevelSpawn");
            Instantiate(crossWalk, new Vector3(-3.6497f, 3.5141f, zScenePos), transform.rotation);

            zScenePos += 18;
            print("watch out for cars!");
            crossWalkCount = 0;
            spawnCrossWalk = true;
        }

        if (spawnCrossWalk == true)
        {
            StartCoroutine("LevelSpawn");
            spawnCrossWalk = false;
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
            zBuildingObject = Random.Range(zScenePos - 2.5f, zScenePos + 2.5f);
            Instantiate(fireHydrant, new Vector3(-0.864f, 0.908f, zBuildingObject), transform.rotation);
        }
        else if (currentSpawn == buildings[1])
        {
            // Drop a tree randomly on the Z axis by the edge of the sidewalk
            zBuildingObject = Random.Range(zScenePos - 2.5f, zScenePos + 2.5f);
            Instantiate(tree, new Vector3(-0.943f, 1.825227f, zBuildingObject), transform.rotation);
        }
    }
}
