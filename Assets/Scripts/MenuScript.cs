using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

    public void Exit() {
        Application.Quit();
    }

    public void Discord() {
        Application.OpenURL("https://discord.gg/HNJPsqav7f");
    }
    
}
