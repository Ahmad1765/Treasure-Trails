using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management
using UnityEngine.UI; // Required for UI elements

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; // Assign the pause panel in the Unity Inspector
    private bool isPaused = false;

    public GameObject pauseButton;

    void Start()
    {
        // Ensure the pause panel is initially hidden
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    void Update()
    {
        // Toggle pause state when the pause button is pressed (example: "P" key for testing)
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void PauseButton()
    {
        // Deactivate the pause button and toggle pause
        pauseButton.SetActive(false);
        TogglePause();
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Pause the game and show the pause panel
            Time.timeScale = 0f; // Stops the game time
            pausePanel.SetActive(true);
        }
        else
        {
            // Resume the game and hide the pause panel
            Time.timeScale = 1f; // Resumes the game time
            pausePanel.SetActive(false);

            // Reactivate the pause button when resuming the game
            pauseButton.SetActive(true);
        }
    }

    public void StartButton()
    {
        // Resume the game
        TogglePause();
    }

    public void ExitButton()
    {
        // Quit the application
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void RetryButton()
    {
        Time.timeScale = 1f; // Ensure time is resumed before reloading
        StartCoroutine(ReloadSceneTwice());
    }

    private IEnumerator ReloadSceneTwice()
    {
        // First reload
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield return null; // Wait for the scene to load

        // Second reload after a frame
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButton()
    {
        // Load the Main Menu scene
        Time.timeScale = 1f; // Ensure time is resumed before loading the main menu
        SceneManager.LoadScene("Title"); // Replace "Main Menu" with your actual main menu scene name
    }

    public void ExitPausePanel()
    {
        // Hide the pause panel
        pausePanel.SetActive(false);

        // Reactivate the pause button
        pauseButton.SetActive(true);
    }

    public void ContinueButton()
    {
        RetryButton();
    }
}
