using UnityEngine;

public class DisplayScript : MonoBehaviour
{    
    // This start function is used to activate all monitors for the Software, this script exists in all scenes.
    // This script dose not affect anything inside of Unity, but it allows for multi monitor UI's after you build the applicaiton and run it

    void Start()
    {
        for (int i = 0; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }
}
