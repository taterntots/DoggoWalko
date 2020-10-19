using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDog : MonoBehaviour
{
    private DoggoBehavior doggoBehaviorRef;

    // Start is called before the first frame update
    void Start()
    {
        // Grabs referene to the DoggoBehavior script
        doggoBehaviorRef = GameObject.FindWithTag("Player").GetComponent<DoggoBehavior>();
        // Makes sure hearts are turned off at start
        HeartsOff();
        // Turn on regular Dog Sprite
        gameObject.transform.Find("DoggoSpriteParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("DoggoSpriteParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
    }

    public void PetDoggo()
    {
        StartCoroutine("PetDoggoz");
    }

    // Coroutine that swaps the dog sprite to the fetching animation
    public IEnumerator PetDoggoz()
    {
        // Turn on heart sprites
        HeartsOn();

        // Wait for a given amount of time
        yield return new WaitForSeconds(3);

        // Turn off heart sprites
        HeartsOff();
    }

    // Function that turns off the heart sprites
    void HeartsOff()
    {
        // Turn off hearts
        gameObject.transform.Find("HeartParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("HeartParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("HeartParent").GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
        // Return Dog Sprite to normal
        gameObject.transform.Find("DoggoSpriteParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("DoggoSpriteParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("DoggoPetParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoPetParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
    }

    // Function that turns on the heart sprites
    void HeartsOn()
    {
        // Turn on hearts
        gameObject.transform.Find("HeartParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("HeartParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("HeartParent").GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
        // Turn on Pet Dog Sprite
        gameObject.transform.Find("DoggoSpriteParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoSpriteParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("DoggoPetParent").GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("DoggoPetParent").GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
    }
}