using UnityEngine;

public class NextScreen : MonoBehaviour
{

    // Thus script is used to load the next scene when a button is clicked.

    public string NextScreenName;

    public void NextScreenButtonClicked()
    {
        //Checks if there is a dedicated selection by name
        if (!string.IsNullOrWhiteSpace(NextScreenName))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(NextScreenName);
        }
        else//if not goes to game index
        {
            switch (GameList.gameIndex)
            {
                case 0:
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene("TargetTapGame");
                        break;
                    }
                case 1:
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene("JamesTestScene");
                        break;
                    }
            }
        }


    }
}
