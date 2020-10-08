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
        // Stops the level from spawning further
        DoggoBehavior.walkingDog = false;
        // Stops the camera from moving
        GameObject.Find("MainCamera").GetComponent<MoveCamera>().cameraSpeed = 0;
        // Stops the obstacle launchers from spawning anything
        //GameObject.FindWithTag("ObstacleSpawnerEnemy").SetActive(false);
        //GameObject.FindWithTag("ObstacleSpawnerTennisBall").SetActive(false);
        //GameObject.FindWithTag("CarSpawner").SetActive(false);
        //GameObject.FindWithTag("CarSpawner2").SetActive(false);
        ObstacleSpawner.stopSpawning = true;
        // Stops the Timer (simply hides it. technically it is still counting)
        Timer.timer = false;
        // Destroys all gameObjects tagged as "Bad"
        DestroyTheThing.DestroyAll("Bad");
        // Destroys all stray Tennis Balls
        DestroyTheThing.DestroyAllParents("TennisBall");
        // Pulls up the Game Over Screen with stats and restart button
        gameOverCanvas.SetActive(true);
        // Destroy the doggo so the player can't move anymore
        Destroy(GameObject.FindWithTag("Player"));
        // Hide the Dog Walker from the background
        GameObject.Find("DogWalkerParent").SetActive(false);
        // Hide the Walker Text from the background
        GameObject.Find("WalkerTextParent").SetActive(false);
        // Stop the day/night cycle
        DayNightCycle.stopCounting = true;

    }
}
