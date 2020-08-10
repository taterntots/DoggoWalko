using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    //Spawn this object in the object spawner
    //public GameObject spawnObject;
    public GameObject[] spawnObjects;

    public float maxTime;
    public float minTime;

    //current time
    private float timer;
    //The time to spawn the object
    private float spawnTime;

    void Start()
    {
        //At the start of game, sets a random time between our max and min, and resets our timer to zero
        SetRandomTime();
        timer = 0;
    }

    void FixedUpdate()
    {
        //Counts up starting from zero
        timer += Time.deltaTime;

        //Check if it's the right time to spawn the object
        if (timer >= spawnTime)
        {
            SpawnObject();
            SetRandomTime();
        }
    }

    //Spawns the object and resets the time
    void SpawnObject()
    {
        //Instantiate(spawnObject, transform.position, spawnObject.transform.rotation);
        Instantiate(randomObjects(), transform.position, randomObjects().transform.rotation);
        //Reset our timer back to zero
        timer = 0;
    }

    //Sets the random time between minTime and maxTime
    void SetRandomTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

    //randomizes the building chosen between all prefabs in the array
    private GameObject randomObjects()
    {
        return spawnObjects[Random.Range(0, spawnObjects.GetLength(0))];
    }
}