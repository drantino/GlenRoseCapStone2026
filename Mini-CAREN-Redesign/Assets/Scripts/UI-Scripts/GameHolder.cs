using UnityEngine;
using UnityEngine.UI;
using TMPro;


/*
    GameHolder is responsible for populating the game list UI with game items, 
    setting their images, titles, descriptions, tags, and icons. 
    It also manages the logic for clicking on a game item to navigate to its details page.
*/

public class GameHolder : MonoBehaviour
{

    public GameObject GamesList;
    public GameObject Game_Item_prefab;
    public NextScreen nextScreen;
    public ScrollRect scrollRect;
    public SearchBar searchBar;
    


    /*
        Unity method called once before the first frame update.  
        It populates the game list by iterating through GameList.staticGameList 
        and creating UI elements for each game with their respective details.
    */


    void Start() 
    {

    SetScroleToTop(); 

    if (GameList.staticGameList == null) 
    {
        return;
    }
    int index = 0;
    foreach (Game game in GameList.staticGameList) 
    {
        AddItemToGameHolder(game.gameName, index);
        SetImage(game.gameImage, game.gameName);
        SetTitle(game.gameName);
        SetDescription(game.gameDescription, game.gameName);


        if (game.gameTags.Length == 1) 
        {
            SetTag(game.gameTags[0], game.gameName, 1);
            DisableTag(game.gameName, 2);
        } 
        else if (game.gameTags.Length == 2)
        {
            SetTag(game.gameTags[0], game.gameName, 1);
            SetTag(game.gameTags[1], game.gameName, 2);
        } 
        else
        {
            DisableTag(game.gameName, 1);
            DisableTag(game.gameName, 2);
        }
        

        if (game.gameIcon.Length == 1) 
        {
            SetIconImage(game.gameIcon[0], game.gameName, 1);
            DisableIconIamges(game.gameName, 2);
        } 
        else if (game.gameIcon.Length == 2)
        {
            SetIconImage(game.gameIcon[0], game.gameName, 1);
            SetIconImage(game.gameIcon[1], game.gameName, 2);
        } 
        else
        {
            DisableIconIamges(game.gameName, 1);
            DisableIconIamges(game.gameName, 2);
        }


        

        index++;
    }

    searchBar.GetGameTitlesList();

    }



    /*
        Sets up the button logic for a game item, so that when clicked, 
        it navigates to the game's detail screen and sets the selected game index.
    */
    public void SetGameItemButtonLogic(Transform gameItem, int index) {
        // Get the button component of the game item
        Button button = gameItem.GetComponent<Button>();
        // next screen
        button.onClick.AddListener(() => {
            nextScreen.NextScreenButtonClicked();
            GameList.SetGameIndex(index);
        });
    }

    /*
        Adds a new game item to the game holder UI, sets its name, parent, and position,
        and configures its button logic for navigation.
    */
    public void AddItemToGameHolder(string gameName, int index) {
        // Instantiate the Game_Item_prefab and set its parent to GameHolder, name the new item "Game_Item"
        
        GameObject newItem = Instantiate(Game_Item_prefab);
        newItem.name = gameName;
        newItem.transform.SetParent(GamesList.transform, false);
        newItem.transform.localPosition = Vector3.zero;
        Transform ImageArea = newItem.transform.Find("ImageArea");
        Transform GameTextBox = newItem.transform.Find("GameTextBox");

        SetGameItemButtonLogic(ImageArea,index);
        SetGameItemButtonLogic(GameTextBox,index);
    }

    /*
        The following methods are used to set various properties of a game item in the UI,
        such as its image, title, description, tags, and icons. They locate the relevant
        UI elements within the game item and update their content accordingly.
    */
    public void SetImage(Sprite image, string gameName) {
        Transform Game = GamesList.transform.Find(gameName);
        Transform imageTransform = Game.Find("ImageArea");
        imageTransform.GetComponent<UnityEngine.UI.Image>().sprite = image;
    }

    /*
        Sets the title text of a game item identified by gameName.
    */
    public void SetTitle(string gameName) {
        Transform Game = GamesList.transform.Find(gameName);
        Transform titleTransform = Game.transform.Find("GameTextBox");
        Transform titleTextTransform = titleTransform.Find("Title");
        titleTextTransform.GetComponent<TMPro.TextMeshProUGUI>().text = gameName;
    }
    /*
        Sets the description text of a game item identified by gameName.
    */
    public void SetDescription(string description, string gameName) {
        Transform Game = GamesList.transform.Find(gameName);
        Transform descriptionTransform = Game.transform.Find("GameTextBox");
        Transform descriptionTextTransform = descriptionTransform.Find("Description");
        descriptionTextTransform.GetComponent<TMPro.TextMeshProUGUI>().text = description;
    }

    /*
        Sets the tag text of a game item identified by gameName and TagNumber.
    */
    public void SetTag(string tag, string gameName, int TagNumber) {
        Transform Game = GamesList.transform.Find(gameName);
        Transform GameTextBoxTransform = Game.transform.Find("GameTextBox");
        Transform TagLocation = GameTextBoxTransform.Find("Tag-" + TagNumber);
        Transform tagTextTransform = TagLocation.Find("Tag-"+ TagNumber + "-Text");
        tagTextTransform.GetComponent<TMPro.TextMeshProUGUI>().text = tag;
    }

    /*
        Sets the icon image of a game item identified by gameName and IconNumber.
    */  
    public void SetIconImage(Sprite image, string gameName, int IconNumber) {
        Transform Game = GamesList.transform.Find(gameName);
        Transform GameTextBoxTransform = Game.transform.Find("GameTextBox");
        Transform IconLocation = GameTextBoxTransform.Find("Icon-" + IconNumber);
        IconLocation.GetComponent<UnityEngine.UI.Image>().sprite = image;
    }

    /*
        The following methods disable specific UI elements (icons or tags) of a game item
        identified by gameName and their respective numbers (IconNumber or TagNumber).
    */
    public void DisableIconIamges(string gameName, int IconNumber) {
        Transform Game = GamesList.transform.Find(gameName);
        Transform GameTextBoxTransform = Game.transform.Find("GameTextBox");
        Transform IconLocation = GameTextBoxTransform.Find("Icon-" + IconNumber);
        IconLocation.gameObject.SetActive(false);
    }

    /*
        Disables a specific tag UI element of a game item identified by gameName and TagNumber.
    */
    public void DisableTag(string gameName, int TagNumber) {
        Transform Game = GamesList.transform.Find(gameName);
        Transform GameTextBoxTransform = Game.transform.Find("GameTextBox");
        Transform TagLocation = GameTextBoxTransform.Find("Tag-" + TagNumber);
        TagLocation.gameObject.SetActive(false);
    }
    /*
        Hides a game item in the game list by setting its active state to false.
    */
    public void HideGame(string gameName) {
        Transform Game = GamesList.transform.Find(gameName);
        Game.gameObject.SetActive(false);
    }

    /*
        Shows a game item in the game list by setting its active state to true.
    */
    public void ShowGame(string gameName) {
        Transform Game = GamesList.transform.Find(gameName);
        Game.gameObject.SetActive(true);
    }

    /*
        Sets the scroll position of the game list to the top.
    */
    public void SetScroleToTop() {
        LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.content);
        scrollRect.verticalNormalizedPosition = 1f;
    }
    
}
