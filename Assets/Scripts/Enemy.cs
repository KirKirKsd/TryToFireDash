using System;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health = 30f;
    private Waves wavesScript;
    private Score scoreScript;

    public float needCooldown = 1f;
    private float cooldown;

    public bool canTakeLightDamage;
    public int cost;

    private void Start() {
        cooldown = needCooldown;
        wavesScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Waves>();
        scoreScript = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
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
            Destroy(gameObject);
            wavesScript.EnemyKilled();
            scoreScript.AddScore(cost * wavesScript.currentWave);
        }
    }

    public void TakeDamageOfLight(float damage) {
        if (cooldown >= needCooldown && canTakeLightDamage) {
            TakeDamage(damage);
        }
    }
    

}
