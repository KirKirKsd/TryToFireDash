using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour {

    public Slider sensivityXSlider;
    public Slider sensivityYSlider;

    private void Start() {
        sensivityXSlider.value = PlayerMotor.xSens;
        sensivityYSlider.value = PlayerMotor.ySens;
    }

    public void OnValueXChange() {
        PlayerMotor.xSens = sensivityXSlider.value;
    }

    public void OnValueYChange() {
        PlayerMotor.xSens = sensivityYSlider.value;
    }

}
