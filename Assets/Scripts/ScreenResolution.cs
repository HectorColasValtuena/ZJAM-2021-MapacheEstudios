using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    public void Awake ()
    {
    	Debug.LogError("ScreenResolution");
    	Screen.SetResolution(320, 240, true);
    }
}
