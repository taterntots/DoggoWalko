using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentDestroyer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
        {
        Destroy(other.gameObject);
        }
}
