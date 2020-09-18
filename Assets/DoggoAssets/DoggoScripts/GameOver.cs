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
        GameObject.FindWithTag("ObstacleSpawnerEnemy").SetActive(false);
        GameObject.FindWithTag("ObstacleSpawnerTennisBall").SetActive(false);
        // Stops the Timer (simply hides it. technically it is still counting)
        Timer.timer = false;
        // Destroys all gameObjects tagged as "Bad"
        DestroyAllEnemies("Bad");
        // Pulls up the Game Over Screen with stats and restart button
        gameOverCanvas.SetActive(true);
        // Destroy the doggo so the player can't move anymore
        Destroy(GameObject.FindWithTag("Player"));

    }

    // Function that destroys all enemies, passing the tag/type you want destroyed as the argument
    void DestroyAllEnemies(string tag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);

        for(int i=0; i<enemies.Length; i++)
        {
            GameObject.Destroy(enemies[i]);
        }
    }
}
