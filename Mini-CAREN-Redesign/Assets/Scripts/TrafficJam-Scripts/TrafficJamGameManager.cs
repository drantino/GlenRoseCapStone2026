using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficJamGameManager : MonoBehaviour
{
    [SerializeField] private TrafficJamUIManager UIManager;
    [SerializeField] private VehicleSpawner leftSpawner, rightSpawner;
    [SerializeField] private EmergencyVehicleSpawner emergencySpawner;
    public int leftAmount, leftPassed, rightAmount, rightPassed;

    //currentCarsInLane variables are meant for both vehicle spawners and emergency vehicle spawner to reference
    ///public float currentCarsInLaneLeft, currentCarsInLaneRight, currentCarsInLaneEmergency;
    
    //TEMP: Serialize to view in editor
    [SerializeField]
    private float endTime, startTime;
    private int countdownTime;

    void Update()
    {
        if (Time.fixedTime >= endTime)
        {
            //EndGame();
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
        ResetVechicleList();

        StopAllCoroutines();
        StartCoroutine(StartingCountdown());

        
    }

    [ContextMenu("PauseGame")]
    public void PauseGame()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            UIManager.PausePanelActive = false;
        }
        else
        {
            UIManager.PausePanelActive = true;
            Time.timeScale = 0;
        }
            
    } 

    [ContextMenu("EndGame")]
    public void EndGame()
    {
        UIManager.ShowEndResults(leftAmount, leftPassed, rightAmount, rightPassed);
        
    }

    // The timer raises or lowers when the operator/therapist adjusts the time, instead of completely resetting
    public void AdjustTimer(int timerLengthSeconds)
    {
        endTime = startTime + timerLengthSeconds;
        //UIManager.UpdateTimer(endTime - Time.fixedTime);
    }

    [ContextMenu("Reset Vechicles")]
    private void ResetVechicleList()
    {
        leftSpawner.ResetVehicleList();
        rightSpawner.ResetVehicleList();
        emergencySpawner.ResetVehicleList();
    }

    private IEnumerator StartingCountdown()
    {
        leftSpawner.gameObject.SetActive(false);
        rightSpawner.gameObject.SetActive(false);
        emergencySpawner.gameObject.SetActive(false);

        UIManager.CountdownPanelActive = true;
        while (countdownTime > 0)
        {
            UIManager.Countdown = countdownTime;
            //TODO: Play a sound.
            countdownTime--;
            yield return new WaitForSeconds(1);
        }

        leftSpawner.gameObject.SetActive(true);
        rightSpawner.gameObject.SetActive(true);
        emergencySpawner.gameObject.SetActive(true);

        UIManager.CountdownPanelActive = false;
        yield return null;
    }
    
}
