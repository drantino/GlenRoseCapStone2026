using UnityEngine;
using UnityEngine.UI;
using TMPro;


/* This script is used for the calibration UI button and the logic behind it
    It is used to open and close the calibration panels, as well as enable the start button
    once calibration has been completed
    It also contains some logic for if there are not enough markers in the scene
    to complete the calibration process 
*/

public class CalibrationButton : MonoBehaviour
{

    public bool IsCalibrationEnabled = false;   // Used to determine if calibration has already occured 

    // The rest of these are used for the UI logic for calibration
    public Button StartButton;
    public GameObject CalibrationPanel;
    public GameObject CalibrationPanel2;
    public TextMeshProUGUI myText;

    /*
    This start function is just used to set the calibrationtion button to be uninteractable
    */
    void Start()
    {
        if (IsCalibrationEnabled)
        {
            StartButton.interactable = true;
        }
        else
        {
            StartButton.interactable = false;
        }
    }

    // This is just used to enable the first UI panel for Calibration
    public void CalibrationButtonClicked()
    {
        CalibrationPanel.SetActive(true);
    }


    /*
    
    This function is used to close the calibration logic panel, and then open the next dialog panel

    */
    public void CloseCalibrationPanel()
    {
        if (CalibrationLogic.NotEnoughtMarkers) {
            myText.text = "Please try again, as there were not exactly 2 markers in the scene";
        }
        else{
            CalibrationPanel.SetActive(false);
            CalibrationPanel2.SetActive(true);
        }
    }

    /*
        Logic for what to do when the second calibration panel closes
    */

    public void CloseCalibrationPanel2()
    {
        CalibrationPanel2.SetActive(false);
        IsCalibrationEnabled = true;
        StartButton.interactable = true;
    }

    /*
      This function is only used for the target tap game, where when you are loaded into the game
      you do not need to set the start button  to be enabled as the game is already open
    */

    public void CloseCalibrationPanel2_empty(){
        CalibrationPanel2.SetActive(false);
        IsCalibrationEnabled = true;
    }

}
