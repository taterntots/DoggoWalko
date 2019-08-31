using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObstacleSpawner : MonoBehaviour
{

    public GameObject badGuy;
    public float minTime;
    public float maxTime;

    //private GameObject badGuyClone;
    //public float moveSpeed;
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Spawn");
        spawnTime = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2);
        Instantiate(badGuy, transform.position, transform.rotation);
        Invoke("Spawn", spawnTime);
    }
}
