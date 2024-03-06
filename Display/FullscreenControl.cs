using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FullscreenControl : MonoBehaviour
{
    // Define the key codes for fullscreen toggle
    private KeyCode fullscreenKeyMonitor1 = KeyCode.Alpha1;
    private KeyCode fullscreenKeyMonitor2 = KeyCode.Alpha2;

    public List<DisplayInfo> m_Displays = new List<DisplayInfo>();

    private void Start()
    {
        Debug.Log(Display.displays.Length);
        Screen.GetDisplayLayout(m_Displays);
    }

    void Update()
    {
        // Check if the specified key is pressed and toggle fullscreen accordingly
        if (Input.GetKeyDown(fullscreenKeyMonitor1))
        {
            //ToggleFullscreen(0);
            OnDisplayChanged(0);
        }
        else if (Input.GetKeyDown(fullscreenKeyMonitor2))
        {
            //ToggleFullscreen(1);
            OnDisplayChanged(1);
        }
    }

    private void OnDisplayChanged(int index)
    {
        Screen.GetDisplayLayout(m_Displays);
        StartCoroutine(MoveToDisplay(index));
    }

    private IEnumerator MoveToDisplay(int index)
    {
        try
        {
            Debug.Log(m_Displays[index]);

            var display = m_Displays[index];

            Debug.Log($"Moving window to {display.name}");

            Vector2Int targetCoordinates = new Vector2Int(0, 0);
            if (Screen.fullScreenMode != FullScreenMode.Windowed)
            {
                // Target the center of the display. Doing it this way shows off
                // that MoveMainWindow snaps the window to the top left corner
                // of the display when running in fullscreen mode.
                targetCoordinates.x += display.width / 2;
                targetCoordinates.y += display.height / 2;
            }

            var moveOperation = Screen.MoveMainWindowTo(display, targetCoordinates);
            yield return moveOperation;
        }
        finally
        {

        }
        
    }
   
}