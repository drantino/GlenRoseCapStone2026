using UnityEngine;

[System.Serializable]
public struct SettingFloat {
    public float value;
    public float min;
    public float max;
    public string units;
    public bool LiveSetting;

}

[System.Serializable]
public struct SettingInt
{
    public int value;
    public int min;
    public int max;
    public string units;
}

[System.Serializable]
public struct SettingPlatform
{
    public float frontValue;
    public float backValue;
    public float leftValue;
    public float rightValue;
    public float min;
    public float max;
    public float increment;
}

[System.Serializable] public struct CardFloat
{
    public string cardName;
    public string cardDescription;
    public Sprite cardImage;
    public SettingFloat SettingValue;
    public bool sliderisWholeNumber;

}

[System.Serializable] public struct CardInt
{
    public string cardName;
    public string cardDescription;
    public Sprite cardImage;
    public SettingInt SettingValue;
    public GameObject cardPrefab;
}

[System.Serializable] public struct CardPlatform
{
    public string cardName;
    public string cardDescription;
    public Sprite cardImage;
    public SettingPlatform SettingValue;
    public GameObject cardPrefab;
}