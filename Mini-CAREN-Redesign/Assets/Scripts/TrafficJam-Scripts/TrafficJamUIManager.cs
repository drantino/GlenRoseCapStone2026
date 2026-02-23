using TMPro;
using UnityEngine;

public class TrafficJamUIManager : MonoBehaviour
{
    // For projector display: l+r cars passed, l+r cars squished, time '[min] : [sec]',
    // pause panel, end panel, l+r results (ratio and percentage),
    // countdown panel, countdown text

    [SerializeField] private TrafficJamGameManager gameManager;

    [SerializeField] private TextMeshProUGUI leftCarsPassedText, rightCarsPassedText, leftCarsSquishedText, rightCarsSquishedText, 
        timerText, leftRatioText, leftPercentageText, rightRatioText, rightPercentageText, countdownText;
    [SerializeField] private GameObject countdownPanel, pausePanel, endPanel;

    // Number Properties
    public int LeftCarsPassed { set {leftCarsPassedText.text = $"{value}";}}
    public int LeftCarsSquished { set {leftCarsSquishedText.text = $"{value}";}}
    public int RightCarsPassed { set {rightCarsPassedText.text = $"{value}";}}
    public int RightCarsSquished { set {rightCarsSquishedText.text = $"{value}";}}
    public int Countdown { set {countdownText.text = $"{value}";}}

    // Panel Properties
    public bool CountdownPanelActive { set {countdownPanel.SetActive(value);} }
    public bool PausePanelActive { set {pausePanel.SetActive(value);} }
    public bool EndPanelActive { set {endPanel.SetActive(value);} }


	private void Update()
	{
        //LeftCarsPassed = TrafficJamScoreKeeper.leftFootVehiclesPassed;
        //LeftCarsSquished = TrafficJamScoreKeeper.leftFootVehiclesSquished;
        //RightCarsPassed = TrafficJamScoreKeeper.rightFootVehiclesPassed;
        //RightCarsSquished = TrafficJamScoreKeeper.rightFootVehiclesSquished;
        LeftCarsPassed = gameManager.leftPassed;
        LeftCarsSquished = gameManager.leftSquished;
        RightCarsPassed = gameManager.rightPassed;
        RightCarsSquished = gameManager.rightSquished;

		//UpdateTimer(Timer.GetTime());
	}

	public void ResetText()
    {
        leftCarsPassedText.text = "0";
        leftCarsSquishedText.text = "0";
        leftRatioText.text = "0 / 0";
        leftPercentageText.text = "100%";

        rightCarsPassedText.text = "0";
        rightCarsSquishedText.text = "0";
        rightRatioText.text = "0 / 0";
        rightPercentageText.text = "100%";

    }

    public void ShowEndResults(float leftAmount, float leftPassed, float rightAmount, float rightPassed)
    {
        float leftScore = 100;
        float rightScore = 100;

        if (leftAmount > 0)
        {
            leftScore *= leftPassed/leftAmount;
            leftScore = Mathf.Round(leftScore);
        }
        if (rightAmount > 0)
        {
            rightScore *= rightPassed/rightAmount;
            rightScore = Mathf.Round(rightScore);
        }

        leftRatioText.text = $"{leftPassed} / {leftAmount}";
        leftPercentageText.text = $"{leftScore}%";
        rightRatioText.text = $"{rightPassed} / {rightAmount}";
        rightPercentageText.text = $"{rightScore}%";

        endPanel.SetActive(true);
    }

    public void UpdateTimer(float timeRemainingSec)
    {
        float minutes;
        float seconds;

        timeRemainingSec = Mathf.Round(timeRemainingSec);
        seconds = timeRemainingSec % 60;
        minutes = Mathf.Floor(timeRemainingSec / 60);

        if (seconds < 10)
            timerText.text = $"{minutes} : 0{seconds}";
        else
            timerText.text = $"{minutes} : {seconds}";
    }
}
