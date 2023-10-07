using UnityEngine;
using System.Collections;

public class ActivateAllDisplays : MonoBehaviour
{
    void Start()
    {
        Debug.Log("displays connected: " + Display.displays.Length);
        // Display.displays[0] is the primary, default display and is always ON, so start at index 1.
        // Check if additional displays are available and activate each.
        /*for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
        Display.displays[1].Activate();
        Display.displays[2].Activate();*/
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
            Screen.fullScreen = true;
        }
    }

    void Update()
    {

    }
}
