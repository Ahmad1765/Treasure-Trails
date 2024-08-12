using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private Chest[] chestsInLevel; // Array to hold all chests in the level
    [SerializeField] private int totalChests;
    [SerializeField] private int openedChests;

    public float delayBeforeNextLevel = 3f; // Time to wait before loading the next level

    void Start()
    {
        // Find all chests in the level and store them in the array
        chestsInLevel = FindObjectsOfType<Chest>();
        totalChests = chestsInLevel.Length;
        openedChests = 0;

        // Subscribe to each chest's opening event
        foreach (Chest chest in chestsInLevel)
        {
            chest.OnChestOpened += ChestOpened;
        }
        UnlockCurrentLevel();
    }

    // This method is called when a chest is opened
    public void ChestOpened()
    {
        openedChests++;

        if (openedChests >= totalChests)
        {
            StartCoroutine(LoadNextLevelAfterDelay());
        }
    }

    IEnumerator LoadNextLevelAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeNextLevel);

        // Ensure time is resumed before loading the next level
        Time.timeScale = 1f;

        // Get the current level index
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next level if available
        if (currentLevelIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentLevelIndex + 1);
        }
        else
        {
            Debug.Log("No more levels to load.");
            // Optionally, you could return to the main menu or restart the game
        }
    }
    private void UnlockCurrentLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex + 1; // Get the current scene index and adjust to match your level numbers

        int currentUnlockedLevel = PlayerPrefs.GetInt("unlockedLevel", 1);

        // Check if the current level is higher than the last unlocked level
        if (currentLevel > currentUnlockedLevel)
        {
            // Unlock the current level
            PlayerPrefs.SetInt("unlockedLevel", currentLevel);
            PlayerPrefs.Save(); // Ensure the data is saved
        }
    }

}
