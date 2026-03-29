using UnityEngine;
using TMPro;
using UnityEngine.UI;

/*
    This script manages the behavior of three buttons that allow users to select different card display options.
    When a button is clicked, it updates the button colors to indicate the selected option and stores the selected value.
*/

public class CardButtons : MonoBehaviour
{
    public TextMeshProUGUI buttonText1;
    public TextMeshProUGUI buttonText2;
    public TextMeshProUGUI buttonText3;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;


    private string SelectedValue;

    public UpdateUISettingsPage updateUISettingsPage;

    /*
        Unity method called once before the first frame update.  
        Initializes the button colors and sets the default selected value.
    */
    public void button1Clicked()
    {
        // set the color of button 1 to white
        button1.GetComponent<Image>().color = Color.white;
        // set the color of button 2 to DFD5E5
        button2.GetComponent<Image>().color = new Color(0.8745098f,0.8352941f,0.8980392f);
        button3.GetComponent<Image>().color = new Color(0.8745098f,0.8352941f,0.8980392f);
        SelectedValue = buttonText1.text;
        updateUISettingsPage.UpdateUI();
    }

    /*
        Called when button 2 is clicked.  
        Updates button colors and the selected value accordingly.
    */
    public void button2Clicked()
    {
         // set the color of button 1 to white
        button2.GetComponent<Image>().color = Color.white;
        // set the color of button 2 to DFD5E5
        button1.GetComponent<Image>().color = new Color(0.8745098f,0.8352941f,0.8980392f);
        button3.GetComponent<Image>().color = new Color(0.8745098f,0.8352941f,0.8980392f);
        SelectedValue = buttonText2.text;

    }
    /*
        Called when button 3 is clicked.  
        Updates button colors and the selected value accordingly.
    */
    public void button3Clicked()
    {
         // set the color of button 1 to white
        button3.GetComponent<Image>().color = Color.white;
        // set the color of button 2 to DFD5E5
        button1.GetComponent<Image>().color = new Color(0.8745098f,0.8352941f,0.8980392f);
        button2.GetComponent<Image>().color = new Color(0.8745098f,0.8352941f,0.8980392f);
        SelectedValue = buttonText3.text;
    }

   
}
