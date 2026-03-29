using UnityEngine;
using TMPro;

public class SettingsHeader : MonoBehaviour
{

    /*
        This script manages the settings header, including setting the title based on the selected game
        and handling navigation back to the previous scene.
    */
    
    public TextMeshProUGUI title;
    public string PreviousSceneName;

    void Start()
    {
        SetTitle();
    }

    public void SetTitle() {
        title.text = GameList.staticGameList[GameList.gameIndex].gameName;
    }

    public void backButton() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(PreviousSceneName);
    }
}
