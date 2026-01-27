using UnityEngine;

public class Game :ScriptableObject
{
    public string gameName;
    public string gameDescription;
    public Sprite gameImage;
    public string[] gameTags = new string[2];
    public Sprite[] gameIcon = new Sprite[2];

    public CardFloat[] Settings;
    public bool RestartGame;
    public bool CardOrder;

    // game name can be 14 characters long
    // game names must be unique

    // game description can be 250 characters long
    // there can only be a max of 2 tags, and they can be 10 characters long
    // there can only be a max of 2 images
    // Images cannot be null
    // Icons cannot be null

    
}

