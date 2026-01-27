using UnityEngine;
using System;
using System.Collections.Generic;

/*  This script is used to handle the calibration logic for the OptiTrack markers
    It is used to determine which marker is on the left foot and which is on the right
    It also calculates the center position between the two markers
    It also contains logic for recalibration if needed
*/

public class CalibrationLogic : MonoBehaviour
{
    public OptitrackStreamingClient optitrackClient;
    public static List<Int32> markerIds;

    // IDs for the left and right foot markers
    public static Int32 leftFootMarkerId = 0;
    public static Int32 rightFootMarkerId = 0;

    // List to store the positions of the markers
    public static List<Vector3> markerPositions = new List<Vector3>();    
    public static Vector3 leftFootPosition;
    public static Vector3 rightFootPosition;

    // Center position between the two markers
    public static Vector3 centerPosition = new Vector3(0, 0, 0);

    // Flags to indicate if there are not enough markers or if recalibration has occurred
    public static bool NotEnoughtMarkers = false;


    // this varialbe is used in the optitrackStreamingClient script to determine if you need to remove the existing markers for recalibration
    public static bool Recalibratebool = false; 
    

    // UI element to show error message if not enough markers are detected
    public GameObject Recalibrate_Error_UI;


    /* This function is used to perform the calibration logic
    It checks the positions of the markers and assigns
    the left and right foot marker IDs based on their x positions
    It also calculates the center position between the two markers
    */

    public void Calibrate()
    {
        markerIds = new List<Int32>();
        markerPositions.Clear();


        // Get the latest marker states from the OptiTrack client
        foreach (KeyValuePair<Int32, OptitrackMarkerState> markerEntry in optitrackClient.m_latestMarkerStates)
        {
            markerIds.Add(markerEntry.Key);
            markerPositions.Add(markerEntry.Value.Position);

        }

        // determine which foot is on the left and which is on the right
        // this line of code assumes that there are atleast two markers in the scene

        if(markerPositions.Count != 2) {
            NotEnoughtMarkers = true; 
        }
        else{
            NotEnoughtMarkers = false;

        if (markerPositions[0].x < markerPositions[1].x)
        {
            leftFootMarkerId = markerIds[0];
            rightFootMarkerId = markerIds[1];
            leftFootPosition = markerPositions[0];
            rightFootPosition = markerPositions[1];
        }
        else
        {
            // right foot
            rightFootMarkerId = markerIds[0];
            leftFootMarkerId = markerIds[1];
            rightFootPosition = markerPositions[0];
            leftFootPosition = markerPositions[1];
        }

        // calculate the center position between the two feet
        centerPosition = (leftFootPosition + rightFootPosition) / 2;
        //TRY CHANGING TO centerPosition += (leftFootPosition + rightFootPosition) / 2;
        }

    }


    /* This function is used to perform recalibration logic
       It clears the existing marker positions and IDs
       It then re-fetches the latest marker states from the OptiTrack client
       It performs the same logic as the Calibrate function to determine left and right foot markers
       It also sets a flag to indicate that recalibration has occurred
    */
    
    public void Recalibrate(){

        markerIds = new List<Int32>();

        markerPositions.Clear();
        // Get the latest marker states from the OptiTrack client
        foreach (KeyValuePair<Int32, OptitrackMarkerState> markerEntry in optitrackClient.m_latestMarkerStates)
        {
            markerIds.Add(markerEntry.Key);
            markerPositions.Add(markerEntry.Value.Position);
        }

        if (markerPositions.Count != 2) {
            NotEnoughtMarkers = true ;
            Recalibrate_Error_UI.SetActive(true);
        }
        else {
            NotEnoughtMarkers= false ;
            Recalibrate_Error_UI.SetActive(false);

            if (markerPositions[0].x < markerPositions[1].x)
            {
                leftFootMarkerId = markerIds[0];
                rightFootMarkerId = markerIds[1];
                leftFootPosition = markerPositions[0];
                rightFootPosition = markerPositions[1];
            }
            else
            {
                // right foot
                rightFootMarkerId = markerIds[0];
                leftFootMarkerId = markerIds[1];
                rightFootPosition = markerPositions[0];
                leftFootPosition = markerPositions[1];
            }

            centerPosition = (leftFootPosition + rightFootPosition) / 2;
            // TRY CHANGING TO centerPosition += (leftFootPosition + rightFootPosition) / 2;
            Recalibratebool = true;
        }
    }
   
}
