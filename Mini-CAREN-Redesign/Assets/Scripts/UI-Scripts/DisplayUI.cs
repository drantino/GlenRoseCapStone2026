using UnityEngine;

/*
    This is a simple script that allows for a UI to be displayed and hidden when a button is pressed.
*/
public class DisplayUI : MonoBehaviour
{
    public GameObject UI;

    private bool UIDisplayed = false;
    
    public void DisplayUIPopUP(){
        if(UIDisplayed==false){
            UI.SetActive(true);
            UIDisplayed = true;
        }
        else {
            UI.SetActive(false);
            UIDisplayed = false;
        }
    }

}
