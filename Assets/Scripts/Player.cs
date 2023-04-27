using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float health = 100f;
    private float maxHealth;

    public TextMeshProUGUI healthTextNumText;
    public Slider healthBarSlider;

    private void Start() {
        maxHealth = health;
        
        healthBarSlider.value = health / maxHealth;
        healthTextNumText.text = health + "/" + maxHealth;
    }

    public void TakeDamage(float damage) {
        health -= damage;
        
        healthBarSlider.value = health / maxHealth;
        healthTextNumText.text = health + "/" + maxHealth;
    }

    public void HealthUpgrade() {
        var per = health / maxHealth;
        maxHealth += Mathf.Round(maxHealth * 0.5f);
        health = Mathf.Round(maxHealth * per);
        
        healthBarSlider.value = health / maxHealth;
        healthTextNumText.text = health + "/" + maxHealth;
    }
    
}
