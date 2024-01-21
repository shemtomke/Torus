using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool Play= true;
    public bool isGameOver = false;
    public bool isStart = false;
    

    public GameObject gameOverPanel;
    public GameObject gamePlayPanel;
    public GameObject mainMenuPanel;
    public GameObject pauseMenuPanel;

    AudioManager audioManager;
    private void Awake() 
    {
        Time.timeScale=1;
        audioManager= FindObjectOfType<AudioManager>();
        
    }

    private void Update()
    {
        GameOver();
        StartGame();
    }

    private void StartGame()// NOT SURE WHAT YOU WANTED TO DO HERE SO ILL CREATE ANOTHER ONE FOR START BUTTON
    {
        if(!isStart)
        {
           

        }
    }

    public void StartButton()
    {
        mainMenuPanel.SetActive(false);
        gamePlayPanel.SetActive(true);
        isStart=true;
    }
    public void RestartGame()
    {
        Time.timeScale=1;
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void PauseGame()
    {
        Time.timeScale=0;
        gamePlayPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);

    }

    public void ResumeGame()
    {
        Time.timeScale=1;
        gamePlayPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);

    }

    void GameOver()
    {
        
        if(isGameOver)
        {
            if(Play)
            {
                audioManager.Play("GameOver");
                Play = false;
            }

            Time.timeScale=0;
            gameOverPanel.SetActive(true);
            gamePlayPanel.SetActive(false);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
