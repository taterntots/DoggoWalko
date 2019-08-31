using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HeartsOff();
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void PetDoggo()
    {
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
        Invoke("HeartsOff", 3f);
    }

    void HeartsOff()
    {
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
    }
}