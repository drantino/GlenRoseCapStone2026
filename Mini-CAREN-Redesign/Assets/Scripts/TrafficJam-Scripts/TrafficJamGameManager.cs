using System.Collections;
using UnityEngine;

public class TrafficJamGameManager : MonoBehaviour
{
    [SerializeField] private TrafficJamUIManager UIManager;
    public int leftAmount, leftPassed, rightAmount, rightPassed;
    
    //TEMP: Serialize to view in editor
    [SerializeField]
    private float endTime, startTime;
    private int countdownTime;

    void Update()
    {
        if (Time.fixedTime >= endTime)
        {
            EndGame();
        }
        //UIManager.UpdateTimer(endTime - Time.fixedTime);
        
    }

    [ContextMenu("Start Game")]
    public void StartGame()
    {
        //Reset game values
        startTime = Time.fixedTime;
        Time.timeScale = 1;
        countdownTime = 3;
        //TODO: Remove cars

        StopAllCoroutines();
        StartCoroutine(StartingCountdown());
    }

    [ContextMenu("PauseGame")]
    public void PauseGame()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    } 

    [ContextMenu("EndGame")]
    public void EndGame()
    {
        //UIManager.ShowEndPanel((float)leftAmount, (float)leftPassed, (float)rightAmount, (float)rightPassed);
        
    }

    // The timer raises or lowers when the operator/therapist adjusts the time, instead of completely resetting
    public void AdjustTimer(int timerLengthSeconds)
    {
        endTime = startTime + timerLengthSeconds;
        //UIManager.UpdateTimer(endTime - Time.fixedTime);
    }

    private IEnumerator StartingCountdown()
    {
        UIManager.CountdownPanelActive = true;
        while (countdownTime > 0)
        {
            Debug.Log(countdownTime);
            UIManager.Countdown = countdownTime;
            //TODO: Play a sound.
            countdownTime--;
            yield return new WaitForSeconds(1);
        }
        UIManager.CountdownPanelActive = false;
        yield return null;
    }
    
}
