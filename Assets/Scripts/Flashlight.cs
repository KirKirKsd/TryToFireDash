using System;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour {

    private bool isOn;

    public Light spot;
    public Material point;

    private float cooldown;
    private float maxCooldown = 5f;
    private float speed = 2f;

    public Slider powerSliderUI;

    private void Start() {
        spot.enabled = false;
        point.SetColor("_EmissionColor", Color.red);
        cooldown = maxCooldown;
    }

    private void Update() {
        powerSliderUI.value = cooldown / maxCooldown;
        if (isOn && cooldown >= 0) {
            cooldown -= Time.deltaTime * speed;
            spot.enabled = true;
            point.SetColor("_EmissionColor", Color.green);
        }
        else {
            spot.enabled = false;
            point.SetColor("_EmissionColor", Color.red);
        }
    }

    public void SwitchPower() {
        switch (isOn) {
            case true:
                isOn = false;
                break;
            case false:
                isOn = true;
                break;
        }
    }

    public void Fill() {
        cooldown = maxCooldown;
    }

}
