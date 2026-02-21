using UnityEngine;
using UnityEngine.UI;

public class BoolToggle : MonoBehaviour
{
    public Toggle activeToggle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeToggle.onValueChanged.AddListener((value) =>
        {
            ((TrafficJam)GameList.staticGameList[GameList.gameIndex]).SpecialCardEmergencyVehicle.isActive = value;
        });
    }
}
