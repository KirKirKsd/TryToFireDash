using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsScript : MonoBehaviour {

    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityText;

    private void OnEnable() {
        sensitivitySlider.value = PlayerMotor.sens;
        sensitivityText.text = sensitivitySlider.value.ToString();
    }

    public void OnValueChanged() {
        PlayerMotor.sens = sensitivitySlider.value;
        sensitivityText.text = sensitivitySlider.value.ToString();
    }

}
