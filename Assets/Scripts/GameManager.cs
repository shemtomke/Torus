using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
    public bool isStart = false;

    public GameObject gameOverPanel;
    public GameObject mainMenuPanel;
    public GameObject pauseMenuPanel;

    private void Update()
    {
        GameOver();
        StartGame();
    }

    private void StartGame()
    {
        if(isStart)
        {

        }
    }
    public void RestartGame()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }

    void GameOver()
    {
        if(isGameOver)
        {
            gameOverPanel.SetActive(true);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
