using System;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour {

    private bool isOn;

    public Light spot;
    public Material point;

    private float cooldown;
    private float maxCooldown = 5f;
    private float speed = 0.5f;

    public Slider powerSliderUI;
    public GameObject sound;
    public GameObject disableSound;
    [HideInInspector] public bool isDisable;

    private void Start() {
        spot.enabled = false;
        point.SetColor("_EmissionColor", Color.red);
        cooldown = maxCooldown;
    }

    private void Update() {
        powerSliderUI.value = cooldown / maxCooldown;
        if (isOn && cooldown > 0) {
            cooldown -= Time.deltaTime * speed;
            spot.enabled = true;
            point.SetColor("_EmissionColor", Color.green);
        }
        else {
            if (!isDisable && cooldown <= 0) {
                print("asd");
                Instantiate(disableSound);
                isDisable = true;
            }
            spot.enabled = false;
            point.SetColor("_EmissionColor", Color.red);
        }
    }

    public void SwitchPower() {
        switch (isOn) {
            case true:
                isOn = false;
                if (!isDisable) Instantiate(sound);
                break;
            case false:
                isOn = true;
                if (!isDisable) Instantiate(sound);
                break;
        }
    }

    public void Fill() {
        cooldown = maxCooldown;
        isDisable = false;
        if (isOn) {
            Instantiate(sound);
        }
    }

}
