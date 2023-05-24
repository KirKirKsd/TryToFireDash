using UnityEngine;

public class GunSystem : MonoBehaviour {

    public GameObject gun1;
    public GameObject gun2;
    public GameObject gun3;
    public int currentGun = 1;

    public GunRifle gunRifleScript;
    public GunPistol gunPistolScript;
    public Knife knifeScript;
    private Shooting shootingScript;
    public Transform shootPoint;

    private Weapon riffle1 = new();
    private Weapon riffle2 = new();

    private void Awake() {
        shootingScript = GetComponent<Shooting>();
        
        riffle1.damage = 10;
        riffle1.ammo = 90;
        riffle1.magAmmo = 30;
        riffle1.needCooldown = 0.2f;

        riffle2.damage = 15;
        riffle2.ammo = 60;
        riffle2.magAmmo = 20;
        riffle2.needCooldown = 0.5f;
        
        var gun1Script = GetComponentInChildren<GunRifle>();
        if (PlayerPrefs.GetInt("ChooseWeapon") == 0) {
            gun1Script.damage = riffle1.damage;
            gun1Script.currentAmmo = riffle1.ammo;
            gun1Script.maxAmmo = riffle1.magAmmo;
            gun1Script.needCooldown = riffle1.needCooldown;
        }
        else if (PlayerPrefs.GetInt("ChooseWeapon") == 1) {
            gun1Script.damage = riffle2.damage;
            gun1Script.currentAmmo = riffle2.ammo;
            gun1Script.maxAmmo = riffle2.magAmmo;
            gun1Script.needCooldown = riffle2.needCooldown;
        }
        gun1Script.AmmoAwake();
    }
    
    private void Start() {
        SetCurrentGun(1);
    }

    public void Shoot() {
        switch (currentGun) {
            case 1:
                shootingScript.Shoot();
                gunRifleScript.Shoot();
                break;
            case 2:
                shootingScript.Shoot();
                gunPistolScript.Shoot();
                break;
            case 3:
                shootPoint.localRotation = Quaternion.identity;
                knifeScript.Shoot();
                break;
        }
    }

    public void Reload() {
        switch (currentGun) {
            case 1:
                gunRifleScript.StartReload();
                break;
            case 2:
                gunPistolScript.StartReload();
                break;
        }
    }

    public void UpgradeGuns() {
        gunRifleScript.damage += gunRifleScript.damage * 0.5f;
        gunPistolScript.damage += gunPistolScript.damage * 0.6f;
    }

    public void AddAmmo() {
        gunPistolScript.currentAmmo += 48;
        gunRifleScript.currentAmmo += 120;
    }
    
    public void SetCurrentGun(int arg) {
        arg = arg switch {
            4 => 1,
            0 => 3,
            _ => arg
        };

        switch (arg) {
            case 1:
                currentGun = 1;
                gun1.SetActive(true);
                gun2.SetActive(false);
                gun3.SetActive(false);
                break;
            case 2:
                currentGun = 2;
                gun1.SetActive(false);
                gun2.SetActive(true);
                gun3.SetActive(false);
                break;
            case 3:
                currentGun = 3;
                gun1.SetActive(false);
                gun2.SetActive(false);
                gun3.SetActive(true);
                break;
        }
    }

}
