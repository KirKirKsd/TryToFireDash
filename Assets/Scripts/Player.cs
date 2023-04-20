using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float health = 100f;

    public void TakeDamage(float damage) {
        health -= damage;
    }
    
}
