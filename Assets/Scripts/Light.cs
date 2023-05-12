using System;
using UnityEngine;

public class Light : MonoBehaviour {

    public float damage = 3f;
    public bool canChangeSize;
    public UnityEngine.Light pointLight;

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Enemy")) {
            other.gameObject.GetComponent<Enemy>().TakeDamageOfLight(damage);
        }
    }

    public void SetSize(float size) {
        if (canChangeSize) {
            pointLight.range = size;
        }
    }
    
    public void AddDamage() {
        damage += Mathf.Round(damage * 0.5f);
    }

}
