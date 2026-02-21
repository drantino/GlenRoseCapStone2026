using UnityEngine;

public class NextScreen : MonoBehaviour
{

    // Thus script is used to load the next scene when a button is clicked.

    public string NextScreenName;
    public bool UseGameIndex;
    public void NextScreenButtonClicked()
    {
        //checks if wanting to use game index
        if (!UseGameIndex)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(NextScreenName);
        }
        else//if not goes to game index
        {
            switch (GameList.staticGameList[GameList.gameIndex].gameName)
            {
                case "TargetTap":
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene("TargetTapGame");
                        break;
                    }
                case "TrafficJam":
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene("TrafficJam");
                        break;
                    }
                default:
                    {

                        break;
                    }
            }
        }


    }
}
