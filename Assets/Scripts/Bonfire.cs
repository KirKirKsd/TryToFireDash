using UnityEngine;

public class Bonfire : MonoBehaviour {
    
    public float health = 200f;

    private void Update() {
        Debug.Log(health);
    }

    public void TakeHit(float damage) {
        health -= damage;
    }
    
}
