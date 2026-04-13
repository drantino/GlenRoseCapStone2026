using UnityEngine;

public class TrafficJamSettings : MonoBehaviour
{
    public Game staticTrafficJamSettings;
    public bool hasSettings;
    public bool useDebugSettings;

    //Debug settings
    [Header("Debug Settings")]
    [SerializeField] private float gameTime;
    [SerializeField] private float heightThreshold;
    [SerializeField] private float carSpeed;
    [SerializeField] private float carSpawnInterval;
    [SerializeField] private float carLength;
    [SerializeField] private bool carDetour;
    [SerializeField] private float emergencyVehicleSideBias;
    [SerializeField] private bool emergencyVehicleActive;
    [SerializeField] private float masterVolume;
    [SerializeField] private float carSpawnBias;

	//private void Update()
	//{
	//	AudioPlayer.masterVolume = MasterVolume;
	//}

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
    public float MasterVolume
    {
        get
        {
            if (hasSettings && !useDebugSettings)
            {
                return staticTrafficJamSettings.Settings[5].SettingValue.value / 100;
            }
            else
            {
                return masterVolume;
            }
        }
    }
    public float CarSpawnBias
    {
        get
        {
            if(hasSettings && !useDebugSettings)
            {
                return staticTrafficJamSettings.Settings[6].SettingValue.value;
            }
            else
            {
                return carSpawnBias;
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
