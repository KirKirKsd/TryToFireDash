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
    public Camera minimapCamera;
    public GameObject lightGameObject;

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

    // public void thirdCard() {
    //     CantChoose();
    //     bonfireScript.UpgradeHealth();
    // }
    
    public void secondCard() {
        setLightPositionUI.SetActive(true);
        UpgradesUI.SetActive(false);
    }

    public void MouseClicked() {
        var position = minimapCamera.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(lightGameObject, new Vector3(position.x - 90f, 0f, position.z - 10f), Quaternion.identity);
        CantChoose();
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
        wavesScript.StartWave();
        Cursor.visible = false;
    }
    
}
