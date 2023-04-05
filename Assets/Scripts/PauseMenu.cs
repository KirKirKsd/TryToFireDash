using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    [SerializeField] private GameObject pauseMenuUI;
    private bool isPaused;

    public Slider xSens;
    public Slider ySens;
    
    private void Start() {
        xSens.value = PlayerMotor.xSens;
        ySens.value = PlayerMotor.ySens;
    }

    

    public void OnEsc() {
        switch (isPaused) {
            case true:
                Resume();
                break;
            case false:
                Pause();
                break;
        }
    }

    private void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void xSensChange() {
        PlayerMotor.xSens = xSens.value;
    }

    public void ySensChange() {
        PlayerMotor.ySens = ySens.value;
    }

}
