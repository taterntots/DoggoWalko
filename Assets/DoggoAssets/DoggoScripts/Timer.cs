using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static float currentTime;
    public static float finishTime;
    public TextMeshProUGUI timerText;

    public static bool timer = true;

    void Start()
    {
        //Makes sure the game always starts with the timer showing after a gameover
        timer = true;
    }

    void Update()
    {
        if (timer == true)
        {
            currentTime = currentTime + Time.deltaTime;
            timerText.text = Mathf.Round(currentTime).ToString();
        }

        //currently hides and stops the timer text once the Game Over screen kicks in
        if (timer == false)
        {
            timerText.gameObject.SetActive(false);
        }
    }
}
