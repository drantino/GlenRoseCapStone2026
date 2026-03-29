using UnityEngine;

public class TargetLogic : MonoBehaviour
/**
Class that stores all the logic for the targets.
*/
{
    public TargetTapGameLogic targetTapGame;
    public HomeBoxLogic homeBoxLogic;
    public PlatformLogic platformLogic;
    public AudioSource slidingSound;
    public AudioSource kickSound;
    public GameObject target;
    public GameObject homeBox;

    // Current Target Information
    public GameObject currentTarget;
    public float currentTargetStartTime;
    public float currentTargetEndTime;
    public int currentTargetSection;
    private Renderer targetRenderer;

    public float targetHoldCountdown;


    public void TargetSpawn()
    /**
    Instantiates a new target object at a random location on the edge of the platform. 
    Uses the GenerateTargetLocation() method to determine its location.
    Sets the currentTargetStartTime to set the time the target spawned, calls SetTargetSection() to set which location the target is in,
    and gives the target access to the TargetLogic script.
    */
    {
        currentTarget = Instantiate(target, GenerateTargetLocation(), transform.rotation);
        TargetCollisionDetection temp = currentTarget.GetComponentInChildren<TargetCollisionDetection>();
        temp.targetLogic = this;
        currentTargetStartTime = Time.time;
        SetTargetSection();
        targetRenderer = currentTarget.GetComponentInChildren<Renderer>();
    }

    private Vector3 GenerateTargetLocation()
    /**
    Generates the location for the target to spawn.
    Uses TargetBias, TargetHeight, and Platform settings to limit the bounds.
    Returns the location of thr target as a Vector3 type.
    */
    {
        float x;
        float y;
        float z;

        // Use bias setting to influence which side the target will spawn.
        int random;
        int quadrant;

        // If bias slider is all the way to the left, targets will only spawn in the left two quadrants.
        if (targetTapGame.settings.targetBias == 0)
        {
            random = Random.Range(0, 2);
            if (random == 0)
            {
                quadrant = 2;
            }
            else
            {
                quadrant = 3;
            }
        }
        // If bias slider is all the way to the right, targets will only spawn in the right two quadrants.
        else if (targetTapGame.settings.targetBias == 100)
        {
            random = Random.Range(0, 2);
            if (random == 0)
            {
                quadrant = 1;
            }
            else
            {
                quadrant = 4;
            }
        }
        // Else, it will use the Random function to generate a random number and assign the target to a quadrant
        // Quadrants are based on the quadrants on a cartesian plane.
        else
        {
            random = Random.Range(0, 100);
            if (0 <= random && random < targetTapGame.settings.targetBias / 2)
            {
                quadrant = 1;
            }
            else if (targetTapGame.settings.targetBias / 2 <= random && random < targetTapGame.settings.targetBias)
            {
                quadrant = 4;
            }
            else if (targetTapGame.settings.targetBias <= random && random < (targetTapGame.settings.targetBias + (100 - targetTapGame.settings.targetBias) / 2))
            {
                quadrant = 2;
            }
            else
            {
                quadrant = 3;
            }
        }

        // Depending on which quadrant the target will spawn in, it will use the bounds of the platform corresponding to that quadrant.
        switch (quadrant)
        {
            case 1:
                x = Random.Range(0, targetTapGame.settings.rightPlatform);
                z = Mathf.Sqrt(Mathf.Pow(targetTapGame.settings.frontPlatform, 2) * (1 - Mathf.Pow(x, 2) / Mathf.Pow(targetTapGame.settings.rightPlatform, 2)));
                break;
            case 2:
                x = Random.Range(0, targetTapGame.settings.leftPlatform);
                z = Mathf.Sqrt(Mathf.Pow(targetTapGame.settings.frontPlatform, 2) * (1 - Mathf.Pow(x, 2) / Mathf.Pow(targetTapGame.settings.leftPlatform, 2)));
                x *= -1;
                break;
            case 3:
                x = Random.Range(0, targetTapGame.settings.leftPlatform);
                z = Mathf.Sqrt(Mathf.Pow(targetTapGame.settings.backPlatform, 2) * (1 - Mathf.Pow(x, 2) / Mathf.Pow(targetTapGame.settings.leftPlatform, 2)));
                x *= -1;
                z *= -1;
                break;
            default:
                x = Random.Range(0, targetTapGame.settings.rightPlatform);
                z = Mathf.Sqrt(Mathf.Pow(targetTapGame.settings.backPlatform, 2) * (1 - Mathf.Pow(x, 2) / Mathf.Pow(targetTapGame.settings.rightPlatform, 2)));
                z *= -1;
                break;
        }
        // Edge case - in case there is a dividing by 0 issue
        if (z is float.NaN)
        {
            z = 0f;
        }

        // Multiplying by a constant because the platform has been scaled down.
        x *= 0.57f;
        z *= 0.57f;

        // Uses the TargetHeight setting value to generate the height of the target.
        y = 0.2f + targetTapGame.settings.targetHeight + Random.Range(-0.05f, 0.05f);
        return new Vector3(x, y, z);
    }

    private void SetTargetSection()
    /**
    Sets the section of the current target.
    */
    {
        float x = currentTarget.transform.position.x;
        float z = currentTarget.transform.position.z;

        // Hard coded bounds that divide the sections.
        float frontBackLeftBound = -0.2f;
        float frontBackRightBound = 0.2f;
        float lateralUpperBound = 0.3f;
        float lateralLowerBound = -0.3f;

        if (x > frontBackRightBound && z > lateralUpperBound)
        {
            currentTargetSection = 0; // front-right
        }
        else if (x >= frontBackLeftBound && x <= frontBackRightBound && z > 0)
        {
            currentTargetSection = 1; // front
        }
        else if (x < frontBackLeftBound && z > lateralUpperBound)
        {
            currentTargetSection = 2; // front-left
        }
        else if (z >= lateralLowerBound && z <= lateralUpperBound && x < 0)
        {
            currentTargetSection = 3; // left
        }
        else if (x < frontBackLeftBound && z < lateralLowerBound)
        {
            currentTargetSection = 4; // back-left
        }
        else if (x >= frontBackLeftBound && x <= frontBackRightBound && z < 0)
        {
            currentTargetSection = 5; // back
        }
        else if (x > frontBackRightBound && z < lateralLowerBound)
        {
            currentTargetSection = 6; // back-right
        }
        else if (z >= lateralLowerBound && z <= lateralUpperBound && x > 0)
        {
            currentTargetSection = 7; // right
        }            
    }

    public void TargetHit()
    /**
    The logic for when the current target is successfully hit.
    Saves currentTargetEndTime so that hit time can be calculated.
    Deletes the current target object
    Updates the statistics by calling Statistics.UpdateStatistics().
    Spawns the HomeBox by calling HomeBoxLogic.HomeBoxSpawn().
    */
    {
        currentTargetEndTime = Time.time;
        Destroy(currentTarget);
        targetTapGame.statistics.UpdateStatistics();
        homeBoxLogic.HomeBoxSpawn();
    }

    public void TargetSkip()
    /**
    The logic for if the "Skip Target" button is clicked.
    */
    {
        if (homeBox.activeSelf == false)
        {
            targetTapGame.statistics.targetsSkipped++;
            Destroy(currentTarget);
            TargetSpawn();
        }
    }

    public void StartVisualCue()
    /**
    Starts the visual cue to show contact between the shoe collider and the target.
    Shrinks the target until Target Hold Time is reached or until it reaches a set size - this only shrinks the mesh, 
    the rigid body of the is still that same as it was initially.
    */
    {
        if (currentTarget.transform.GetChild(0).localScale.x > 0.5f)
        {
            currentTarget.transform.GetChild(0).localScale -= new Vector3(0.005f, 0.005f, 0.005f);  
        }
    }

    public void ResetVisualCue()
    /**
    Resets the visual cue if the shoe collider and the target are no longer in contact.
    Resets the size of the target to the value of TargetSize from the settings.
    */
    {
        currentTarget.transform.GetChild(0).localScale = new Vector3(targetTapGame.settings.targetSize, targetTapGame.settings.targetSize, targetTapGame.settings.targetSize);
    }

    public void TargetFade()
    /**
    Turns the current target translucent when the shoe collider and target are in contact.
    */
    {
        Color color1 = targetRenderer.materials[0].color;
        Color color2 = targetRenderer.materials[1].color;

        color1.a = 0.7f;
        color2.a = 0.7f;

        targetRenderer.materials[0].color = color1;
        targetRenderer.materials[1].color = color2;
    }

    public void ResetTargetFade()
    /**
    Resets the current target translucency when the shoe collider and target are no longer in contact.
    */
    {
        Color color1 = targetRenderer.materials[0].color;
        Color color2 = targetRenderer.materials[1].color;

        color1.a = 1f;
        color2.a = 1f;

        targetRenderer.materials[0].color = color1;
        targetRenderer.materials[1].color = color2;
    }

    public void StartAudioCue()
    /**
    Plays an audio cue when the shoe collider and target are in contact. 
    Starts with an initial kick noise, then a continious whistle noise.
    */
    {
        slidingSound.Play();
        kickSound.Play();

    }

    public void StopAudioCue()
    /**
    Stops the audio cue either when the shoe collider and target are no longer in contact, or if the current target is destroyed.
    */
    {
        slidingSound.Stop();
    }

    public float GetHitTime()
    /**
    Returns the hit time of the current target.
    */
    {
        return currentTargetEndTime - currentTargetStartTime;
    }

    public void SetTargetCountdown()
    /**
    Sets the targetHoldCountdown using the targetHoldTime value set in settings.
    */
    {
        targetHoldCountdown = targetTapGame.settings.targetHoldTime;
    }

    public void ChangeTargetSize()
    /**
    Changes the size of the target using the targetSize value set in settings.
    */
    {
        target.transform.localScale = new Vector3(targetTapGame.settings.targetSize, targetTapGame.settings.targetSize, targetTapGame.settings.targetSize);
    }
}