using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTheThing : MonoBehaviour
{
    // Destroys all objects that have this tag
    public static void DestroyAll(string tag)
    {
        GameObject[] whateverNeedsBlasting = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject thing in whateverNeedsBlasting)
        {
            Destroy(thing);
        }
    }

    // Destroys the parent of any gameObjects that have this tag on a child object
    public static void DestroyAllParents(string tag)
    {
        GameObject[] whateverNeedsBlasting = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject thing in whateverNeedsBlasting)
        {
            Destroy(thing.transform.parent.gameObject);
        }
    }
}
