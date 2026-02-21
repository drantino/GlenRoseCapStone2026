using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoolFloatSlider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Slider slider;
    public Toggle activeToggle;
    public TextMeshProUGUI leftBiasText;
    public TextMeshProUGUI rightBiasText;
    public int settingIndex;

    /*
        Initializes the slider to update the left and right bias text values and the corresponding game setting
        whenever the slider's value changes. The value is rounded to the nearest integer.
        Initializes the toggle to update the corresponding game setting
    */

    void Start()
    {
        slider.onValueChanged.AddListener((value) =>
        {
            leftBiasText.text = (100 - value).ToString("F0");
            rightBiasText.text = value.ToString("F0");
            float roundedValue = Mathf.Round(slider.value * 100f) / 100f;
            ((TrafficJam)GameList.staticGameList[GameList.gameIndex]).SpecialCardEmergencyVehicle.SettingValue.value = (int)roundedValue;
        });
        activeToggle.onValueChanged.AddListener((value) =>
        {
            ((TrafficJam)GameList.staticGameList[GameList.gameIndex]).SpecialCardEmergencyVehicle.isActive = value;
        });
    }
}
