using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    private float dayToNightSpeed;
    private float nightToDaySpeed;

    private float dayToDuskSpeed;
    private float duskToNightSpeed;
    private float nightToDawnSpeed;
    private float dawnToDaySpeed;

    public float transitionSpeed = 0.4f;
    public Color dayColor;
    public Color dawnAndDuskColor;
    public Color nightColor;
    public float timeOfDay;
    public bool startGameDayTime = true;
    public bool dawn = false;
    public bool dusk = true;
    public bool night = false;
    public bool day = false;

    private MoonFade sunFadeRef;
    private MoonFade moonFadeRef;

    // Start is called before the first frame update
    void Start()
    {
        sunFadeRef = GameObject.Find("SunTimer").GetComponent<MoonFade>();
        moonFadeRef = GameObject.Find("MoonTimer").GetComponent<MoonFade>();

    }

    void Update()
    {
        // Constantly counts up in seconds
        timeOfDay += Time.deltaTime;

        // Determines whent to trigger shifts in sky color depending on time of day
        if (timeOfDay >= 50 && timeOfDay < 70)
        {
            startGameDayTime = false;
            dusk = true;
        }
        else if (timeOfDay >= 70 && timeOfDay < 120)
        {
            dusk = false;
            night = true;
            StartCoroutine(moonFadeRef.FadeImage(false));
            StartCoroutine(sunFadeRef.FadeImage(true));
        }
        else if (timeOfDay >= 120 && timeOfDay < 140)
        {
            night = false;
            dawn = true;
        }
        else if (timeOfDay >= 140)
        {
            dawn = false;
            day = true;
            timeOfDay = 0;
            StartCoroutine(moonFadeRef.FadeImage(true));
            StartCoroutine(sunFadeRef.FadeImage(false));
        }

        // Handles the actual transitions of daytime color via lerping
        if (dusk && startGameDayTime == false)
        {
            dawnToDaySpeed = 0;
            dayToDuskSpeed += Time.deltaTime;
            float t = dayToDuskSpeed * transitionSpeed;
            GetComponent<Camera>().backgroundColor = Color.Lerp(dayColor, dawnAndDuskColor, t);
        }
        else if (night && startGameDayTime == false)
        {
            dayToDuskSpeed = 0;
            duskToNightSpeed += Time.deltaTime;
            float t = duskToNightSpeed * transitionSpeed;
            GetComponent<Camera>().backgroundColor = Color.Lerp(dawnAndDuskColor, nightColor, t);
        }
        else if (dawn && startGameDayTime == false)
        {
            duskToNightSpeed = 0;
            nightToDawnSpeed += Time.deltaTime;
            float t = nightToDawnSpeed * transitionSpeed;
            GetComponent<Camera>().backgroundColor = Color.Lerp(nightColor, dawnAndDuskColor, t);
        }
        else if (day && startGameDayTime == false)
        {
            nightToDawnSpeed = 0;
            dawnToDaySpeed += Time.deltaTime;
            float t = dawnToDaySpeed * transitionSpeed;
            GetComponent<Camera>().backgroundColor = Color.Lerp(dawnAndDuskColor, dayColor, t);
        }
    }
}