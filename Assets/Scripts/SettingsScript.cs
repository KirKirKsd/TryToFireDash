using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsScript : MonoBehaviour {

    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityText;
    [Space]
    public AudioMixer mixer;
    [Space] 
    public Slider masterVolumeSlider;
    public TextMeshProUGUI masterVolumeTextUI;
    public Slider musicVolumeSlider;
    public TextMeshProUGUI musicVolumeTextUI;
    public Slider effectsVolumeSlider;
    public TextMeshProUGUI effectsVolumeTextUI;

    private void OnEnable() {
        sensitivitySlider.value = PlayerPrefs.GetInt("Controls.Sens");
        sensitivityText.text = PlayerPrefs.GetInt("Controls.Sens").ToString();

        masterVolumeSlider.value = PlayerPrefs.GetFloat("Volume.Master");
        masterVolumeTextUI.text = Mathf.Round(PlayerPrefs.GetFloat("Volume.Master") * 100) + "%";
        
        musicVolumeSlider.value = PlayerPrefs.GetFloat("Volume.Music");
        musicVolumeTextUI.text = Mathf.Round(PlayerPrefs.GetFloat("Volume.Music") * 100) + "%";
        
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("Volume.SFX");
        effectsVolumeTextUI.text = Mathf.Round(PlayerPrefs.GetFloat("Volume.SFX") * 100) + "%";
    }

    public void OnValueSensitivityChanged() {
        PlayerPrefs.SetInt("Controls.Sens", (int) sensitivitySlider.value);
        PlayerMotor.sens = sensitivitySlider.value;
        sensitivityText.text = PlayerPrefs.GetInt("Controls.Sens").ToString();
    }

    public void OnValueVolumeChanged() {
        PlayerPrefs.SetFloat("Volume.Master", masterVolumeSlider.value);
        mixer.SetFloat("MasterParam", Mathf.Log10(masterVolumeSlider.value) * 20);
        masterVolumeTextUI.text = Mathf.Round(PlayerPrefs.GetFloat("Volume.Master") * 100) + "%";
    }
    
    public void OnValueEffectsVolumeChanged() {
        PlayerPrefs.SetFloat("Volume.SFX", effectsVolumeSlider.value);
        mixer.SetFloat("SFXParam", Mathf.Log10(effectsVolumeSlider.value) * 20);
        effectsVolumeTextUI.text = Mathf.Round(PlayerPrefs.GetFloat("Volume.SFX") * 100) + "%";
    }
    
    public void OnValueMusicVolumeChanged() {
        PlayerPrefs.SetFloat("Volume.Music", musicVolumeSlider.value);
        mixer.SetFloat("MusicParam", Mathf.Log10(musicVolumeSlider.value) * 20);
        musicVolumeTextUI.text = Mathf.Round(PlayerPrefs.GetFloat("Volume.Music") * 100) + "%";
    }

}
