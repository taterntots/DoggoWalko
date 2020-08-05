using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.SetActive(false);
    }

    public void EndGame()
    {
        //Stops the level from spawning further
        DoggoBehavior.walkingDog = false;
        //Stops the camera from moving
        GameObject.Find("MainCamera").GetComponent<MoveCamera>().cameraSpeed = 0;
        //Stops the obstacle launcher from spawning anything
        GameObject.FindWithTag("ObstacleSpawner").SetActive(false);
        //Stops the Timer (simply hides it. technically it is still counting)
        Timer.timer = false;
        //Destroys all gameObjects tagged as "Bad"
        Destroy(GameObject.FindWithTag("Bad")); //Currently does not work
        //Pulls up the Game Over Screen with stats and restart button
        gameOverCanvas.SetActive(true);


    }
}
