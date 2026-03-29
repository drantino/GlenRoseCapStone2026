using UnityEngine;


public class GameList : MonoBehaviour
{
    public Game[] gamesList;  // Array of TempGame (can be either BasicGameData or AdvancedGameData)
    public static Game[] staticGameList;
    public static int gameIndex = 0; // Index of the current game in the list


    private void Awake()
    {
        staticGameList = gamesList;
    }

    void Start()
    {
        // Check if the game names are unique
        CheckGameNamesAreUnique();

        // Check if the game names are less than 14 characters
        CheckGameNamesAreLessThanCharacters(14);

        // Check if the game descriptions are less than 250 characters
        CheckGameDescriptionsAreLessThanCharacters(250);

        // Check if the game images are not null
        CheckGameImagesAreNotNull();

        // Check if the game tags are less than 15 characters
        CheckGameTagsAreLessThanCharacters(15);

        // Check if the game icons are not null
        CheckGameIconsAreNotNull();
    }




    // The Bellow function is used to set the game index, so that other scripts can access the current game
    public static void SetGameIndex(int index)
    {
        gameIndex = index;
    }


    /*
        * Function to check if the game names are unique
        * If they are not, it will print an error message to the console
    */
    public void CheckGameNamesAreUnique()
    {
        if (gamesList.Length > 2){
        for (int i = 0; i < gamesList.Length; i++)
        {
            for (int j = i + 1; j < gamesList.Length; j++)
            {
                if (gamesList[i].gameName == gamesList[j].gameName)
                {
                    Debug.LogError("Game names are not unique: " + gamesList[i].gameName);
                }
            }
        }
        }
    }


    /*
        * Function to check if the game names are less than a certain number of characters
        * If they are not, it will print an error message to the console
    */
    public void CheckGameNamesAreLessThanCharacters(int maxLength)
    {
        for (int i = 0; i < gamesList.Length; i++)
        {
            if (gamesList[i].gameName.Length > maxLength)
            {
                Debug.LogError("Game name is longer than " + maxLength + " characters: " + gamesList[i].gameName);
            }
        }
    }

    /*
        * Function to check if the game descriptions are less than a certain number of characters
        * If they are not, it will print an error message to the console
    */
    public void CheckGameDescriptionsAreLessThanCharacters(int maxLength)
    {
        for (int i = 0; i < gamesList.Length; i++)
        {
            if (gamesList[i].gameDescription.Length > maxLength)
            {
                Debug.LogError("Game description is longer than " + maxLength + " characters: " + gamesList[i].gameDescription);
            }
        }
    }

    /*
        * Function to check if the game images are not null
        * If they are null, it will print an error message to the console
    */
    public void CheckGameImagesAreNotNull()
    {
        for (int i = 0; i < gamesList.Length; i++)
        {
            if (gamesList[i].gameImage == null)
            {
                Debug.LogError("Game image is null: " + gamesList[i].gameName);
            }
        }
    }

    /*
        * Function to check if the game tags are less than a certain number of characters
        * If they are not, it will print an error message to the console
        * Also checks if there are more than 2 tags
    */
    public void CheckGameTagsAreLessThanCharacters(int maxLength)
    {
        for (int i = 0; i < gamesList.Length; i++)
        {
            for (int j = 0; j < gamesList[i].gameTags.Length; j++)
            {
                if (gamesList[i].gameTags[j].Length > maxLength)
                {
                    Debug.LogError("Game tag is longer than " + maxLength + " characters: " + gamesList[i].gameTags[j]);
                }
            }
        }

        // Check if the gameTags arrayy has a max of two tags
        for (int i = 0; i < gamesList.Length; i++)
        {
            if (gamesList[i].gameTags.Length > 2)
            {
                Debug.LogError("Game has more than 2 tags: " + gamesList[i].gameName);
            }
        }
    }


    /*
        * Function to check if the game icons are not null
        * If they are null, it will print an error message to the console
        * Also checks if there are more than 2 icons
    */
    public void CheckGameIconsAreNotNull()
    {
        for (int i = 0; i < gamesList.Length; i++)
        {
            for (int j = 0; j < gamesList[i].gameIcon.Length; j++)
            {
                if (gamesList[i].gameIcon[j] == null)
                {
                    Debug.LogError("Game icon is null: " + gamesList[i].gameName);
                }
            }
        }

        // Check if the gameIcons array has a max of two icons
        for (int i = 0; i < gamesList.Length; i++)
        {
            if (gamesList[i].gameIcon.Length > 2)
            {
                Debug.LogError("Game has more than 2 icons: " + gamesList[i].gameName);
            }
        }
    }

}
