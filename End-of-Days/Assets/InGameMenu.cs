using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public static InGameMenu Instance { get; set; }
    public bool GamePaused;
    public GameObject pauseMenu;
    public GameObject winScreen;
    public GameObject loseScreen;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    
    void Update()
    {
        // Listens for when the ESC key is pressed.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Checks whether the game is already paused
            // If it is it will resume the game else it will pause it
            if (GamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void RestartGame()
    {
        GamePaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        GamePaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void WonGame()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void LoseGame()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
