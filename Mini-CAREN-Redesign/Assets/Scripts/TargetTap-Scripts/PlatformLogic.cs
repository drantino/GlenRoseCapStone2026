using UnityEngine;

public class PlatformLogic : MonoBehaviour
/** 
A class which changes the size of the platform in the front, back, left, right and directions.
Uses the values set in Settings and adjusts the platform size accordingly.
*/
{
    public TargetTapGameLogic targetTapGame;
    public GameObject platformFrontRight;
    public GameObject platformFrontLeft;
    public GameObject platformBackLeft;
    public GameObject platformBackRight;


    public void SetFrontStep()
    /**
    The logic for change the size of the front part of the platform.
    */
    {
        platformFrontRight.transform.localScale = new Vector3(targetTapGame.settings.frontPlatform, platformFrontRight.transform.localScale.y, platformFrontRight.transform.localScale.z);
        platformFrontLeft.transform.localScale = new Vector3(platformFrontLeft.transform.localScale.x, platformFrontLeft.transform.localScale.y, targetTapGame.settings.frontPlatform);
    }
    public void SetBackStep()
    /**
    The logic for change the size of the back part of the platform.
    */
    {
        platformBackLeft.transform.localScale = new Vector3(targetTapGame.settings.backPlatform, platformBackLeft.transform.localScale.y, platformBackLeft.transform.localScale.z);
        platformBackRight.transform.localScale = new Vector3(platformBackRight.transform.localScale.x, platformBackRight.transform.localScale.y, targetTapGame.settings.backPlatform);
    }
    public void SetLeftStep()
    /**
    The logic for change the size of the left part of the platform.
    */
    {
        platformFrontLeft.transform.localScale = new Vector3(targetTapGame.settings.leftPlatform, platformFrontLeft.transform.localScale.y, platformFrontLeft.transform.localScale.z);
        platformBackLeft.transform.localScale = new Vector3(platformBackLeft.transform.localScale.x, platformBackLeft.transform.localScale.y, targetTapGame.settings.leftPlatform);
    }
    public void SetRightStep()
    /**
    The logic for change the size of the right part of the platform.
    */
    {
        platformFrontRight.transform.localScale = new Vector3(platformFrontRight.transform.localScale.x, platformFrontRight.transform.localScale.y, targetTapGame.settings.rightPlatform);
        platformBackRight.transform.localScale = new Vector3(targetTapGame.settings.rightPlatform, platformBackRight.transform.localScale.y, platformBackRight.transform.localScale.z);
    }
}