using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogWalker : MonoBehaviour
{

    private ColorLerper colorLerperRef;

    // Start is called before the first frame update
    void Start()
    {
        colorLerperRef = GameObject.FindWithTag("MainCamera").GetComponent<ColorLerper>();
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
        if (DoggoBehavior.walkerAttitude < 0)
        {
            gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
            gameObject.transform.GetChild(1).GetComponent<Image>().enabled = false;
            gameObject.transform.GetChild(2).GetComponent<Image>().enabled = true;

            colorLerperRef.DayToNight();
        }
    }

    void NeutralWalker()
    { 
        if (DoggoBehavior.walkerAttitude == 0)
        {
            gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
            gameObject.transform.GetChild(1).GetComponent<Image>().enabled = true;
            gameObject.transform.GetChild(2).GetComponent<Image>().enabled = false;
        }
    }

    void HappyWalker()
    {
        if (DoggoBehavior.walkerAttitude > 0)
        {
            gameObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
            gameObject.transform.GetChild(1).GetComponent<Image>().enabled = false;
            gameObject.transform.GetChild(2).GetComponent<Image>().enabled = false;
        }
    }
}
