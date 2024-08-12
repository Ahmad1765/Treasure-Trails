using UnityEngine;

public class Menu : MonoBehaviour
{
    // References to the UI panels
    public GameObject titlePanel;
    public GameObject lobbyPanel;
    public GameObject stageSelectionPanel;

    // Method to navigate to the lobby

    void Start(){
        titlePanel.SetActive(true);
        lobbyPanel.SetActive(false);
        stageSelectionPanel.SetActive(false);
    }
    public void GoToLobby()
    {
        DisableAllPanels();
        if (lobbyPanel != null)
        {
            lobbyPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Lobby Panel is not assigned.");
        }
    }

    // Method to navigate back to the main menu
    public void GoToMainMenu()
    {
        DisableAllPanels();
        if (titlePanel != null)
        {
            titlePanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Main Menu Panel is not assigned.");
        }
    }

    // Method to navigate to the stage selection panel
    public void GoToStageSelection()
    {
        DisableAllPanels();
        if (stageSelectionPanel != null)
        {
            stageSelectionPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Stage Selection Panel is not assigned.");
        }
    }

    public void GoToTitle(){
        lobbyPanel.SetActive(false);
        stageSelectionPanel.SetActive(false);
        titlePanel.SetActive(true);
    }

    // Helper method to disable all panels
    private void DisableAllPanels()
    {
        if (titlePanel != null) titlePanel.SetActive(false);
        if (lobbyPanel != null) lobbyPanel.SetActive(false);
        if (stageSelectionPanel != null) stageSelectionPanel.SetActive(false);
    }
}
