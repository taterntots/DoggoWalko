using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogWalker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HappyWalker();
        NeutralWalker();
        AngryWalker();
    }

    void AngryWalker()
    {
        if (ScoreHolder.badBoiPoints > ScoreHolder.goodBoiPoints)
        {
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void NeutralWalker()
    { 
        if (ScoreHolder.badBoiPoints == ScoreHolder.goodBoiPoints)
        {
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void HappyWalker()
    {
        if (ScoreHolder.badBoiPoints < ScoreHolder.goodBoiPoints)
        {
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
