using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidNavigation : MonoBehaviour
{
    void Update()
    {
        // Check if the back button is pressed on Android
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Exit the application
            Application.Quit();
        }
    }
}
