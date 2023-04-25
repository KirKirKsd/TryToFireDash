using System;
using UnityEngine;

public class Light : MonoBehaviour {

    public float damage = 3f;
    
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Enemy")) {
            other.gameObject.GetComponent<Enemy>().TakeDamageOfLight(damage);
        }
    }

}
