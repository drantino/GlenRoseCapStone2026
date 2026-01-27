using UnityEngine;

public class TargetCollisionDetection : MonoBehaviour
/**
A class which holds the logic for detecting collisions with the target.
*/
{
    public TargetLogic targetLogic;


    void OnTriggerEnter(Collider other)
    /**
    Checks if either the left shoe collider or the right shoe collider are in contact with the target.
    This method also starts the audio and visual cues to show that the shoe collider and the target are in contact with one another.
    The TargetLogic.SetTargetCountdown() method is called to set the countdown for TargetHoldTime.
    */
    {
        if (other.CompareTag("LeftShoe") || other.CompareTag("RightShoe"))
        {
            targetLogic.StartAudioCue();
            targetLogic.SetTargetCountdown();
            targetLogic.TargetFade();
        }
    }

    void OnTriggerStay(Collider other)
    /**
    The logic for target collision and Target hold time. If either the left or right shoe collider are in contact with the target, 
    the target countdown will decrease and the visual and audio cues continue.
    If the target countdown reaches 0, the visual and audio cues stop and the TargetLogic.TargetHit() method is called.
    */
    {
        if (other.CompareTag("LeftShoe") || other.CompareTag("RightShoe"))
        {
            targetLogic.targetHoldCountdown -= Time.deltaTime;
            targetLogic.StartVisualCue();
            if (targetLogic.targetHoldCountdown < 0)
            {
                targetLogic.TargetHit();
                targetLogic.StopAudioCue();
            }
        }
    }

    void OnTriggerExit(Collider other)
    /**
    The logic for if the shoe collider an the target are no longer in contact with each other. The audio and visual cues both reset.
    */
    {
        targetLogic.StopAudioCue();
        targetLogic.ResetVisualCue();
        targetLogic.ResetTargetFade();
    }
}