using UnityEngine;
using TMPro;

public class TabsController : MonoBehaviour
{
    /*
        TabsController handles switching between the "Statistics" 
        and "Settings" pages when their respective tab buttons are clicked. 
        It also controls the visibility of the line dividers under the active tab.
    */

    public GameObject LineDivider1;
    public GameObject LineDivider2;

    public GameObject StatisicsPage;
    public GameObject SettingsPage;

    /*
        Unity method called once before the first frame update.  
        Initializes the default active tab as "Statistics".
    */
    void Start()
    {
        // Initialize the tabs to show the first tab by default
        LineDivider1.SetActive(true);
        LineDivider2.SetActive(false);

        StatisicsPage.SetActive(true);
        SettingsPage.SetActive(false);
    }


    /*
        Called when the Statistics tab button is clicked.  
        Activates the Statistics page and its divider while hiding the Settings page.
    */
    public void StatisticsTabButtonClicked()
    {
        LineDivider1.SetActive(true);
        LineDivider2.SetActive(false);
        StatisicsPage.SetActive(true);
        SettingsPage.SetActive(false);
    }


    /*
        Called when the Settings tab button is clicked.  
        Activates the Settings page and its divider while hiding the Statistics page.
    */
    public void SettingsTabButtonClicked()
    {
        LineDivider1.SetActive(false);
        LineDivider2.SetActive(true);
        StatisicsPage.SetActive(false);
        SettingsPage.SetActive(true);
    }


}
