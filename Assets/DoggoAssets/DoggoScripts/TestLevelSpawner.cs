using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelSpawner : MonoBehaviour
{
    public GameObject[] buildings;
    public float spawnTime;

    public float waitTime = 0.1f;
    private float zScenePos = 28.378f;

    private GameObject getBuilding()
    {
        return buildings[Random.Range(0, buildings.Length)];
    }

    void Start()
    {
        //Spawn();
        StartCoroutine("LevelSpawn");
    }

    //void Spawn()
    //{
        //Instantiate(buildings[Random.Range(0, buildings.GetLength(0))], transform.position, Quaternion.identity);
        //Invoke("Spawn", spawnTime);
    //}

    IEnumerator LevelSpawn()
    {
        //As long as the dog is alive, the level will continue to spawn terrain. Once the dog is dead, spawning should cease
        while (DoggoBehavior.walkingDog == true)
        {
            //Instantly builds out the next three house tiles. Enumerate to get it to spawn one at a time as camera draws near?
            yield return new WaitForSeconds(waitTime);
            Instantiate(buildings[Random.Range(0, buildings.GetLength(0))], new Vector3(-3.6497f, 3.5141f, zScenePos), transform.rotation);
            zScenePos += 6;
        }
    }
}
