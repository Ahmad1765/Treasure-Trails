using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management
using UnityEngine.UI; // Required for UI elements

public class MainMenu : MonoBehaviour
{
    public GameObject levelsPanel; // Assign the levels panel in the Unity Inspector
    public Button[] levelButtons;  // Assign level buttons in the Unity Inspector

    void Start()
    {
        // Hide the levels panel initially
        levelsPanel.SetActive(false);

        // Unlock levels based on the player's progress
        int unlockedLevel = PlayerPrefs.GetInt("unlockedLevel", 1); // Default to level 1 if no data

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i < unlockedLevel)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void StartButton()
    {
        // Load the latest unlocked level
        int unlockedLevel = PlayerPrefs.GetInt("unlockedLevel", 1); // Default to level 1 if no data
        SceneManager.LoadScene("Level " + unlockedLevel);
    }

    public void ExitButton()
    {   
        Debug.Log("Quiting");
        // Quit the application
        Application.Quit();
    }

    public void LevelsButton()
    {
        // Show the levels panel
        levelsPanel.SetActive(true);
    }

    // Method to load a specific level
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level " + levelIndex);
    }
    public void BackLevelButton(){
        levelsPanel.SetActive(false);
    }
}
