using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAndSave : MonoBehaviour {

    private List<float> values = new();

    public GameObject riffle1;
    public GameObject riffle2;
    public GameObject pistol1;
    public GameObject pistol2;

    public GameObject bonfire;
    
    public void LoadSave(int scene) {
        PlayerPrefs.SetInt("ActiveScene", scene);
        SceneManager.LoadScene(1);
    }

    private void EncryptSave(int scene) {
        var loadStr = PlayerPrefs.GetString("save" + scene);

        if (loadStr == "") {
            return;
        }

        var tempValues = loadStr.Split("s").ToList();

        foreach (var arg in tempValues) {
            values.Add(Convert.ToSingle(arg));
        }
    }
    
    /*
         * Player:
         *      pos: x, y, z (3)
         *      health: now, max (2)
         *      weapons:
         *          1-st: type, damage, ammo, currentAmmo (4)
         *          2-st: type, damage, ammo, currentAmmo (4)
         * Bonfire:
         *      health: now, max (2)
         *      damage: (1)
         * Score:
         *      WaveNum: (1)
         *      score: (1)
         * Light:
         *      lightDamage: (1)
         *      lightsCount: (1)
         *      light(s): (lightsCount)
         *          pos: x, y, z (3)
         *          type: (if type == torch: type = 0, else type = strength) (1)
         *      
         */

    private void LoadToScene() {
        transform.position = new Vector3(values[0], values[1], values[2]);
        GetComponent<Player>().health = values[3];
        GetComponent<Player>().maxHealth = values[4];

        GameObject activeRiffle;
        
        if (values[5] == 0) { riffle2.SetActive(false); activeRiffle = riffle1; } 
        else { riffle1.SetActive(false); activeRiffle = riffle2; }

        activeRiffle.GetComponent<GunRifle>().damage = values[6];
        activeRiffle.GetComponent<GunRifle>().ammo = (int) values[7];
        activeRiffle.GetComponent<GunRifle>().currentAmmo = (int) values[8];

        GameObject activePistol;
        
        if (values[9] == 0) { pistol2.SetActive(false); activePistol = pistol1; } 
        else { pistol1.SetActive(false); activePistol = pistol2; }

        activePistol.GetComponent<GunPistol>().damage = values[10];
        activePistol.GetComponent<GunPistol>().ammo = (int) values[11];
        activePistol.GetComponent<GunPistol>().currentAmmo = (int) values[12];

        bonfire.GetComponent<Bonfire>().health = values[13];
        bonfire.GetComponent<Bonfire>().maxHealth = values[14];
        bonfire.GetComponent<Light>().damage = values[15];

        GetComponent<Score>().score = (int) values[16];
        GetComponent<Waves>().currentWave = (int)values[17] - 1;
        
        GetComponent<Waves>().StartWave();
    }
    
}
