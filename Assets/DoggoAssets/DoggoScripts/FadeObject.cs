using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeObject : MonoBehaviour
{
    // the image you want to fade, assign in inspector
    public Image img;
    public SpriteRenderer spr;

    private void Start()
    {

    }

    // Handles fading of images
    public IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }

    // Handles fading of sprites
    public IEnumerator FadeSprite(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= (Time.deltaTime * 0.5f))
            {
                // set color with i as alpha
                spr.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += (Time.deltaTime * 0.5f))
            {
                // set color with i as alpha
                spr.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}