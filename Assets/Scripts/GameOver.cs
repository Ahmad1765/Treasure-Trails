using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Reference to the PlayerHealth script
    public PlayerHealth playerHealth;

    // Reference to the Continue Panel
    public GameObject continuePanel;
    void Awake(){
        continuePanel.SetActive(false);
    }
    void Start()
    {
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth script reference is missing!");
            return;
        }

        if (continuePanel == null)
        {
            Debug.LogError("Continue Panel reference is missing!");
            return;
        }

        continuePanel.SetActive(false); // Ensure the panel is initially inactive
    }

    void Update()
    {
        // Check if the player's health is zero
        if (playerHealth != null && playerHealth.GetCurrentHealth() <= 0)
        {
            //ActivateContinuePanel();
            ActivateContinuePanel();
        }
    }

    // Method to activate the Continue panel
    void ActivateContinuePanel()
    {
        Time.timeScale = 0f; // Pause the game
        continuePanel.SetActive(true); // Activate the Continue panel
    }
}
