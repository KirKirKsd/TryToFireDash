using System.Collections;
using UnityEngine;
using TMPro;

public class GunRifle : MonoBehaviour {

    private bool canShoot = true;
    private bool isReloading;
    
    public float needCooldown = 0.2f;
    private float cooldown = 0.2f;

    public int currentAmmo;
    public int maxAmmo;
    public int ammo;

    public ParticleSystem shootParticles;
    public GameObject gunFire;
    
    public TextMeshProUGUI ammoText;
    public GameObject reloadingText;

    public float damage = 10f;
    public LayerMask enemyLayer;
    public Transform shootPoint;
    
    public void AmmoAwake() {
        ammo = (currentAmmo - 1) % maxAmmo;
        currentAmmo -= maxAmmo;
        ammo += 1;
    }

    private void OnEnable() {
        reloadingText.SetActive(false);
        canShoot = true;
        isReloading = false;
        if (ammo == 0 && currentAmmo > 0) {
            StartCoroutine(Reload());
        }
    }

    private void Update() {
        if (cooldown < needCooldown) {
            cooldown += Time.deltaTime;
        }

        ammoText.text = ammo + "/" + currentAmmo;
    }

    public void Shoot() {
        if (cooldown >= needCooldown && ammo > 0 && canShoot) {
            RaycastHit hit;
            if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, 100f, enemyLayer)) {
                Damage(hit.transform.gameObject);
            }
            shootParticles.Play();
            StartCoroutine(ChangeVisibilityFire());
            ammo -= 1;
            if (ammo == 0 && currentAmmo > 0) {
                StartCoroutine(Reload());
            }

            AfterShoot();
        }
    }

    private void AfterShoot() {
        cooldown = 0f;
    }

    IEnumerator ChangeVisibilityFire() {
        gunFire.SetActive(true);
        yield return new WaitForSeconds(0.025f);
        gunFire.SetActive(false);
    }

    public void StartReload() {
        StartCoroutine(Reload());
    }
    
    private IEnumerator Reload() {
        if (ammo < maxAmmo && !isReloading) {
            isReloading = true;
            reloadingText.SetActive(true);
            canShoot = false;
            yield return new WaitForSeconds(2);
            if (currentAmmo >= maxAmmo && ammo == 0) {
                ammo = maxAmmo;
                currentAmmo -= maxAmmo;
            }
            else if (currentAmmo >= maxAmmo && ammo > 0) {
                currentAmmo -= maxAmmo - ammo;
                ammo = maxAmmo;
            }
            else if (currentAmmo < maxAmmo) {
                ammo = currentAmmo;
                currentAmmo = 0;
            }
            reloadingText.SetActive(false);
            canShoot = true;
            isReloading = false;
        }
    }

    private void Damage(GameObject enemy) {
        enemy.GetComponent<Enemy>().TakeDamage(damage);
    }
    
}
