using UnityEngine;

public class NextScreen : MonoBehaviour
{

    // Thus script is used to load the next scene when a button is clicked.
    
    public string NextScreenName;

    public void NextScreenButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(NextScreenName);
    }
}
