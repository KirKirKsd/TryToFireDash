using System;
using UnityEngine;

public class Upgrades : MonoBehaviour {

    public GameObject UpgradesUI;

    public Waves wavesScript;
    public Player playerScript;
    public Bonfire bonfireScript;

    public void CanChoose() {
        UpgradesUI.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void firstCard() {
        CantChoose();
        playerScript.HealthUpgrade();
    }

    public void secondCard() {
        
    }

    public void thirdCard() {
        CantChoose();
        bonfireScript.UpgradeHealth();
    }
    
    private void CantChoose() {
        Time.timeScale = 1f;
        UpgradesUI.SetActive(false);
        wavesScript.StartWave();
        Cursor.visible = false;
    }
    
}
