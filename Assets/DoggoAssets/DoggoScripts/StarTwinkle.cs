using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleStudio95;

public class StarTwinkle : MonoBehaviour
{
    private DoodleAnimator doodleAnimatorRef;
    private FadeObject starFadeRef;
    public static bool showStars;

    // Start is called before the first frame update
    void Start()
    {
        doodleAnimatorRef = gameObject.GetComponent<DoodleAnimator>();
        starFadeRef = gameObject.GetComponent<FadeObject>();

        // Makes stars twinkle at different speeds/intervals
        doodleAnimatorRef.speed = Random.Range(0.75f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Makes the stars fade in only at night
        if (DayNightCycle.night == true)
        {
            StartCoroutine(starFadeRef.FadeSprite(false));
        }
        else
        {
            StartCoroutine(starFadeRef.FadeSprite(true));
        }
    }
}
