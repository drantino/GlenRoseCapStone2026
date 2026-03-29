using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlusMinusButtons : MonoBehaviour
{

    public TextMeshProUGUI InputFieldText;
    public string platform;

    /*
        Increases the value in the input field by a predefined increment, ensuring it does not exceed the maximum limit.
        Updates the corresponding platform setting value after adjustment.
    */
    public void PlusButtonClicked()
    {

        float currentValue = float.Parse(InputFieldText.text);

        if (currentValue + (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.SettingValue.increment > (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.SettingValue.max)
        {
            currentValue = (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.SettingValue.max;
        }
        else
        {
            currentValue = currentValue + (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.SettingValue.increment;
        }

        InputFieldText.text = currentValue.ToString("F1");
        SetSettingsValue();
    }

    public void MinusButtonClicked()
    {
        float currentValue = float.Parse(InputFieldText.text);

        if (currentValue - (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.SettingValue.increment < (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.SettingValue.min)
        {
            currentValue = (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.SettingValue.min;
        }
        else
        {
            currentValue = currentValue - (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.SettingValue.increment;
        }

        InputFieldText.text = currentValue.ToString("F1");
        SetSettingsValue();
    }

    private void SetSettingsValue()
    {
        if (platform == "Front")
        {
            (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.SettingValue.frontValue = float.Parse(InputFieldText.text);
        }
        else if (platform == "Back")
        {
            (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.SettingValue.backValue = float.Parse(InputFieldText.text);
        }
        else if (platform == "Left")
        {
            (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.SettingValue.leftValue = float.Parse(InputFieldText.text);
        }
        else if (platform == "Right")
        {
            (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.SettingValue.rightValue = float.Parse(InputFieldText.text);
        }
    }
}
