using UnityEngine;
using UnityEngine.UI;

public class TargetTapGameLogic : GameBase
/**
This class implements the GameBase interface. It contains all the basic methods needed to create a game and has game logic
specific to the TargetTap game.
*/
{
    public TargetTapSettings settings;
    public TargetTapStatistcs statistics;
    public TargetLogic targetLogic;
    public HomeBoxLogic homeBoxLogic;
    public PlatformLogic platformLogic;
    public TimerScript timer;
    public GameObject homeBox;

    public bool gameStopped;
    public GameObject pauseUI;
    public GameObject endGameUI;

    public GameObject startButton;
    public GameObject pauseButton;
    public GameObject endButton;
    public GameObject skipButton;

    // TEMPORARY FOR TESTING
    public GameObject rightfoot;
    public GameObject leftfoot;


    public override void GameStart()
    /**
    The logic for when the TargetTapGame scene first loads. 
    The initial game state is set by setting all of the game settings, starting the time, and initializing any values for the Statsitics class.
    All of the UI elements are set to be in their initial game state.
    */
    {
        settings.SetSettings();
        homeBox.SetActive(false);
        targetLogic.TargetSpawn();
        statistics.targetsRemaining = settings.numTargets;
        statistics.InitializeHitTimeLists();
        timer.TimerStart();

        gameStopped = false;
        pauseUI.SetActive(false);
        endGameUI.SetActive(false);
        pauseButton.SetActive(true);
        startButton.SetActive(false);
    }

    public override void GamePause()
    /**
    Logic for if the "Pause Game" button is clicked.
    The PauseUI is activated, and some UI buttons are deactivated to prevent the game from progressings.
    Disables contact between the shoe colliders and the target or the HomeBox - depending on the game state when paused.
    */
    {
        gameStopped = true;
        pauseUI.SetActive(true);
        pauseButton.SetActive(false);
        startButton.SetActive(true);
        skipButton.GetComponent<Button>().enabled = false;
        if (homeBox.activeSelf == true)
        {
            homeBox.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            targetLogic.currentTarget.GetComponentInChildren<SphereCollider>().enabled = false;
        }
    }

    public override void GameUnpause()
    /**
    The logic for if the "Start Game" button is clicked.
    The PauseUI is deactivated, and the UI buttons are reactivated. The game state is reset to how it was before the game was paused.
    */
    {
        gameStopped = false;
        pauseUI.SetActive(false);
        pauseButton.SetActive(true);
        startButton.SetActive(false);
        skipButton.GetComponent<Button>().enabled = true;
        if (homeBox.activeSelf == true)
        {
            homeBox.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            targetLogic.currentTarget.GetComponentInChildren<SphereCollider>().enabled = true;
        }
    }

    public override void GameStop()
    /**
    The logic for if the "End Game" button is clicked, and the user confirms to end the game.
    The timer is stopped, the statistics are displayed on the projector, and the game is disabled from progressing.
    */
    {
        gameStopped = true;
        pauseUI.SetActive(false);
        skipButton.SetActive(false);
        startButton.SetActive(false);
        pauseButton.SetActive(false);
        endButton.SetActive(false);
        statistics.DisplayMedianEndScreen();
        
        endGameUI.SetActive(true);

        if (homeBox.activeSelf == true)
        {
            homeBox.SetActive(false);
        }
        else
        {
            Destroy(targetLogic.currentTarget);
        }
    }


    // TEMP FUNCTION
    public void TestMove()
    /**
    Allows for movement of the shoe models used during testing. They can be controlled using the WASD keys and arrow keys.
    */
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            leftfoot.transform.position = leftfoot.transform.position + new Vector3(0, 0, 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            leftfoot.transform.position = leftfoot.transform.position + new Vector3(-0.2f, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            leftfoot.transform.position = leftfoot.transform.position + new Vector3(0, 0, -0.2f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            leftfoot.transform.position = leftfoot.transform.position + new Vector3(0.2f, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rightfoot.transform.position = rightfoot.transform.position + new Vector3(0, 0, 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rightfoot.transform.position = rightfoot.transform.position + new Vector3(-0.2f, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rightfoot.transform.position = rightfoot.transform.position + new Vector3(0, 0, -0.2f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightfoot.transform.position = rightfoot.transform.position + new Vector3(0.2f, 0, 0);
        }
    }


    void Start()
    /**
    Function is called by Unity when the scene starts.
    */
    {
        GameStart();
    }

    void Update()
    /**
    Function is called by Unity every frame.
    If the game is not stopped (or paused), it will continue to update the statistics, the timer, and the settings.
    It also checks if there are any targets remaining - if there are none left, the GameStop() method is called.
    */ 
    {
        TestMove(); // Temporary: use to control object without motion capture

        if (!gameStopped)
        {
            // If Targets Left = 0;
            if (settings.numTargets - statistics.targetsHit == 0)
            {
                GameStop();
            }

            settings.SetSettings();
            statistics.DisplayStatistics();
            timer.TimerUpdate();
        }
    }
}