using UnityEngine;

public class TraffitJamSettingsUI : MonoBehaviour
{
    public GameObject endGamePanel;
    public GameObject exitGamePanel;
    public GameObject recalibrationCompletePanel;
    public GameObject recalibrationErrorPanel;

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
}
