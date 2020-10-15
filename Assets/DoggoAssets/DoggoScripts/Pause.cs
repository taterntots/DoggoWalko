using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private GameObject[] pauseObjects;
    public GameObject pauseCanvas;

    private bool isPaused = false;

    void Start()
    {
        // Needed for the Pause Menu to work
        pauseCanvas.SetActive(true);
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
    }

    // Update is called once per frame
    void Update()
    {
        // Uses the P Key to pause and unpause the game
        if (isPaused == false && Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            showPaused();
            isPaused = true;
        }
        else if (isPaused && Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 1;
            hidePaused();
            isPaused = false;
        }

        // Restarts the game if paused and pressing the R Key
        if (isPaused && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame("Runner");
        }

        // Restarts the game on gameover if certain keys are pushed
        if (GameOver.gameIsOver && (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Return)))
        {
            RestartGame("Runner");
        }
    }

    // Reloads the level upon hitting the restart button and resets good/badboi points, walker attitude, and Timer back to zero
    public void RestartGame(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
        ScoreHolder.badBoiPoints = 0;
        ScoreHolder.goodBoiPoints = 0;
        DoggoBehavior.walkerAttitude = 3;
        Timer.currentTime = 0;
    }

    // Controls the pausing of the scene
    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }

        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    // Shows objects with ShowOnPause tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    // Hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    // Brings player back to the main menu (or any scene, really. Depends what scene you drop in)
    public void LoadLevel(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
        Time.timeScale = 1;
    }
}