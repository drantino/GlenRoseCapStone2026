using UnityEngine;

public class TrafficJamGameManager : MonoBehaviour
{
    public float leftAmount, leftPassed, rightAmount, rightPassed;
    private float leftScore, rightScore;
    
    //TEMP: Serialize to view in editor
    [SerializeField]
    private float endTime, startTime;

    void Update()
    {
        if (Time.fixedTime >= endTime)
        {
            EndGame();
        }
    }

    public void StartGame()
    {
        //Reset game values
        startTime = Time.fixedTime;
        Time.timeScale = 1;
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
        leftScore = 100;
        rightScore = 100;

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
            
        
        //TODO: Send the scores to the UI to display

        //TEMP: Debug.Log to see the scores
        Debug.Log($"Left score: {leftScore}%, Right score: {rightScore}%"); 
        
    }

    public void AdjustTimer(int timerLengthSeconds)
    {
        endTime = startTime + timerLengthSeconds;
    }
    
}
