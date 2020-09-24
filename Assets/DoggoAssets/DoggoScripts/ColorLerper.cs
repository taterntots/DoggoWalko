using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerper : MonoBehaviour
{
    public float speed = 1.0f;
    public Color startColor;
    public Color endColor;
    public bool duskToDawn = false;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
       if (!duskToDawn)
        {
            float t = (Time.time - startTime) * speed;
            GetComponent<Camera>().backgroundColor = Color.Lerp(startColor, endColor, t);
        }
       else
        {
            float t = (Time.time - startTime) * speed;
            GetComponent<Camera>().backgroundColor = Color.Lerp(endColor, startColor, t);
        }
    }

    public void DayToNight()
    {
        float t = (Time.time - startTime) * speed;
        GetComponent<Camera>().backgroundColor = Color.Lerp(startColor, endColor, t);
    }
}
