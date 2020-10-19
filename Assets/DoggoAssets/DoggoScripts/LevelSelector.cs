using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    private MoveCamera moveCameraRef;
    private LevelSpawner levelSpawnerRef;
    private ObstacleSpawner obstacleSpawnerEnemyRef;

    [Range(1, 10)] public int level;

    // Start is called before the first frame update
    void Start()
    {
        // Grabs reference to the MoveCamera script
        moveCameraRef = GameObject.FindWithTag("MainCamera").GetComponent<MoveCamera>();
        // Grabs reference to the LevelSpawner script
        levelSpawnerRef = GameObject.FindWithTag("GameController").GetComponent<LevelSpawner>();
        // Grabs reference to the ObstacleSpawnerEnemy script component
        obstacleSpawnerEnemyRef = GameObject.FindWithTag("ObstacleSpawnerEnemy").GetComponent<ObstacleSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        // As long as the game is running
        if (GameOver.gameIsOver == false)
        {
            if (level == 1)
            {
                // Makes the walker (aka the game) move faster
                moveCameraRef.cameraSpeed = 1.0f;
                // Makes the dog walk faster to match the cameraspeed
                PlayerMovement.doggoAutoSpeed = 1.0f;
                // Makes enemies spawn more frequently
                obstacleSpawnerEnemyRef.maxTime = 2.5f;
                obstacleSpawnerEnemyRef.minTime = 1.5f;
                // Makes Obstacle Spawner move a little faster
                obstacleSpawnerEnemyRef.spawnerSpeed = 1.0f;
                // Increase homespawn to make levels longer
                levelSpawnerRef.homeSpawn = 12;
            }
            else if (level == 2)
            {
                // Makes the walker (aka the game) move faster
                moveCameraRef.cameraSpeed = 1.5f;
                // Makes the dog walk faster to match the cameraspeed
                PlayerMovement.doggoAutoSpeed = 1.5f;
                // Makes enemies spawn more frequently
                obstacleSpawnerEnemyRef.maxTime = 2.0f;
                obstacleSpawnerEnemyRef.minTime = 1.0f;
                // Makes Obstacle Spawner move a little faster
                obstacleSpawnerEnemyRef.spawnerSpeed = 1.5f;
                // Increase homespawn to make levels longer
                levelSpawnerRef.homeSpawn = 14;
            }
            else if (level == 3)
            {
                // Makes the walker (aka the game) move faster
                moveCameraRef.cameraSpeed = 2.0f;
                // Makes the dog walk faster to match the cameraspeed
                PlayerMovement.doggoAutoSpeed = 2.0f;
                // Makes enemies spawn more frequently
                obstacleSpawnerEnemyRef.maxTime = 1.5f;
                obstacleSpawnerEnemyRef.minTime = 1.0f;
                // Makes Obstacle Spawner move a little faster
                obstacleSpawnerEnemyRef.spawnerSpeed = 2.0f;
                // Increase homespawn to make levels longer
                levelSpawnerRef.homeSpawn = 16;
            }
            else if (level == 4)
            {
                // Makes the walker (aka the game) move faster
                moveCameraRef.cameraSpeed = 2.5f;
                // Makes the dog walk faster to match the cameraspeed
                PlayerMovement.doggoAutoSpeed = 2.5f;
                // Makes enemies spawn more frequently
                obstacleSpawnerEnemyRef.maxTime = 1.5f;
                obstacleSpawnerEnemyRef.minTime = 1.0f;
                // Makes Obstacle Spawner move a little faster
                obstacleSpawnerEnemyRef.spawnerSpeed = 2.5f;
                // Increase homespawn to make levels longer
                levelSpawnerRef.homeSpawn = 18;
            }
            else if (level == 5)
            {
                // Makes the walker (aka the game) move faster
                moveCameraRef.cameraSpeed = 3.0f;
                // Makes the dog walk faster to match the cameraspeed
                PlayerMovement.doggoAutoSpeed = 3.0f;
                // Makes enemies spawn more frequently
                obstacleSpawnerEnemyRef.maxTime = 0.8f;
                obstacleSpawnerEnemyRef.minTime = 1.2f;
                // Makes Obstacle Spawner move a little faster
                obstacleSpawnerEnemyRef.spawnerSpeed = 3.0f;
                // Increase homespawn to make levels longer
                levelSpawnerRef.homeSpawn = 20;
            }
            else if (level == 6)
            {
                // Makes the walker (aka the game) move faster
                moveCameraRef.cameraSpeed = 3.5f;
                // Makes the dog walk faster to match the cameraspeed
                PlayerMovement.doggoAutoSpeed = 3.5f;
                // Makes enemies spawn more frequently
                obstacleSpawnerEnemyRef.maxTime = 0.7f;
                obstacleSpawnerEnemyRef.minTime = 1.1f;
                // Makes Obstacle Spawner move a little faster
                obstacleSpawnerEnemyRef.spawnerSpeed = 3.5f;
                // Increase homespawn to make levels longer
                levelSpawnerRef.homeSpawn = 22;
            }
            else if (level == 7)
            {
                // Makes the walker (aka the game) move faster
                moveCameraRef.cameraSpeed = 4.0f;
                // Makes the dog walk faster to match the cameraspeed
                PlayerMovement.doggoAutoSpeed = 4.0f;
                // Makes enemies spawn more frequently
                obstacleSpawnerEnemyRef.maxTime = 0.6f;
                obstacleSpawnerEnemyRef.minTime = 1.0f;
                // Makes Obstacle Spawner move a little faster
                obstacleSpawnerEnemyRef.spawnerSpeed = 4.0f;
                // Increase homespawn to make levels longer
                levelSpawnerRef.homeSpawn = 24;
            }
            else if (level == 8)
            {
                // Makes the walker (aka the game) move faster
                moveCameraRef.cameraSpeed = 4.5f;
                // Makes the dog walk faster to match the cameraspeed
                PlayerMovement.doggoAutoSpeed = 4.5f;
                // Makes enemies spawn more frequently
                obstacleSpawnerEnemyRef.maxTime = 0.5f;
                obstacleSpawnerEnemyRef.minTime = 0.8f;
                // Makes Obstacle Spawner move a little faster
                obstacleSpawnerEnemyRef.spawnerSpeed = 4.0f;
                // Increase homespawn to make levels longer
                levelSpawnerRef.homeSpawn = 26;
            }
            else if (level == 9)
            {
                // Makes the walker (aka the game) move faster
                moveCameraRef.cameraSpeed = 5.0f;
                // Makes the dog walk faster to match the cameraspeed
                PlayerMovement.doggoAutoSpeed = 5.0f;
                // Makes enemies spawn more frequently
                obstacleSpawnerEnemyRef.maxTime = 0.5f;
                obstacleSpawnerEnemyRef.minTime = 0.8f;
                // Makes Obstacle Spawner move a little faster
                obstacleSpawnerEnemyRef.spawnerSpeed = 4.0f;
                // Increase homespawn to make levels longer
                levelSpawnerRef.homeSpawn = 28;
            }
            else if (level >= 10)
            {
                // Makes the walker (aka the game) move faster
                moveCameraRef.cameraSpeed = 5.5f;
                // Makes the dog walk faster to match the cameraspeed
                PlayerMovement.doggoAutoSpeed = 5.5f;
                // Makes enemies spawn more frequently
                obstacleSpawnerEnemyRef.maxTime = 0.5f;
                obstacleSpawnerEnemyRef.minTime = 0.8f;
                // Makes Obstacle Spawner move a little faster
                obstacleSpawnerEnemyRef.spawnerSpeed = 4.0f;
                // Increase homespawn to make levels longer
                levelSpawnerRef.homeSpawn = 30;
            }
        }
    }
}