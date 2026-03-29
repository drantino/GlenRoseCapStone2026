using UnityEngine;
using TMPro;

public class SearchBar : MonoBehaviour
{
    public GameObject GameList;
    public string[] GameTitlesList;
    public TMP_InputField SearchInputField;
    public GameHolder gameHolder;



    /*
        The bellow function calls the GamesList variable and looks for all
        the exisint gmae title names to get a simple list that only 
        continas the list of all the games, this is used for performing 
        the search function later on
    */
    public void GetGameTitlesList()
    {
        int count = GameList.transform.childCount;
        GameTitlesList = new string[count];
        int index = 0;
        foreach (Transform Game in GameList.transform) {
            Transform GameTextBox = Game.Find("GameTextBox");
            Transform titleTextBox = GameTextBox.Find("Title");
            string title = titleTextBox.GetComponent<TMPro.TextMeshProUGUI>().text;
            GameTitlesList[index] = title;
            index++;
        }
    }

    /*
        The bellow function will be used to search for differnt games using there game title and
        only display games that match the search
    */

    public void Search()
    {
        foreach (string title in GameTitlesList) {
            if (title.ToLower().Contains(SearchInputField.text.ToLower())) {
                Debug.Log(title);
                gameHolder.ShowGame(title);
            }
            else {
                gameHolder.HideGame(title);
            }
        }
    }
}
