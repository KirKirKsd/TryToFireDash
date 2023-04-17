using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class GunRifle : MonoBehaviour {

    private bool canShoot = true;
    
    private float needCooldown = 0.2f;
    private float cooldown = 0.2f;

    public int currentAmmo;
    public int maxAmmo;
    private int ammo;

    public ParticleSystem shootParticles;
    public GameObject gunFire;
    
    public TextMeshProUGUI ammoText;
    public GameObject reloadingText;

    private void Awake() {
        ammo = (currentAmmo - 1) % maxAmmo;
        currentAmmo -= maxAmmo;
        ammo += 1;
    }

    private void OnEnable() {
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
        if (ammo < maxAmmo) {
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
        }
    }
    
}
