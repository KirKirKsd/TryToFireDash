using System;
using UnityEngine;

public class Light : MonoBehaviour {

    public float damage = 3f;
    public bool canChangeSize;
    public bool isBonfire;
    public UnityEngine.Light pointLight;

    private int waveEntry;
    [HideInInspector] public int waveAlive;
    private Waves wavesScript;

    private void Start() {
        wavesScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Waves>();
        waveEntry = wavesScript.currentWave;
    }

    private void Update() {
        if (wavesScript.currentWave == waveEntry + waveAlive + 1 && !isBonfire) {
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Enemy")) {
            other.gameObject.GetComponent<Enemy>().TakeDamageOfLight(damage);
        }
    }

    public void SetSize(float size) {
        if (canChangeSize) {
            pointLight.range = size;
            waveAlive = size switch {
                5 => 20,
                10 => 10,
                15 => 5,
                _ => waveAlive
            };
        }
    }
    
    public void AddDamage() {
        damage += Mathf.Round(damage * 0.5f);
    }

}
