using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bonfire : MonoBehaviour {
    
    public float health = 200f;
    public float maxHealth;

    public TextMeshProUGUI healthTextNumText;
    public Slider healthBarSlider;

    public Score scoreScript;

    private void Start() {
        health += PlayerPrefs.GetInt("upgradeLvlBonfire") * 40;
        
        maxHealth = health;
        
        healthBarSlider.value = health / maxHealth;
        healthTextNumText.text = health + "/" + maxHealth;
    }

    public void TakeDamage(float damage) {
        health -= damage;

        if (health <= 0) {
            health = 0;
            Death();
        }
        
        healthBarSlider.value = health / maxHealth;
        healthTextNumText.text = health + "/" + maxHealth;
    }

    public void Heal() {
        health += Mathf.Round(health * 0.4f);

        if (health > maxHealth) {
            health = maxHealth;
        }
        
        healthBarSlider.value = health / maxHealth;
        healthTextNumText.text = health + "/" + maxHealth;
    }

    public void Upgrade() {
        var per = health / maxHealth;
        maxHealth += Mathf.Round(maxHealth * 0.5f);
        health = Mathf.Round(maxHealth * per);
        
        healthBarSlider.value = health / maxHealth;
        healthTextNumText.text = health + "/" + maxHealth;
        
        GetComponentInChildren<Light>().AddDamage();
    }

    public void Death() {
        PlayerPrefs.SetInt("LastScore", scoreScript.score);
        SceneManager.LoadScene(2);
    }
    
}
