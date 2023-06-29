using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MultiScreenManager : MonoBehaviour
{
    public TMP_InputField input_x;
    public TMP_InputField input_y;
    public TextMeshProUGUI tx;
    public TextMeshProUGUI debugtx;
    int displayIndex = 0;
    int screenWidth = 600;
    int screenHeight = 1280;
    int refreshRate = 60;

    public Toggle toggle_fullScreen;
    public bool fullScreen = true;

    void Awake()
    {
         toggle_fullScreen.isOn = fullScreen;
        Screen.SetResolution(1920 , 1080, fullScreen);
    }
    void Start()
    {
        tx.text = "displays connected: " + Display.displays.Length;
        input_x.text = screenWidth.ToString();
        input_y.text = screenHeight.ToString();
        // Display.displays[0] is the primary, default display and is always ON.

        // Check if additional displays are available and activate each.

        if (Display.displays.Length > 1)

            Display.displays[1].Activate();

        if (Display.displays.Length > 2)

            Display.displays[2].Activate();
    }
    public void ChangeResolution()
    {
        screenWidth = int.Parse(input_x.text);
        screenHeight = int.Parse(input_y.text);
        fullScreen = toggle_fullScreen.isOn;

        if (screenWidth < 200 || screenHeight < 200)
        {
            tx.text = "Too Small !!";
            return;
        }

        if (Display.displays.Length > displayIndex)
        {
            Display.displays[displayIndex].Activate(screenWidth, screenHeight, refreshRate);
            Screen.SetResolution(screenWidth, screenHeight, fullScreen);
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
