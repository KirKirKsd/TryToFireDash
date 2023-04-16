using System;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour {

    public GameObject gun1;
    public GameObject gun2;
    public GameObject gun3;
    private int currentGun = 1;

    private void Start() {
        SetCurrentGun(1);
    }

    public void SetCurrentGun(int arg) {
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
