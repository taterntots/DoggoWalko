using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (DoggoBehavior.walkerAttitude < 0)
        {
            gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
            gameObject.transform.GetChild(1).GetComponent<Image>().enabled = false;
            gameObject.transform.GetChild(2).GetComponent<Image>().enabled = true;
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
