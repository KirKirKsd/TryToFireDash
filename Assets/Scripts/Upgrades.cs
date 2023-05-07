using System;
using UnityEditor;
using UnityEngine;

public class Upgrades : MonoBehaviour {

    public GameObject UpgradesUI;

    public Waves wavesScript;
    public Player playerScript;
    public Bonfire bonfireScript;
    public GunSystem gunSystemScript;

    public bool canUpgrade;
    public GameObject setLightPositionUI;
    
    private float mapWidthUI = 360f;
    private float mapHeightUI = 360f;
    private float mapBorderXUI;
    private float mapBorderYUI;

    private bool isSecondUpgrade;
    public GameObject lightPrefab;
    private GameObject lightGameObject;
    private bool mouseOn;
    
    private void Start() {
        setLightPositionUI.SetActive(true);
        var canvas = setLightPositionUI.GetComponent<Canvas>();
        mapWidthUI *= canvas.scaleFactor;
        mapHeightUI *= canvas.scaleFactor;
        mapBorderXUI = (Screen.width - mapWidthUI) / 2;
        mapBorderYUI = (Screen.height - mapHeightUI) / 2;
        setLightPositionUI.SetActive(false);
    }

    private void Update() {
        if (isSecondUpgrade && mouseOn) {
            TransformLight();
        }
    }

    public void CanChoose() {
        UpgradesUI.SetActive(true);
        canUpgrade = true;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void firstCard() {
        CantChoose();
        playerScript.HealthUpgrade();
    }
    
    public void secondCard() {
        setLightPositionUI.SetActive(true);
        Instantiate(lightPrefab, Vector3.zero, Quaternion.identity);
        lightGameObject = GameObject.FindGameObjectWithTag("MoveByMouse");
        isSecondUpgrade = true;
    }
    
    public void thirdCard() {
        CantChoose();
        gunSystemScript.UpgradeGuns();
    }
    
    private void CantChoose() {
        Time.timeScale = 1f;
        UpgradesUI.SetActive(false);
        setLightPositionUI.SetActive(false);
        canUpgrade = false;
        isSecondUpgrade = false;
        wavesScript.StartWave();
        Cursor.visible = false;
    }

    private void TransformLight() {
        var mousePos = Input.mousePosition;
        mousePos.z = 0;
        var newPos = new Vector2(mousePos.x - mapBorderXUI, mousePos.y - mapBorderYUI);
        newPos.x /= mapWidthUI;
        newPos.y /= mapHeightUI;
        newPos *= 200;
        newPos.x -= 100;
        newPos.y -= 100;
        lightGameObject.transform.position = new Vector3(newPos.x, 0f, newPos.y);
    }

    public void EventTrigger(bool arg) {
        mouseOn = arg switch {
            true => true,
            false => false
        };
    }

    public void PutLight() {
        lightGameObject.tag = "Light";
        CantChoose();
    }
    
}
