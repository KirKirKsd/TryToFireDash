using System;
using UnityEngine;

public class Flashlight : MonoBehaviour {

    private bool isOn;

    public Light spot;
    public Material point;

    private void Start() {
        Off();
    }

    public void SwitchPower() {
        switch (isOn) {
            case true:
                Off();
                break;
            case false:
                On();
                break;
        }
    }

    private void Off() {
        isOn = false;
        spot.enabled = false;
        point.SetColor("_EmissionColor", Color.red);
    }

    private void On() {
        isOn = true;
        spot.enabled = true;
        point.SetColor("_EmissionColor", Color.green);
    }
    
}
