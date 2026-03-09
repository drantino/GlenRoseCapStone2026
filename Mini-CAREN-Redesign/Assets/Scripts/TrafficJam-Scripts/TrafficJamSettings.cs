using UnityEngine;

public class TrafficJamSettings : MonoBehaviour
{
    public Game staticTrafficJamSettings;
    public bool hasSettings;
    public bool useDebugSettings;

    //Debug settings
    [Header("Debug Settings")]
    public float gameTime;
    public float heightThreshold;
    public float carSpeed;
    public float carSpawnInterval;
    public float carLength;
    public bool carDetour;
    public float emergencyVehicleSideBias;
    public bool emergencyVehicleActive;

    //Properties
    public float GameTime
    {
        get
        {
            if (hasSettings && !useDebugSettings)
            {
                return staticTrafficJamSettings.Settings[0].SettingValue.value;
            }
            else
            {
                return gameTime;
            }
        }
    }
    public float HeightThreshold
    {
        get
        {
            if (hasSettings && !useDebugSettings)
            {
                return staticTrafficJamSettings.Settings[1].SettingValue.value;
            }
            else
            {
                return heightThreshold;
            }
        }
    }
    public float CarSpeed
    {
        get
        {
            if (hasSettings && !useDebugSettings)
            {
                return staticTrafficJamSettings.Settings[2].SettingValue.value;
            }
            else
            {
                return carSpeed;
            }
        }
    }
    public float CarSpawnInterval
    {
        get
        {
            if (hasSettings && !useDebugSettings)
            {
                return staticTrafficJamSettings.Settings[3].SettingValue.value;
            }
            else
            {
                return carSpawnInterval;
            }
        }
    }
    public float CarLength
    {
        get
        {
            if (hasSettings && !useDebugSettings)
            {
                return staticTrafficJamSettings.Settings[4].SettingValue.value;
            }
            else
            {
                return carLength;
            }
        }
    }
    public bool CarDetour
    {
        get
        {
            if (hasSettings && !useDebugSettings)
            {
                return (GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardDetour.isActive;
            }
            else
            {
                return carDetour;
            }
        }
    }
    public float EmergencyVehicleBias
    {
        get
        {
            if (hasSettings && !useDebugSettings)
            {
                return (GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardEmergencyVehicle.SettingValue.value;
            }
            else
            {
                return emergencyVehicleSideBias;
            }
        }
    }
    public bool EmergencyVehicleActive
    {
        get
        {
            if (hasSettings && !useDebugSettings)
            {
                return (GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardEmergencyVehicle.isActive;
            }
            else
            {
                return emergencyVehicleActive;
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
    }
}
