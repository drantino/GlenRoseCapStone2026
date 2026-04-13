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
    public GameObject sessionSavedPanel;
    public TMP_InputField playerNameInputField; 
    public TextMeshProUGUI filePathTextMesh;

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
        saveSessionPanel.SetActive(true);
    }

    public void OnSaveButtonPressed()
    {
		string filePath = TrafficJamSaveSystem.SaveSessionToDisk();
        saveSessionPanel.SetActive(false);
        sessionSavedPanel.SetActive(true);

        filePathTextMesh.text = filePath;
	}
    public void OnSaveCancelPressed()
    {
        saveSessionPanel.SetActive(false);
    }

    public void OnPlayerNameChanged()
    {
        TrafficJamSaveSystem.SetPlayerName(playerNameInputField.text);
    }

    public void OnSessionSavedCloseButtonpressed()
    {
        sessionSavedPanel.SetActive(false);
    }
}
