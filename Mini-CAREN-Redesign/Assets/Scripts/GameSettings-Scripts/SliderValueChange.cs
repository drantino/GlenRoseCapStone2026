using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderValueChange : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Slider slider;
    public TextMeshProUGUI valueText;
    public int settingIndex;
    
    /*
        Initializes the slider to update the displayed value and the corresponding game setting
        whenever the slider's value changes. The value is rounded to two decimal places.
    */
    void Start() {
        slider.onValueChanged.AddListener((value)=>{
            valueText.text = value.ToString("F2");
            float roundedValue = Mathf.Round(slider.value * 100f) / 100f;
            GameList.staticGameList[GameList.gameIndex].Settings[settingIndex].SettingValue.value = roundedValue;
        });
    }
    
}
