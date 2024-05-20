using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshotter : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            ScreenCapture.CaptureScreenshot("earth_atmosphere");
        }
    }
}
