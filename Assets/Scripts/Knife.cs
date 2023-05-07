using System.Collections;
using UnityEngine;
using TMPro;

public class Knife : MonoBehaviour {
    
    private float needCooldown = 0.2f;
    private float cooldown = 0.2f;

    public TextMeshProUGUI ammoText;
    public GameObject reloadingText;

    public float damage = 10f;
    public LayerMask enemyLayer;
    public Transform shootPoint;

    private void OnEnable() {
        reloadingText.SetActive(false);
        ammoText.text = " / ";
    }

    private void Update() {
        if (cooldown < needCooldown) {
            cooldown += Time.deltaTime;
        }
    }

    public void Shoot() {
        if (cooldown >= needCooldown) {
            RaycastHit hit;
            if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, 2f, enemyLayer)) {
                Damage(hit.transform.gameObject);
                print(hit);
            }

            AfterShoot();
        }
    }

    private void AfterShoot() {
        cooldown = 0f;
    }

    private void Damage(GameObject enemy) {
        enemy.GetComponent<Enemy>().TakeDamage(damage);
    }
    
}
