using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficJamGameManager : MonoBehaviour
{
    [SerializeField] private TrafficJamUIManager UIManager;
    [SerializeField] private VehicleSpawner leftSpawner, rightSpawner;
    [SerializeField] private EmergencyVehicleSpawner emergencySpawner;
    public TrafficJamSettings settings;
    public int leftAmount, leftPassed, leftSquished, leftDetoured, rightAmount, rightPassed, rightSquished, rightDetoured;
    public int TEMPGameTimeStartSec;
    private bool isPlaying;
    
    //TEMP: Serialize to view in editor
    [SerializeField]
    private float startTime;
    private int countdownTime;

    //public int leftSquished => leftAmount - leftPassed;
    //public int rightSquished => rightAmount - rightPassed;

    void Start()
    {
        SetUpTimer(TEMPGameTimeStartSec);
        StartGame(); // TEMP CODE: game should be started manually in final build
    }

    void Update()
    {
        float endTime = startTime + (settings.GameTime*60);
        if (isPlaying)
        {
           if (Time.fixedTime >= endTime)
            {
                EndGame();
            }
            UIManager.UpdateTimer(endTime - Time.fixedTime);

        }
    }

    [ContextMenu("Start Game")]
    public void StartGame()
    {
        //Reset game values
        startTime = Time.fixedTime + 3;
        Time.timeScale = 1;
        countdownTime = 3;
        isPlaying = false;

        // reset score
        ResetValues();
            
        UIManager.PausePanelActive = false;
        UIManager.EndPanelActive = false;
        UIManager.CountdownPanelActive = true;

        leftSpawner.gameObject.SetActive(false);
        rightSpawner.gameObject.SetActive(false);
        emergencySpawner.gameObject.SetActive(false);

        //Remove cars
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
        Time.timeScale = 0;

        UIManager.ShowEndResults(leftAmount, leftPassed, rightAmount, rightPassed);

        // Uncomment these if you wish to disable car spawning once the game ends
        //leftSpawner.gameObject.SetActive(false);
        //rightSpawner.gameObject.SetActive(false);
        //emergencySpawner.gameObject.SetActive(false);

        isPlaying = false;
    }

    // The timer raises or lowers when the operator/therapist adjusts the time, instead of completely resetting
    public void SetUpTimer(int timerLengthSeconds)
    {
        float endTime = startTime + timerLengthSeconds;
        UIManager.UpdateTimer(endTime - Time.fixedTime);
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
        while (countdownTime > 0)
        {
            UIManager.Countdown = countdownTime;
            AudioPlayer.Play(Sound.Countdown);
            countdownTime--;
            yield return new WaitForSeconds(1);
        }

        AudioPlayer.Play(Sound.GameStart);

        leftSpawner.gameObject.SetActive(true);
        rightSpawner.gameObject.SetActive(true);
        emergencySpawner.gameObject.SetActive(true);

        SetUpTimer((int)(settings.GameTime*60));
        UIManager.CountdownPanelActive = false;
        isPlaying = true;
        yield return null;
    }
    
    private void ResetValues()
    {
        leftAmount = 0;
        rightAmount = 0;
        leftPassed = 0;
        rightPassed = 0;
        leftSquished = 0;
        rightSquished = 0;
        leftDetoured = 0;
        rightDetoured = 0;
    }
}
