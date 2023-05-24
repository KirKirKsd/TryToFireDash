using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float health = 100f;
    public float maxHealth;
    
    private int lives = 2;
    
    public TextMeshProUGUI timerTextUI;
    public GameObject deathScreen;
    public TextMeshProUGUI respawnsRemainingTextUI;

    public Camera mainCamera;
    public Camera bonfireCamera;

    public TextMeshProUGUI healthTextNumText;
    public Slider healthBarSlider;

    public Bonfire bonfireScript;

    private void Start() {
        health += PlayerPrefs.GetInt("upgradeLvlPlayer") * 20;
        
        maxHealth = health;
        
        healthBarSlider.value = health / maxHealth;
        healthTextNumText.text = health + "/" + maxHealth;
    }

    public void TakeDamage(float damage) {
        health -= damage;

        if (health <= 0) {
            health = 0;
            StartCoroutine(Death());
        }
        
        healthBarSlider.value = health / maxHealth;
        healthTextNumText.text = health + "/" + maxHealth;
    }

    public void Heal() {
        health = maxHealth;
        
        healthBarSlider.value = health / maxHealth;
        healthTextNumText.text = health + "/" + maxHealth;
    }
    
    public void UpgradeMaxHealth() {
        var per = health / maxHealth;
        maxHealth += Mathf.Round(maxHealth * 0.5f);
        health = Mathf.Round(maxHealth * per);
        
        healthBarSlider.value = health / maxHealth;
        healthTextNumText.text = health + "/" + maxHealth;
    }

    private IEnumerator Death() {
        if (lives > 0) {
            transform.position = new Vector3(20, 1, 20);
            
            mainCamera.gameObject.SetActive(false);
            bonfireCamera.gameObject.SetActive(true);
            
            GetComponent<PlayerMotor>().walk.Disable();
            
            deathScreen.SetActive(true);

            respawnsRemainingTextUI.text = "Respawns remaining: " + lives;
            
            timerTextUI.text = "3";
            yield return new WaitForSeconds(1);
            timerTextUI.text = "2";
            yield return new WaitForSeconds(1);
            timerTextUI.text = "1";
            yield return new WaitForSeconds(1);
            
            transform.position = new Vector3(0, 1, 2);
            
            mainCamera.gameObject.SetActive(true);
            bonfireCamera.gameObject.SetActive(false);
            
            GetComponent<PlayerMotor>().walk.Enable();
            
            deathScreen.SetActive(false);

            lives -= 1;
            health = maxHealth;
            
            healthBarSlider.value = health / maxHealth;
            healthTextNumText.text = health + "/" + maxHealth;
        }
        else {
            bonfireScript.Death();
        }
    }

}
