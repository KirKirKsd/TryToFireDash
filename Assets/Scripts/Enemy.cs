using System;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health = 100f;
    public Waves wavesScript;

    private void Start() {
        wavesScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Waves>();
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            wavesScript.EnemyKilled();
            Destroy(gameObject);
        }
    }

}
