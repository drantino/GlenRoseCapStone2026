using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficJamGameManager : MonoBehaviour
{
    [SerializeField] private TrafficJamUIManager UIManager;
    public int leftAmount, leftPassed, rightAmount, rightPassed;
    
    //TEMP: Serialize to view in editor
    [SerializeField]
    private float endTime, startTime;
    private int countdownTime;

    [SerializeField] private List<GameObject> VechicleList = new List<GameObject>();

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

    //AddVechicleList: Needs to be connected to VechileSpawner
    public void AddToVechicleList(GameObject instantiatedVechicle)
    {
        VechicleList.Add(instantiatedVechicle);
    }
    //RemoveVechicleList: Needs to be connect to VechileSpawner, which is connected to VechileDespawner
    public void RemoveFromVechicleList(GameObject Vechicle)
    {
        VechicleList.Remove(Vechicle);
    }
    //ResetVechileList: 
    [ContextMenu("Reset Vechicles")]
    private void ResetVechicleList()
    {
        foreach (GameObject vech in VechicleList)
        {
            Destroy(vech);
        }
        VechicleList.Clear();
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
