using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerHydrant : MonoBehaviour
{
    private float x = 2.806f;
    private float y = -2.648f;
    private float z;
    private float min = -2.65f;
    private float max = 2.65f;
    private Vector3 pos;

    void Start()
    {
        z = Random.Range(min, max);
        pos = new Vector3(x, y, z);
        Debug.Log(pos);
        transform.position = pos;
    }
}