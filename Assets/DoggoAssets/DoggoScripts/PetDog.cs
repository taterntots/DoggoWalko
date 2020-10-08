using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Makes sure hearts are turned off at start
        HeartsOff();
    }

    public void PetDoggo()
    {
        // Set the heart container parent object to true (turns on heart sprites)
        gameObject.transform.gameObject.SetActive(true);
        // Turns off heart sprites after set amount of seconds
        Invoke("HeartsOff", 3f);
    }

    // Function that turns off the heart sprites
    void HeartsOff()
    {
        gameObject.transform.gameObject.SetActive(false);
    }
}