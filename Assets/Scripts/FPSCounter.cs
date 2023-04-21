using System;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour {

    public TextMeshProUGUI fpsText;

    /*private void Start() {
        Application.targetFrameRate = -1;
        QualitySettings.vSyncCount = -1;
    }*/

    private void Update() {
        fpsText.text = (Mathf.Round(1f / Time.deltaTime)).ToString();
    }

}
