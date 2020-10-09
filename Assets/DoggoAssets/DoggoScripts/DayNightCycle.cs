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
    public static bool stopCounting = false;
    public static bool startGameDayTime = true;
    public bool dawn = false;
    public static bool dusk = true;
    public static bool night = false;
    public bool day = false;

    private FadeObject sunFadeRef;
    private FadeObject moonFadeRef;

    // Start is called before the first frame update
    void Start()
    {
        sunFadeRef = GameObject.Find("SunTimer").GetComponent<FadeObject>();
        moonFadeRef = GameObject.Find("MoonTimer").GetComponent<FadeObject>();

        // Makes sure game always starts in the daytime on restart
        startGameDayTime = true;
        stopCounting = false;
        day = true;
        dawn = false;
        dusk = false;
        night = false;
        timeOfDay = 0;
    }

    void Update()
    {
        // Until we hit a gameover, keep the day night cycle running
        if (stopCounting == false)
        {
            // Constantly counts up in seconds
            timeOfDay += Time.deltaTime;
        }

        // Determines whent to trigger shifts in sky color depending on time of day
        if (timeOfDay >= 45 && timeOfDay < 60)
        {
            startGameDayTime = false;
            dusk = true;
        }
        else if (timeOfDay >= 60 && timeOfDay < 105)
        {
            dusk = false;
            night = true;
            StartCoroutine(moonFadeRef.FadeImage(false));
            StartCoroutine(sunFadeRef.FadeImage(true));
        }
        else if (timeOfDay >= 105 && timeOfDay < 120)
        {
            night = false;
            dawn = true;
        }
        else if (timeOfDay >= 120)
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