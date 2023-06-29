using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenResolution : MonoBehaviour
{
    public TMP_InputField input_x;
    public TMP_InputField input_y;
    public int screenWidth = 600;
    public int screenHeight = 1280;
    public bool fullScreen = true;

    Screen screen;

    void Start()
    {
 
        Display.displays[0].SetRenderingResolution(screenWidth, screenHeight);
        Screen.SetResolution(screenWidth, screenHeight, fullScreen);
    }

    public void ChangeResolution(int width, int height, bool fullscreen)
    {
        Screen.SetResolution(width, height, fullscreen);
    }

    public void BTSetResolution()
    {
        screenWidth = int.Parse(input_x.text);
        screenHeight = int.Parse(input_y.text);
        ChangeResolution(screenWidth, screenHeight, fullScreen);
        Display.displays[0].SetRenderingResolution(screenWidth, screenHeight);
    }
}