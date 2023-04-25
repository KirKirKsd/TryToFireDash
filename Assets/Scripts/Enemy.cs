using System;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health = 30f;
    public Waves wavesScript;

    public float needCooldown = 1f;
    private float cooldown;

    public bool canTakeLightDamage;

    private void Start() {
        cooldown = needCooldown;
        wavesScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Waves>();
    }

    private void Update() {
        if (cooldown < needCooldown) {
            cooldown += Time.deltaTime;
        }
    }

    public void TakeDamage(float damage) {
        health -= damage;
        cooldown = 0f;
        if (health <= 0) { 
            wavesScript.EnemyKilled(); 
            Destroy(gameObject);
        }
    }

    public void TakeDamageOfLight(float damage) {
        if (cooldown >= needCooldown && canTakeLightDamage) {
            TakeDamage(damage);
        }
    }
    

}
