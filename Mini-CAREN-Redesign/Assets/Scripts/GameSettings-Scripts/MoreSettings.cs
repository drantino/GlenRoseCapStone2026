using UnityEngine;

public class MoreSettings : MonoBehaviour
{

    public GameObject moreSettingsPanel;

    public bool isOpen = false;

    /*
        Toggles the visibility of the More Settings panel.
        If the panel is currently open, it will be closed, and vice versa.
    */
    public void ToggleMoreSettings()
    {
        isOpen = !isOpen;
        moreSettingsPanel.SetActive(isOpen);
    }

   
}
