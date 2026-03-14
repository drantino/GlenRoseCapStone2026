using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TraffitJamSettingsUI : MonoBehaviour
{
    public GameObject endGamePanel;
    public GameObject exitGamePanel;
    public GameObject recalibrationCompletePanel;
    public GameObject recalibrationErrorPanel;
    public GameObject saveSessionPanel;
    public TMP_InputField playerNameInputField; 

    public void OpenExitGamePanel()
    {
        exitGamePanel.SetActive(true);
    }
    public void CloseExitGamePanel()
    {
        exitGamePanel.SetActive(false);
    }
    public void OpenEndGamePanel()
    {
        endGamePanel.SetActive(true);
    }
    public void CloseEndGamePanel()
    {
        endGamePanel.SetActive(false);
    }
    public void CloseRecalibrationComplete()
    {
        recalibrationCompletePanel.SetActive(false);
    }

    public void OnSaveSessionButtonPressed()
    {
        // TODO: Open save session page
        // page should contain an text field where user can input the player name
        // page should have a button to save the session
        // page should also contain this disclaimer: "warning: if settings were changed mid-round, only the settings at the end of the round will be saved."

        saveSessionPanel.SetActive(true);
    }

    public void OnSaveButtonPressed()
    {
		TrafficJamSaveSystem.SaveSessionToDisk();
        saveSessionPanel.SetActive(false);
	}

    public void OnPlayerNameChanged()
    {
        TrafficJamSaveSystem.SetPlayerName(playerNameInputField.text);
    }
}
