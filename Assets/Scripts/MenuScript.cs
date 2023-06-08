using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public GameObject creditsGameObject;
    public GameObject menuGameObject;
    public GameObject shopGameObject;
    public GameObject settingsGameObject;

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void Shop() {
        shopGameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ExitShop() {
        shopGameObject.SetActive(false);
        menuGameObject.SetActive(true);
    }

    public void Settings() {
        settingsGameObject.SetActive(true);
        gameObject.SetActive(false);
    }


    public void ExitSettings() {
        settingsGameObject.SetActive(false);
        menuGameObject.SetActive(true);
    }
    
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
