using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Bonfire : MonoBehaviour {
    
    public float health = 200f;
    private float maxHealth;

    public TextMeshProUGUI healthTextNumText;
    public Slider healthBarSlider;

    private void Start() {
        maxHealth = health;
    }

    private void Update() {
        healthBarSlider.value = health / maxHealth;
        healthTextNumText.text = health + "/" + maxHealth;
    }
    
    public void TakeDamage(float damage) {
        health -= damage;
    }

}
