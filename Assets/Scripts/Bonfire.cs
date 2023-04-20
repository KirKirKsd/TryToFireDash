using UnityEngine;

public class Bonfire : MonoBehaviour {
    
    public float health = 200f;

    public void TakeDamage(float damage) {
        health -= damage;
    }

}
