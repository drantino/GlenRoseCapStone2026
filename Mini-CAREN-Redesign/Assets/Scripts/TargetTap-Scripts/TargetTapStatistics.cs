using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TargetTapStatistcs : StatisticsBase
/**
This class implements the StatisticsBase interface. It stores, updates, and displays the statistics for the TargetTap game.
*/
{
    public TargetTapGameLogic targetTapGame;

    public int targetsHit = 0;
    public int targetsRemaining;
    public int targetsSkipped;

    private List<List<float>> hitTimeList;
    private float[] medianList;

    public Transform targetStatisticsCard;
    public Transform scoreboard;
    public Transform endGameUI;

    public Transform frontRight;
    public Transform front;
    public Transform frontLeft;
    public Transform left;
    public Transform backLeft;
    public Transform back;
    public Transform backRight;
    public Transform right;


    public void InitializeHitTimeLists()
    /**
    Initializes the lists of hit times for each section.
    Also initializes the array of median hit times for each section.
    */
    {
        hitTimeList = new List<List<float>>();
        medianList = new float[8];
        for (int i = 0; i < 8; ++i)
        {
            hitTimeList.Add(new List<float>());
        }
    }

    private void AddHitTime()
    /**
    Adds the new hit time value to the hit time list of its corresponding section.
    Sorts the list so that it is easier to find the median hit time value.
    */
    {
        int section = targetTapGame.targetLogic.currentTargetSection;
        float newHitTime = targetTapGame.targetLogic.GetHitTime();

        hitTimeList[section].Add(newHitTime);
        hitTimeList[section].Sort();
    }

    private void UpdateMedianHitTime()
    /**
    Finds the median hit time for every section and updates the median hit time array.
    */
    {
        int sectionLength = hitTimeList[targetTapGame.targetLogic.currentTargetSection].Count;
        int sectionIndex;

        if (sectionLength == 0)
        {
            medianList[targetTapGame.targetLogic.currentTargetSection] = 0.0f;
        }
        else if (sectionLength == 1)
        {
            medianList[targetTapGame.targetLogic.currentTargetSection] = hitTimeList[targetTapGame.targetLogic.currentTargetSection][0];
        }
        else if (sectionLength % 2 != 0)
        {
            sectionIndex = (int)(sectionLength / 2);
            medianList[targetTapGame.targetLogic.currentTargetSection] = hitTimeList[targetTapGame.targetLogic.currentTargetSection][sectionIndex];
        }
        else
        {
            sectionIndex = (int)(sectionLength / 2);
            medianList[targetTapGame.targetLogic.currentTargetSection] = (hitTimeList[targetTapGame.targetLogic.currentTargetSection][sectionIndex - 1] + hitTimeList[targetTapGame.targetLogic.currentTargetSection][sectionIndex]) / 2;
        }
    }

    private void DisplayMedianHitTime()
    /**
    Updates the median hit time displayed on the StatisticsUI page - for therapists.
    */
    {
        if (medianList[0] > 0f)
        {
            frontRight.Find("Median").GetComponent<TextMeshProUGUI>().text = medianList[0].ToString("F1");
        }
        if (medianList[1] > 0f)
        {
            front.Find("Median").GetComponent<TextMeshProUGUI>().text = medianList[1].ToString("F1");
        }
        if (medianList[2] > 0f)
        {
            frontLeft.Find("Median").GetComponent<TextMeshProUGUI>().text = medianList[2].ToString("F1");
        }
        if (medianList[3] > 0f)
        {
            left.Find("Median").GetComponent<TextMeshProUGUI>().text = medianList[3].ToString("F1");
        }
        if (medianList[4] > 0f)
        {
            backLeft.Find("Median").GetComponent<TextMeshProUGUI>().text = medianList[4].ToString("F1");
        }
        if (medianList[5] > 0f)
        {
            back.Find("Median").GetComponent<TextMeshProUGUI>().text = medianList[5].ToString("F1");
        }
        if (medianList[6] > 0f)
        {
            backRight.Find("Median").GetComponent<TextMeshProUGUI>().text = medianList[6].ToString("F1");
        }
        if (medianList[7] > 0f)
        {
            right.Find("Median").GetComponent<TextMeshProUGUI>().text = medianList[7].ToString("F1");
        }
    }

    public void DisplayMedianEndScreen()
    /**
    Displays the median hit time on the projector screen. This is the screen that pops up when the game ends.
    */
    {
        if (medianList[0] > 0f)
        {
            endGameUI.Find("Text - Front Right").GetComponent<TextMeshProUGUI>().text = medianList[0].ToString("F1") + 's';
        }
        if (medianList[1] > 0f)
        {
            endGameUI.Find("Text - Front").GetComponent<TextMeshProUGUI>().text = medianList[1].ToString("F1") + 's';
        }
        if (medianList[2] > 0f)
        {
            endGameUI.Find("Text - Front Left").GetComponent<TextMeshProUGUI>().text = medianList[2].ToString("F1") + 's';
        }
        if (medianList[3] > 0f)
        {   
            endGameUI.Find("Text - Left").GetComponent<TextMeshProUGUI>().text = medianList[3].ToString("F1") + 's';
        }
        if (medianList[4] > 0f)
        {
            endGameUI.Find("Text - Back Left").GetComponent<TextMeshProUGUI>().text = medianList[4].ToString("F1") + 's';
        }
        if (medianList[5] > 0f)
        {
            endGameUI.Find("Text - Back").GetComponent<TextMeshProUGUI>().text = medianList[5].ToString("F1") + 's';
        }
        if (medianList[6] > 0f)
        {
            endGameUI.Find("Text - Back Right").GetComponent<TextMeshProUGUI>().text = medianList[6].ToString("F1") + 's';
        }
        if (medianList[7] > 0f)
        {
            endGameUI.Find("Text - Right").GetComponent<TextMeshProUGUI>().text = medianList[7].ToString("F1") + 's';
        }
    }    

    private void DisplayMinHitTime()
    /**
    Finds and displays the minimum hit time on the StatisticsUI page - for therapists.
    Will only be displayed if the Min/Max toggle is on.
    */
    {
        if (hitTimeList[0].Count() != 0)
        {
            frontRight.Find("Min").GetComponent<TextMeshProUGUI>().text = hitTimeList[0].Min().ToString("F1");
        }
        if (hitTimeList[1].Count() != 0)
        {
            front.Find("Min").GetComponent<TextMeshProUGUI>().text = hitTimeList[1].Min().ToString("F1");
        }
        if (hitTimeList[2].Count() != 0)
        {
            frontLeft.Find("Min").GetComponent<TextMeshProUGUI>().text = hitTimeList[2].Min().ToString("F1");
        }
        if (hitTimeList[3].Count() != 0)
        {
            left.Find("Min").GetComponent<TextMeshProUGUI>().text = hitTimeList[3].Min().ToString("F1");
        }
        if (hitTimeList[4].Count() != 0)
        {
            backLeft.Find("Min").GetComponent<TextMeshProUGUI>().text = hitTimeList[4].Min().ToString("F1");
        }
        if (hitTimeList[5].Count() != 0)
        {
            back.Find("Min").GetComponent<TextMeshProUGUI>().text = hitTimeList[5].Min().ToString("F1");
        }
        if (hitTimeList[6].Count() != 0)
        {
            backRight.Find("Min").GetComponent<TextMeshProUGUI>().text = hitTimeList[6].Min().ToString("F1");
        }
        if (hitTimeList[7].Count() != 0)
        {
            right.Find("Min").GetComponent<TextMeshProUGUI>().text = hitTimeList[7].Min().ToString("F1");
        }
    }

    private void DisplayMaxHitTime()
    /**
    Finds and displays the maximum hit time on the StatisticsUI page - for therapists.
    Will only be displayed if the Min/Max toggle is on.
    */
    {
        if (hitTimeList[0].Count() != 0)
        {
            frontRight.Find("Max").GetComponent<TextMeshProUGUI>().text = hitTimeList[0].Max().ToString("F1");
        }
        if (hitTimeList[1].Count() != 0)
        {
            front.Find("Max").GetComponent<TextMeshProUGUI>().text = hitTimeList[1].Max().ToString("F1");
        }
        if (hitTimeList[2].Count() != 0)
        {
            frontLeft.Find("Max").GetComponent<TextMeshProUGUI>().text = hitTimeList[2].Max().ToString("F1");
        }
        if (hitTimeList[3].Count() != 0)
        {
            left.Find("Max").GetComponent<TextMeshProUGUI>().text = hitTimeList[3].Max().ToString("F1");
        }
        if (hitTimeList[4].Count() != 0)
        {
            backLeft.Find("Max").GetComponent<TextMeshProUGUI>().text = hitTimeList[4].Max().ToString("F1");
        }
        if (hitTimeList[5].Count() != 0)
        {
            back.Find("Max").GetComponent<TextMeshProUGUI>().text = hitTimeList[5].Max().ToString("F1");
        }
        if (hitTimeList[6].Count() != 0)
        {
            backRight.Find("Max").GetComponent<TextMeshProUGUI>().text = hitTimeList[6].Max().ToString("F1");
        }
        if (hitTimeList[7].Count() != 0)
        {
            right.Find("Max").GetComponent<TextMeshProUGUI>().text = hitTimeList[7].Max().ToString("F1");
        }
    }

    private void UpdateTargetsHit()
    /**
    Updates the number of targets that have been hit.
    */
    {
        targetsHit += 1;
    }

    private void DisplayTargetsHit()
    /**
    Displays the number of targets hit, remaining, and skipped on the StatisticsUI page and on the Scoreboard.
    */
    {
        targetStatisticsCard.Find("Targets Hit").GetComponent<TextMeshProUGUI>().text = targetsHit.ToString();
        targetStatisticsCard.Find("Targets Remaining").GetComponent<TextMeshProUGUI>().text = (targetTapGame.settings.numTargets - targetsHit).ToString();
        targetStatisticsCard.Find("Targets Skipped").GetComponent<TextMeshProUGUI>().text = targetsSkipped.ToString();

        scoreboard.Find("Targets Hit").GetComponent<TextMeshProUGUI>().text = targetsHit.ToString();
        scoreboard.Find("Targets Left").GetComponent<TextMeshProUGUI>().text = (targetTapGame.settings.numTargets - targetsHit).ToString();
    }

    public override void UpdateStatistics()
    /**
    Function that calls all the other methods that update statistics.
    */
    {
        UpdateTargetsHit();
        AddHitTime();
        UpdateMedianHitTime();
    }

    public override void DisplayStatistics()
    /**
    Function that calls all the other methods which displays statistics.
    */
    {
        DisplayTargetsHit();
        DisplayMedianHitTime();
        DisplayMinHitTime();
        DisplayMaxHitTime();
    }

    public void MinMaxShow()
    /**
    The logic that shows all of the Min/Max hit times when the Min/Max toggle is turned on.
    */
    {
        frontRight.Find("Min").GetComponent<TextMeshProUGUI>().enabled = true;
        front.Find("Min").GetComponent<TextMeshProUGUI>().enabled = true;
        frontLeft.Find("Min").GetComponent<TextMeshProUGUI>().enabled = true;
        left.Find("Min").GetComponent<TextMeshProUGUI>().enabled = true;
        backLeft.Find("Min").GetComponent<TextMeshProUGUI>().enabled = true;
        back.Find("Min").GetComponent<TextMeshProUGUI>().enabled = true;
        backRight.Find("Min").GetComponent<TextMeshProUGUI>().enabled = true;
        right.Find("Min").GetComponent<TextMeshProUGUI>().enabled = true;

        frontRight.Find("Max").GetComponent<TextMeshProUGUI>().enabled = true;
        front.Find("Max").GetComponent<TextMeshProUGUI>().enabled = true;
        frontLeft.Find("Max").GetComponent<TextMeshProUGUI>().enabled = true;
        left.Find("Max").GetComponent<TextMeshProUGUI>().enabled = true;
        backLeft.Find("Max").GetComponent<TextMeshProUGUI>().enabled = true;
        back.Find("Max").GetComponent<TextMeshProUGUI>().enabled = true;
        backRight.Find("Max").GetComponent<TextMeshProUGUI>().enabled = true;
        right.Find("Max").GetComponent<TextMeshProUGUI>().enabled = true;
    }

    public void MinMaxHide()
    /**
    The logic that hides all of the Min/Max hit times when the Min/Max toggle is turned off.
    */
    {
        frontRight.Find("Min").GetComponent<TextMeshProUGUI>().enabled = false;
        front.Find("Min").GetComponent<TextMeshProUGUI>().enabled = false;
        frontLeft.Find("Min").GetComponent<TextMeshProUGUI>().enabled = false;
        left.Find("Min").GetComponent<TextMeshProUGUI>().enabled = false;
        backLeft.Find("Min").GetComponent<TextMeshProUGUI>().enabled = false;
        back.Find("Min").GetComponent<TextMeshProUGUI>().enabled = false;
        backRight.Find("Min").GetComponent<TextMeshProUGUI>().enabled = false;
        right.Find("Min").GetComponent<TextMeshProUGUI>().enabled = false;

        frontRight.Find("Max").GetComponent<TextMeshProUGUI>().enabled = false;
        front.Find("Max").GetComponent<TextMeshProUGUI>().enabled = false;
        frontLeft.Find("Max").GetComponent<TextMeshProUGUI>().enabled = false;
        left.Find("Max").GetComponent<TextMeshProUGUI>().enabled = false;
        backLeft.Find("Max").GetComponent<TextMeshProUGUI>().enabled = false;
        back.Find("Max").GetComponent<TextMeshProUGUI>().enabled = false;
        backRight.Find("Max").GetComponent<TextMeshProUGUI>().enabled = false;
        right.Find("Max").GetComponent<TextMeshProUGUI>().enabled = false;
    }
}