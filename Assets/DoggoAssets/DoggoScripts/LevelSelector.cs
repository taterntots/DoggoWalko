using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    private MoveCamera moveCameraRef;
    private PlayerMovement playerMovementRef;
    private LevelSpawner levelSpawnerRef;
    private ObstacleSpawner obstacleSpawnerEnemyRef;

    [Range(1, 10)] public int level;

    // Start is called before the first frame update
    void Start()
    {
        // Grabs reference to the MoveCamera script
        moveCameraRef = GameObject.FindWithTag("MainCamera").GetComponent<MoveCamera>();
        // Grabs reference to the PlayerMovement script
        playerMovementRef = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        // Grabs reference to the LevelSpawner script
        levelSpawnerRef = GameObject.FindWithTag("GameController").GetComponent<LevelSpawner>();
        // Grabs reference to the ObstacleSpawnerEnemy script component
        obstacleSpawnerEnemyRef = GameObject.FindWithTag("ObstacleSpawnerEnemy").GetComponent<ObstacleSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (level == 1)
        {
            // Makes the walker (aka the game) move faster
            moveCameraRef.cameraSpeed = 1.0f;
            // Makes the dog walk faster to match the cameraspeed
            playerMovementRef.doggoAutoSpeed = 1.0f;
            // Makes enemies spawn more frequently
            obstacleSpawnerEnemyRef.minTime = 4.0f;
            obstacleSpawnerEnemyRef.maxTime = 2.0f;
            // Makes Obstacle Spawner move a little faster
            obstacleSpawnerEnemyRef.spawnerSpeed = 1.0f;
            // Makes buildings spawn a bit faster
            levelSpawnerRef.spawnTime = 5.0f;
        }
        else if (level == 2)
        {
            // Makes the walker (aka the game) move faster
            moveCameraRef.cameraSpeed = 1.5f;
            // Makes the dog walk faster to match the cameraspeed
            playerMovementRef.doggoAutoSpeed = 1.5f;
            // Makes enemies spawn more frequently
            obstacleSpawnerEnemyRef.minTime = 3.8f;
            obstacleSpawnerEnemyRef.maxTime = 1.6f;
            // Makes Obstacle Spawner move a little faster
            obstacleSpawnerEnemyRef.spawnerSpeed = 1.5f;
            // Makes buildings spawn a bit faster
            levelSpawnerRef.spawnTime = 4.5f;
        }
        else if (level == 3)
        {
            // Makes the walker (aka the game) move faster
            moveCameraRef.cameraSpeed = 2.0f;
            // Makes the dog walk faster to match the cameraspeed
            playerMovementRef.doggoAutoSpeed = 2.0f;
            // Makes enemies spawn more frequently
            obstacleSpawnerEnemyRef.minTime = 3.6f;
            obstacleSpawnerEnemyRef.maxTime = 1.2f;
            // Makes Obstacle Spawner move a little faster
            obstacleSpawnerEnemyRef.spawnerSpeed = 2.0f;
            // Makes buildings spawn a bit faster
            levelSpawnerRef.spawnTime = 4.0f;
        }
        else if (level == 4)
        {
            // Makes the walker (aka the game) move faster
            moveCameraRef.cameraSpeed = 2.5f;
            // Makes the dog walk faster to match the cameraspeed
            playerMovementRef.doggoAutoSpeed = 2.5f;
            // Makes enemies spawn more frequently
            obstacleSpawnerEnemyRef.minTime = 3.4f;
            obstacleSpawnerEnemyRef.maxTime = 0.8f;
            // Makes Obstacle Spawner move a little faster
            obstacleSpawnerEnemyRef.spawnerSpeed = 2.5f;
            // Makes buildings spawn a bit faster
            levelSpawnerRef.spawnTime = 3.5f;
        }
        else if (level == 5)
        {
            // Makes the walker (aka the game) move faster
            moveCameraRef.cameraSpeed = 3.0f;
            // Makes the dog walk faster to match the cameraspeed
            playerMovementRef.doggoAutoSpeed = 3.0f;
            // Makes enemies spawn more frequently
            obstacleSpawnerEnemyRef.minTime = 3.2f;
            obstacleSpawnerEnemyRef.maxTime = 0.4f;
            // Makes Obstacle Spawner move a little faster
            obstacleSpawnerEnemyRef.spawnerSpeed = 3.0f;
            // Makes buildings spawn a bit faster
            levelSpawnerRef.spawnTime = 3.0f;
        }
        else if (level == 6)
        {
            // Makes the walker (aka the game) move faster
            moveCameraRef.cameraSpeed = 3.5f;
            // Makes the dog walk faster to match the cameraspeed
            playerMovementRef.doggoAutoSpeed = 3.5f;
            // Makes enemies spawn more frequently
            obstacleSpawnerEnemyRef.minTime = 3.0f;
            obstacleSpawnerEnemyRef.maxTime = 0.4f;
            // Makes Obstacle Spawner move a little faster
            obstacleSpawnerEnemyRef.spawnerSpeed = 3.5f;
            // Makes buildings spawn a bit faster
            levelSpawnerRef.spawnTime = 2.5f;
        }
        else if (level == 7)
        {
            // Makes the walker (aka the game) move faster
            moveCameraRef.cameraSpeed = 4.0f;
            // Makes the dog walk faster to match the cameraspeed
            playerMovementRef.doggoAutoSpeed = 4.0f;
            // Makes enemies spawn more frequently
            obstacleSpawnerEnemyRef.minTime = 2.8f;
            obstacleSpawnerEnemyRef.maxTime = 0.4f;
            // Makes Obstacle Spawner move a little faster
            obstacleSpawnerEnemyRef.spawnerSpeed = 4.0f;
            // Makes buildings spawn a bit faster
            levelSpawnerRef.spawnTime = 2.0f;
        }
        else if (level == 8)
        {
            // Makes the walker (aka the game) move faster
            moveCameraRef.cameraSpeed = 4.5f;
            // Makes the dog walk faster to match the cameraspeed
            playerMovementRef.doggoAutoSpeed = 4.5f;
            // Makes enemies spawn more frequently
            obstacleSpawnerEnemyRef.minTime = 2.6f;
            obstacleSpawnerEnemyRef.maxTime = 0.4f;
            // Makes Obstacle Spawner move a little faster
            obstacleSpawnerEnemyRef.spawnerSpeed = 4.0f;
            // Makes buildings spawn a bit faster
            levelSpawnerRef.spawnTime = 1.5f;
        }
        else if (level == 9)
        {
            // Makes the walker (aka the game) move faster
            moveCameraRef.cameraSpeed = 5.0f;
            // Makes the dog walk faster to match the cameraspeed
            playerMovementRef.doggoAutoSpeed = 5.0f;
            // Makes enemies spawn more frequently
            obstacleSpawnerEnemyRef.minTime = 2.4f;
            obstacleSpawnerEnemyRef.maxTime = 0.4f;
            // Makes Obstacle Spawner move a little faster
            obstacleSpawnerEnemyRef.spawnerSpeed = 4.0f;
            // Makes buildings spawn a bit faster
            levelSpawnerRef.spawnTime = 1.0f;
        }
        else if (level >= 10)
        {
            // Makes the walker (aka the game) move faster
            moveCameraRef.cameraSpeed = 5.5f;
            // Makes the dog walk faster to match the cameraspeed
            playerMovementRef.doggoAutoSpeed = 5.5f;
            // Makes enemies spawn more frequently
            obstacleSpawnerEnemyRef.minTime = 2.2f;
            obstacleSpawnerEnemyRef.maxTime = 0.4f;
            // Makes Obstacle Spawner move a little faster
            obstacleSpawnerEnemyRef.spawnerSpeed = 4.0f;
            // Makes buildings spawn a bit faster
            levelSpawnerRef.spawnTime = 1.0f;
        }
    }
}
