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
        // Resets the timer to zero anytime a new game is started
        currentTime = 0;
        // Makes sure the game always starts with the timer showing after a gameover
        timer = true;
    }

    void Update()
    {
        // Displays timer on the HUD
        if (timer == true)
        {
            currentTime = currentTime + Time.deltaTime;
            timerText.text = Mathf.Round(currentTime).ToString();
        }

        // Currently hides and stops the timer text once the Game Over screen kicks in
        if (timer == false)
        {
            timerText.gameObject.SetActive(false);
        }
    }
}
