using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTheThing : MonoBehaviour
{
    public static void DestroyAll(string tag)
    {
        GameObject[] whateverNeedsBlasting = GameObject.FindGameObjectsWithTag(tag);
        for(int i = 0; i < whateverNeedsBlasting.Length; i++)
        {
            Destroy(whateverNeedsBlasting[i]);
        }
    }
}
