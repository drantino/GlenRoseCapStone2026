using UnityEngine;

public class GameSettingsToggle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject GameSettings;
    public GameObject GameStatistics;
    

    /*
        Toggles the view to show Game Settings and hide Game Statistics.
    */
   public void ToggleGameSettings()
    {
        GameSettings.SetActive(true);
        GameStatistics.SetActive(false);
    }

    public void ToggleGameStatistics()
    {
        GameSettings.SetActive(false);
        GameStatistics.SetActive(true);
    }
}
