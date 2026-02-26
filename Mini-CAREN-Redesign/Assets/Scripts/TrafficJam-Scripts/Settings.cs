using UnityEngine;

public class Settings : MonoBehaviour
{
    public Game staticTrafficJamSettings;
    public bool hasSettings;
    public bool useDebugSettings;

    //Debug settings
    public float gameTime;



    //Properties
    public float GameTime {
        get
            {
            if (hasSettings)
            {
                return staticTrafficJamSettings.Settings[0].SettingValue.value;
            }
            else
            {
                return gameTime;
            }     
        }
    }
    void Start()
    {
        try
        {
            staticTrafficJamSettings = GameList.staticGameList[GameList.gameIndex];
            hasSettings = true;
        }
        catch
        {
            Debug.LogWarning("No static settings found.");
        }
        Debug.Log(GameTime);
    }
}
