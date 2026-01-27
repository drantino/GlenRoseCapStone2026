using UnityEngine;

public class TargetSettings : MonoBehaviour
{
    public int numTargets;
    public float[] circleSize; // L, R, T, B
    public float targetHeight;
    public float targetHoldTime;
    public float homeboxHoldTime;
    public float targetSize;
    public float homeboxSize;
    

    /*
        This script manages the target settings for the game, including number of targets, circle size,
        target height, hold times, and sizes. It provides methods to set each of these parameters.
    */

    void Start()
    {
        // Set the default values for the settings
        setNumTargets(50);
        setCircleSize(0.5f, 0.5f, 0.5f, 0.5f);
        setTargetHeight(1f);
        setTargetHoldTime(0.5f);
        setHomeboxHoldTime(1f);
        setTargetSize(0.2f);
        setHomeboxSize(1.5f);
    }


    // TODO: Need to connect settings UI screen to script





    public void setNumTargets(int numTargetsInput){
        numTargets = numTargetsInput;
    }

    public void setCircleSize(float left, float right, float top, float bottom) // or have them enter an array instead?
    {
        circleSize = new float[4] {left, right, top, bottom};
    }

    public void setTargetHeight(float targetHeightInput)
    {
        targetHeight = targetHeightInput;
    }

    public void setTargetHoldTime(float targetHoldTimeInput)
    {
        targetHoldTime = targetHoldTimeInput;
    }

    public void setHomeboxHoldTime(float homeboxHoldTimeInput)
    {
        homeboxHoldTime = homeboxHoldTimeInput;
    }

    public void setTargetSize(float targetSizeInput)
    {
        targetSize = targetSizeInput;
    }

    public void setHomeboxSize(float homeboxSizeInput)
    {
        homeboxSize = homeboxSizeInput;
    }

    // TODO: may potentially need to add getters depending on how values are accessed? To prevent from accidentally modifying value
}
