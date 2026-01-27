using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TargetBiasSlider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Slider slider;
    public TextMeshProUGUI leftBiasText;
    public TextMeshProUGUI rightBiasText;
    public int settingIndex;

    /*
        Initializes the slider to update the left and right bias text values and the corresponding game setting
        whenever the slider's value changes. The value is rounded to the nearest integer.
    */

    void Start()
    {
        slider.onValueChanged.AddListener((value) =>
        {
            leftBiasText.text = (100 - value).ToString("F0");
            rightBiasText.text = value.ToString("F0");
            float roundedValue = Mathf.Round(slider.value * 100f) / 100f;
            ((TargetTap)GameList.staticGameList[GameList.gameIndex]).SpecialCardBias.SettingValue.value = (int)roundedValue;
        });
    }
    
}

