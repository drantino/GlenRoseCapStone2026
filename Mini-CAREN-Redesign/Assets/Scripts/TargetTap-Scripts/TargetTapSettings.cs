public class TargetTapSettings : GameSettingsBase
/**
This class implements the GameSettingsBase interface. It stores and sets the various settings values.
*/
{
    public TargetTapGameLogic targetTapGame;
    public PlatformLogic platformLogic;

    // Setting Values
    public int numTargets;
    public float targetHeight;
    public float targetHoldTime;
    public float homeBoxHoldTime;
    public float targetSize;
    public float homeBoxSize;
    public int targetBias;
    public float frontPlatform;
    public float backPlatform;
    public float leftPlatform;
    public float rightPlatform;


    /**
    Index of Setting Values - hardcoded based on the order of the settings on the SettingsUI screen (excluding bias and platform
    because they are special card types).
    ONLY CHANGE IF YOU CHANGE THE ORDER OF THE SETTINGS ON THE SETTINGS SCREEN
    */
    private readonly int indexTargetHeight = 0;
    private readonly int indexTargetHoldTime = 1;
    private readonly int indexTargetSize = 2;
    private readonly int indexNumTargets = 3;
    private readonly int indexHomeBoxHoldTime = 4;
    private readonly int indexHomeBoxSize = 5;


    private void SetTargetHeight()
    /**
    A setter for the target height setting value.
    */
    {
        targetHeight = GameList.staticGameList[GameList.gameIndex].Settings[indexTargetHeight].SettingValue.value;
    }

    private void SetTargetHoldTime()
    /**
    A setter for the target hold time setting value.
    */
    {
        targetHoldTime = GameList.staticGameList[GameList.gameIndex].Settings[indexTargetHoldTime].SettingValue.value;
    }

    private void SetTargetSize()
    /**
    A setter for the target size setting value.
    */
    {
        targetSize = GameList.staticGameList[GameList.gameIndex].Settings[indexTargetSize].SettingValue.value;
        targetTapGame.targetLogic.ChangeTargetSize();
    }

    private void SetCircleSize()
    /**
    A setter for the platform sizes setting values. Calls the methods from class PlatformLogic to set the platform size.
    */
    {
        frontPlatform = ((TargetTap)GameList.staticGameList[GameList.gameIndex]).SpecialCardPlatform.SettingValue.frontValue;
        backPlatform = ((TargetTap)GameList.staticGameList[GameList.gameIndex]).SpecialCardPlatform.SettingValue.backValue;
        leftPlatform = ((TargetTap)GameList.staticGameList[GameList.gameIndex]).SpecialCardPlatform.SettingValue.leftValue;
        rightPlatform = ((TargetTap)GameList.staticGameList[GameList.gameIndex]).SpecialCardPlatform.SettingValue.rightValue;
        platformLogic.SetFrontStep();
        platformLogic.SetBackStep();
        platformLogic.SetLeftStep();
        platformLogic.SetRightStep();
    }

    private void SetTargetBias()
    /**
    A setter for the target bias setting value.
    */
    {
        targetBias = ((TargetTap)GameList.staticGameList[GameList.gameIndex]).SpecialCardBias.SettingValue.value;
    }

    private void SetNumTargets()
    /**
    A setter for the number of targets setting value.
    */
    {
        numTargets = (int)GameList.staticGameList[GameList.gameIndex].Settings[indexNumTargets].SettingValue.value;
    }

    private void SetHomeBoxHoldTime()
    /**
    A setter for the HomeBox hold time setting value.
    */
    {
        homeBoxHoldTime = GameList.staticGameList[GameList.gameIndex].Settings[indexHomeBoxHoldTime].SettingValue.value;
    }

    private void SetHomeBoxSize()
    /**
    A setter for the HomeBox size setting value.
    */
    {
        homeBoxSize = GameList.staticGameList[GameList.gameIndex].Settings[indexHomeBoxSize].SettingValue.value;
        targetTapGame.homeBoxLogic.ChangeHomeBoxSize();
    }

    public override void SetSettings()
    /**
    Calls all of the setters for the settngs.
    */
    {
        SetNumTargets();
        SetTargetHoldTime();
        SetTargetHeight();
        SetTargetBias();
        SetHomeBoxHoldTime();
        SetTargetSize();
        SetHomeBoxSize();
        SetCircleSize();
    }
}