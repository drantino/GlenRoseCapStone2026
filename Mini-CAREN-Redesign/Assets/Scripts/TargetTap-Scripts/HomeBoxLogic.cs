using UnityEngine;

public class HomeBoxLogic : MonoBehaviour
/**
A class which holds various functions for the center home box.
*/
{
    public TargetTapGameLogic targetTapGame;
    public GameObject homeBox;

    private float homeBoxCountdown;
    private bool rightFootInBox = false;
    private bool leftFootInBox = false;


    public void HomeBoxHit()
    /**
    The logic for when the HomeBox is hit for x number of seconds, where x is set in the settings.
    Calls TargetLogic.TargetSpawn() to the next target and hides the HomeBox to prevent multiple targets from spawning.
    */
    {
        leftFootInBox = false;
        rightFootInBox = false;
        targetTapGame.targetLogic.TargetSpawn();
        homeBox.SetActive(false);
    }
    
    public void HomeBoxSpawn()
    /**
    The logic for unhiding the HomeBox when the player successfully hits the target.
    First checks to see if there are any TargetsRemaining, if yes, then the HomeBox will be unhidden.
    */
    {
        if (targetTapGame.statistics.targetsRemaining > 0)
        {
            homeBox.SetActive(true);
        }
    }

    public void ChangeHomeBoxSize()
    /**
    The logic which changes the size of the HomeBox. The size is controlled by the settings
    */
    {
        float x = targetTapGame.settings.homeBoxSize;
        float z = 0.1f + 0.5f * (targetTapGame.settings.homeBoxSize - 0.5f);
        homeBox.transform.localScale = new Vector3(x, 0.01f, z);
    }

    void OnTriggerEnter(Collider other)
    /**
    The logic for detecting if there is a collision with the HomeBox.
    Checks if the left shoe collider is in contact with the HomeBox and sets the leftFootInBox condition to true. 
    Does the same for the right shoe collider.
    Also sets the homeBoxCountdown value according to the HomeBoxHoldTime set in the settings.
    */
    {
        homeBoxCountdown = targetTapGame.settings.homeBoxHoldTime;
        if (other.CompareTag("LeftShoe"))
        {
            leftFootInBox = true;
        }
        if(other.CompareTag("RightShoe"))
        {
            rightFootInBox = true;
        }
    }

    void OnTriggerStay(Collider other)
    /**
    Logic for the HomeBox hold time. 
    Checks to see if both the leftFootInBox and rightFootInBox conditions are true, if yes, the homeBoxCountdown decreases.
    If the homeBoxCountdown reachers 0, HomeBoxHit() is called.
    */
    {
       if(leftFootInBox && rightFootInBox)
       {
            homeBoxCountdown -= Time.deltaTime;
            if(homeBoxCountdown < 0)
            {
                HomeBoxHit();
            }
       }
    }

    void OnTriggerExit(Collider other)
    /**
    The logic for if either the left shoe collider or right shoe collider are no longer in contact with the HomeBox.
    If they are not in contact, it will set the leftFootInBox and/or rightFootInBox conditions to false.
    */
    {
        if(other.CompareTag("LeftShoe"))
        {
            leftFootInBox = false;
        }
        if(other.CompareTag("RightShoe"))
        {
            rightFootInBox = false;
        }
    }
}