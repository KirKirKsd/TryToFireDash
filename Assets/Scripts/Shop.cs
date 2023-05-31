using System;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour {

    private int upgradeLvlPlayer;
    private int upgradeLvlBonfire;

    public GameObject upgradeLvlPlayerDone;
    public GameObject upgradeLvlPlayerBuy;
    [Space]
    public GameObject upgradeLvlBonfireDone;
    public GameObject upgradeLvlBonfireBuy;
    [Space]
    public TextMeshProUGUI moneyTextUI;
    [Space] [Space]
    public GameObject riffle1Select;
    public GameObject riffle1Equip;
    [Space]
    public GameObject riffle2Buy;
    public GameObject riffle2Select;
    public GameObject riffle2Equip;

    private int gunEquipped;
    private int riffle2Bought;
    
    private void Start() {
        upgradeLvlPlayer = PlayerPrefs.GetInt("upgradeLvlPlayer");
        upgradeLvlBonfire = PlayerPrefs.GetInt("upgradeLvlBonfire");
        
        moneyTextUI.text = PlayerPrefs.GetInt("Money").ToString();
        
        if (upgradeLvlPlayer == 10) {
            upgradeLvlPlayerBuy.SetActive(false);
            upgradeLvlPlayerDone.SetActive(true);
        }
        if (upgradeLvlBonfire == 10) {
            upgradeLvlBonfireBuy.SetActive(false);
            upgradeLvlBonfireDone.SetActive(true);
        }

        gunEquipped = PlayerPrefs.GetInt("ChooseWeapon");
        riffle2Bought = PlayerPrefs.GetInt("riffle2Bought");

        if (gunEquipped == 0) {
            riffle1Select.SetActive(false);
            riffle1Equip.SetActive(true);
            riffle2Equip.SetActive(false);
            if (riffle2Bought == 0) {
                riffle2Buy.SetActive(true);
                riffle2Select.SetActive(false);
            }
            else if (riffle2Bought == 1) {
                riffle2Buy.SetActive(false);
                riffle2Select.SetActive(true);
            }
        }
        else if (gunEquipped == 1) {
            riffle2Buy.SetActive(false);
            riffle2Select.SetActive(false);
            riffle2Equip.SetActive(true);
            
            riffle1Equip.SetActive(false);
            riffle1Select.SetActive(true);
        }
    }

    public void UpgradePlayerHealth() {
        if (PlayerPrefs.GetInt("Money") < 600) return;

        upgradeLvlPlayer += 1;
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 600);
        moneyTextUI.text = PlayerPrefs.GetInt("Money").ToString();
        PlayerPrefs.SetInt("upgradeLvlPlayer", upgradeLvlPlayer);
        if (upgradeLvlPlayer == 10) {
            upgradeLvlPlayerBuy.SetActive(false);
            upgradeLvlPlayerDone.SetActive(true);
        }
    }

    public void UpgradeBonfireHealth() {
        if (PlayerPrefs.GetInt("Money") < 600) return;

        upgradeLvlBonfire += 1;
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 600);
        moneyTextUI.text = PlayerPrefs.GetInt("Money").ToString();
        PlayerPrefs.SetInt("upgradeLvlBonfire", upgradeLvlBonfire);
        if (upgradeLvlBonfire == 10) {
            upgradeLvlBonfireBuy.SetActive(false);
            upgradeLvlBonfireDone.SetActive(true);
        }
    }

    public void BuyRiffle2() {
        if (PlayerPrefs.GetInt("Money") < 1200) return;
        
        PlayerPrefs.SetInt("riffle2Bought", 1);
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 1200);
        moneyTextUI.text = PlayerPrefs.GetInt("Money").ToString();
        
        riffle2Buy.SetActive(false);
        riffle2Select.SetActive(true);
    }

    public void ChooseRiffle(int riffle) {
        switch (riffle) {
            case 1:
                PlayerPrefs.SetInt("ChooseWeapon", 0);
                riffle1Equip.SetActive(true);
                riffle1Select.SetActive(false);
                
                riffle2Equip.SetActive(false);
                riffle2Select.SetActive(true);
                
                break;
            case 2:
                PlayerPrefs.SetInt("ChooseWeapon", 1);
                riffle2Equip.SetActive(true);
                riffle2Select.SetActive(false);
                
                riffle1Equip.SetActive(false);
                riffle1Select.SetActive(true);
                
                break;
        }
    }

}
