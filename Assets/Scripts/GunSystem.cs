using UnityEngine;

public class GunSystem : MonoBehaviour {

    public GameObject gun1;
    public GameObject gun2;
    public GameObject gun3;
    public int currentGun = 1;

    public GunRifle gunRifleScript;
    public GunPistol gunPistolScript;

    private void Start() {
        SetCurrentGun(1);
    }

    public void Shoot() {
        switch (currentGun) {
            case 1:
                gunRifleScript.Shoot();
                break;
            case 2:
                gunPistolScript.Shoot();
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
        gunRifleScript.currentAmmo += 120;
        gunRifleScript.damage += gunRifleScript.damage * 0.5f;
        gunPistolScript.currentAmmo += 48;
        gunPistolScript.damage += gunPistolScript.damage * 0.5f;
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
