using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
/**
Timer class which keeps track of the timer and timer logic.
*/
{
    private float timeSeconds;
    private int timeMinutes;

    public TextMeshProUGUI timerGUI;
    public TextMeshProUGUI timerStatisticsGUI;
    

    public void TimerStart()
    /**
    Initializes the values of the timer.
    */
    {
        timeSeconds = 0.0f;
        timeMinutes = 0;
    }

    public void TimerUpdate()
    /**
    The logic that updates the timer.
    Displays the timer values in minutes and seconds.
    */
    {
        timeSeconds += Time.deltaTime;
        string text;
        if (timeSeconds >= 60)
        {
            timeMinutes++;
            timeSeconds = 0;
        }

        if (timeSeconds < 10 && timeMinutes < 10)
        {
            text = "0" + timeMinutes + ":0" + (int)timeSeconds;
        }
        else if (timeSeconds < 10 && timeMinutes >= 10)
        {
            text = timeMinutes + ":0" + (int)timeSeconds;
        }
        else if (timeSeconds >= 10 && timeMinutes < 10)
        {
            text = "0" + timeMinutes + ":" + (int)timeSeconds;
        }
        else
        {
            text = timeMinutes + ":" + (int)timeSeconds;
        }
        timerGUI.text = text;
        timerStatisticsGUI.text = text;
    }
}
