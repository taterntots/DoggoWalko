using System.Collections;
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
        StartCoroutine("LevelSpawn");

    }

    IEnumerator LevelSpawn()
    {
        //As long as the dog is alive, the level will continue to spawn terrain. Once the dog is dead, spawning should cease
        while (DoggoBehavior.walkingDog == true)
        {
            //the interval of time between building spawns
            yield return new WaitForSeconds(spawnTime);
            //chooses a random building from the building array
            Instantiate(randomBuilding(), new Vector3(-3.6497f, 3.5141f, zScenePos), transform.rotation);
            spawnCount++;
            //repositions six units on the z for the next spawn
            zScenePos += 6;
        }
    }

    void Update()
    {
        //plops a yellow house (home) after a determined number of randombuilding spawns
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

    //randomizes the building chosen between all prefabs in the array
    private GameObject randomBuilding()
    {
        return buildings[Random.Range(0, buildings.GetLength(0))];
    }
}
