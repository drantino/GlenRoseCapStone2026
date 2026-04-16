using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TrafficJamGameManager : MonoBehaviour
{
    [SerializeField] private TrafficJamUIManager UIManager;
    [SerializeField] private VehicleSpawner leftSpawner, rightSpawner;
    [SerializeField] private VehicleSpawnController vehicleSpawnController;
    [SerializeField] private EmergencyVehicleSpawner emergencySpawner;
    public TrafficJamSettings settings;
    public int leftAmount, leftPassed, leftSquished, leftDetoured, rightAmount, rightPassed, rightSquished, rightDetoured;

    private bool isPlaying;
    
    //TEMP: Serialize to view in editor
    [SerializeField]
    private float startTime;

    //TEMP: Serialize to view in editor
    [SerializeField]
    private int countdownTime;


    public AudioLoop[] audioToMuteOnPause;
    public AudioMixer audioMixer;

    public bool pausible = true;

    void Start()
    {
        SetUpTimer(Mathf.RoundToInt(settings.GameTime * 60));
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

        // audio
        AudioListener.volume = Mathf.Clamp(settings.MasterVolume, 0f, 1f); // for safety, volume is clamped to 100%
    }

    // this function is called when the start game button is pressed
	[ContextMenu("Start Game")]
    public void StartGame()
    {
        //Reset game values
        startTime = Time.fixedTime + 3;
        countdownTime = 3;
        isPlaying = false;

        UnPause();

        // reset score
        ResetValues();

		// reset objects in scene
		vehicleSpawnController.ResetValues();

		UIManager.PausePanelActive = false;
        UIManager.EndPanelActive = false;
        UIManager.CountdownPanelActive = true;
        UIManager.RunTimeStatisicsPanel = true;

        vehicleSpawnController.spawningEnabled = false;
        emergencySpawner.gameObject.SetActive(false);

        //Remove cars
        ResetVechicleList();

        StopAllCoroutines();
        StartCoroutine(StartingCountdown());

        pausible = true;
    }

    // this function is called when the pause button on the settings page is pressed
    [ContextMenu("PauseGame")]
    public void PauseGame()
    {
        if (!pausible) return;

        if (Time.timeScale == 0)
        {
            // unpause
            UnPause();
            UIManager.PausePanelActive = false;
        }
        else
        {
            // pause
            Pause();
            UIManager.PausePanelActive = true;
		}
    }

    private void Pause()
    {
		Time.timeScale = 0;

		foreach (AudioLoop audioLoop in audioToMuteOnPause)
		{
			audioLoop.audioSource.mute = true;
		}
	}

    private void UnPause()
    {
		Time.timeScale = 1;

		foreach (AudioLoop audioLoop in audioToMuteOnPause)
		{
			audioLoop.audioSource.mute = false;
		}
	}

    // this function is called once the timer reaches 0
    [ContextMenu("EndGame")]
    public void EndGame()
    {
        Pause();

        UIManager.ShowEndResults(leftAmount, leftPassed, rightAmount, rightPassed);

        // Uncomment these if you wish to disable car spawning once the game ends
        //leftSpawner.gameObject.SetActive(false);
        //rightSpawner.gameObject.SetActive(false);
        //emergencySpawner.gameObject.SetActive(false);

        isPlaying = false;

        // Save Round
        TrafficJamRoundData roundData = new TrafficJamRoundData
        {
            roundLength = settings.GameTime,
            leftFootPassed = leftPassed,
            leftFootSquished = leftSquished,
            leftFootDetoured = leftDetoured,
            rightFootPassed = rightPassed,
            rightFootSquished = rightSquished,
            rightFootDetoured = rightDetoured,

            settingsData = new TrafficJamSettingsData
            {
                heightThreshold = settings.HeightThreshold,
                carSpeed = settings.CarSpeed,
                carSpawnInterval = settings.CarSpawnInterval,
                carLength = settings.CarLength,
                carDetour = settings.CarDetour,
				emergencyVehicleSideBias = settings.EmergencyVehicleBias,
				emergencyVehicleActive = settings.EmergencyVehicleActive,
			}
        };

        TrafficJamSaveSystem.AddRoundData(roundData);

        AudioPlayer.Play(Sound.RoundEnd);

        pausible = false;
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
			switch (countdownTime)
			{
				case 3:
					AudioPlayer.Play(Sound.Three);
					break;
				case 2:
					AudioPlayer.Play(Sound.Two);
					break;
				case 1:
					AudioPlayer.Play(Sound.One);
					break;
				default:
					break;
			}

			UIManager.Countdown = countdownTime;
            countdownTime--;

            yield return new WaitForSeconds(1);
        }

        AudioPlayer.Play(Sound.GameStart);

        vehicleSpawnController.spawningEnabled = true;
        emergencySpawner.gameObject.SetActive(true);

        vehicleSpawnController.ForceVehicleSpawn();

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
