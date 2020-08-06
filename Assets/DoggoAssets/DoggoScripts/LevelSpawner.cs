﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public GameObject[] buildings;
    public GameObject home;

    private Timer timerRef;
    private bool spawnHome = false;
    private int spawnCount;
    public int homeSpawn;

    public float spawnTime;
    private float zScenePos = 28.378f;

    // Start is called before the first frame update
    void Start()
    {
        timerRef = GameObject.FindWithTag("GameController").GetComponent<Timer>();
        // Set our walkingDog bool back to true, otherwise the level spawner won't work when restarting after gameover
        DoggoBehavior.walkingDog = true;
        // Calls the function to procedurally generate houses
        StartCoroutine("LevelSpawn");

    }

    IEnumerator LevelSpawn()
    {
        // As long as the dog has more positive than negative actions, the level will continue to spawn terrain. Once a gameover has been reached, spawning should cease
        while (DoggoBehavior.walkingDog == true)
        {
            // The interval of time between building spawns
            yield return new WaitForSeconds(spawnTime);
            // Chooses a random building from the building array
            Instantiate(randomBuilding(), new Vector3(-3.6497f, 3.5141f, zScenePos), transform.rotation);
            spawnCount++;
            // Repositions six units on the z for the next spawn
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
}
