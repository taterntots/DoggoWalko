using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    //Spawn this object in the object spawner
    //public GameObject spawnObject;
    public GameObject[] spawnObjects;

    public float maxTime = 15;
    public float minTime = 3;

    //current time
    private float time;
    //The time to spawn the object
    private float spawnTime;

    void Start()
    {
        SetRandomTime();
        time = minTime;
    }

    void FixedUpdate()
    {
        //Counts up
        time += Time.deltaTime;

        //Check if it's the right time to spawn the object
        if (time >= spawnTime)
        {
            SpawnObject();
            SetRandomTime();
        }
    }

    //Spawns the object and resets the time
    void SpawnObject()
    {
        time = minTime;
        //Instantiate(spawnObject, transform.position, spawnObject.transform.rotation);
        Instantiate(randomObjects(), transform.position, randomObjects().transform.rotation);
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