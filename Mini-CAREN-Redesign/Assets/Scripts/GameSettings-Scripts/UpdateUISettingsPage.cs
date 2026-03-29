using UnityEngine;
using TMPro;
public class UpdateUISettingsPage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    /*
        This script updates the UI settings page with the current target settings.
        It retrieves values from input fields and updates the TargetSettings component accordingly.
    */
    public TMP_InputField NumberOfTargets_InputField;
    public TMP_InputField TargetHeight_InputField;
    public TMP_InputField TargetHoldTime_InputField;
    public TMP_InputField TargetSize_InputField;


    public TMP_InputField CircleSize_TL_InputField; // TL = Top Left
    public TMP_InputField CircleSize_TR_InputField; // TR = Top Right
    public TMP_InputField CircleSize_BL_InputField; // BL = Bottom Left
    public TMP_InputField CircleSize_BR_InputField; // BR = Bottom Right

    public TMP_InputField HomeboxHoldTime_InputField;
    public TMP_InputField HomeboxSize_InputField;


    public TargetSettings targetSettings;


    public void UpdateNumberOfTargets()
    {
        int numberOfTargets = int.Parse(NumberOfTargets_InputField.text);
        targetSettings.setNumTargets(numberOfTargets);
    }
    public void UpdateTargetHeight()
    {
        float targetHeight = float.Parse(TargetHeight_InputField.text);
        targetSettings.setTargetHeight(targetHeight);
    }
    public void UpdateTargetHoldTime()
    {
        float targetHoldTime = float.Parse(TargetHoldTime_InputField.text);
        targetSettings.setTargetHoldTime(targetHoldTime);
    }
    public void UpdatePlatformSize()
    {
        float targetSize = float.Parse(TargetSize_InputField.text);
        targetSettings.setTargetSize(targetSize);
    }


    public void UpdateUI() 
    {
        UpdateNumberOfTargets();
        UpdateTargetHeight();
        UpdateTargetHoldTime();
        UpdatePlatformSize();
    }

    void Start()
    {
        
    }

    
}
