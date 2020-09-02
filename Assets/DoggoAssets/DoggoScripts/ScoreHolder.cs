using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreHolder : MonoBehaviour
{
    public static int badBoiPoints = 0;
    public static int goodBoiPoints = 0;
    public static float timeTotal;

    public TextMeshProUGUI badBoiText;
    public TextMeshProUGUI goodBoiText;
    public TextMeshProUGUI totalTime;

 // Update is called once per frame
    void Update()
    {
        goodBoiText.text = goodBoiPoints.ToString();
        badBoiText.text = badBoiPoints.ToString();
        totalTime.text = Mathf.Round(Timer.currentTime).ToString();
    }
}
