using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

    public GameObject creditsGameObject;
    public GameObject menuGameObject;
    
    public void Exit() {
        Application.Quit();
    }

    public void Discord() {
        Application.OpenURL("https://discord.gg/HNJPsqav7f");
    }

    public void Credits() { 
        creditsGameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ExitCredits() {
        menuGameObject.SetActive(true);
        creditsGameObject.SetActive(false);
    }
    
}
