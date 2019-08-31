using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float enemySpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -enemySpeed);
        //should make enemies look at the camera, but just turns them upside down and immobile
        //transform.LookAt(Camera.main.transform.position, -Vector3.up);
    }
}
