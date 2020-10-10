using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentDestroyer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
        {
        Destroy(other.gameObject);

        // Removes the counter for starting buildings, triggering the next building to spawn when the one behind is destroyed
        if (other.gameObject.tag == "Building")
        {
            LevelSpawner.currBuildingCount--;
        }
        }
}
