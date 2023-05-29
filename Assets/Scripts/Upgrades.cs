using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour {

    public GameObject UpgradesUI;

    public Waves wavesScript;
    public Player playerScript;
    public Bonfire bonfireScript;
    public GunSystem gunSystemScript;

    public bool canUpgrade;
    public GameObject setLightPositionUI;
    
    private float mapWidthUI = 360f;
    private float mapHeightUI = 360f;
    private float mapBorderXUI;
    private float mapBorderYUI;

    private bool isSecondUpgrade;
    public GameObject torchPrefab;
    public GameObject lampPrefab;
    private GameObject lightGameObject;
    private bool mouseOn;

    public TextMeshProUGUI setLightPositionNameTextUI;
    public List<Button> setLightPositionButtonsUI;

    public List<GameObject> firstCardObj;
    public List<GameObject> secondCardObj;
    public List<GameObject> thirdCardObj;

    private GameObject firstCardWitchSelected;
    private GameObject secondCardWitchSelected;
    private GameObject thirdCardWitchSelected;

    public int shufflesRemaining = 3;
    public TextMeshProUGUI shufflesRemainingTextUI;
    
    private void Start() {
        setLightPositionUI.SetActive(true);
        var canvas = setLightPositionUI.GetComponent<Canvas>();
        mapWidthUI *= canvas.scaleFactor;
        mapHeightUI *= canvas.scaleFactor;
        mapBorderXUI = (Screen.width - mapWidthUI) / 2;
        mapBorderYUI = (Screen.height - mapHeightUI) / 2;
        setLightPositionUI.SetActive(false);

        firstCardWitchSelected = firstCardObj[0];
        secondCardWitchSelected = secondCardObj[0];
        thirdCardWitchSelected = thirdCardObj[0];
    }

    private void Update() {
        if (isSecondUpgrade && mouseOn) {
            TransformLight();
        }
    }

    public void CanChoose() {
        UpgradesUI.SetActive(true);
        Randomize();
        canUpgrade = true;
        GetComponent<PlayerMotor>().walk.Disable();
        Cursor.visible = true;
        Time.timeScale = 0f;
    }   

    public void Randomize(int a = 0) {
        switch (a) {
            case 1 when shufflesRemaining == 0:
                return;
            case 1:
                shufflesRemaining -= 1;
                shufflesRemainingTextUI.text = "Remaining: " + shufflesRemaining;
                break;
        }

        firstCardWitchSelected.SetActive(false);
        secondCardWitchSelected.SetActive(false);
        thirdCardWitchSelected.SetActive(false);
        
        firstCardWitchSelected = firstCardObj[Random.Range(0, firstCardObj.Count)];
        secondCardWitchSelected = secondCardObj[Random.Range(0, secondCardObj.Count)];
        thirdCardWitchSelected = thirdCardObj[Random.Range(0, thirdCardObj.Count)];
        
        firstCardWitchSelected.SetActive(true);
        secondCardWitchSelected.SetActive(true);
        thirdCardWitchSelected.SetActive(true);
    }

    public void HealPlayer() {
        CantChoose();
        playerScript.Heal();
    }

    public void UpgradeMaxPlayerHealth() {
        CantChoose();
        playerScript.UpgradeMaxHealth();
    }

    public void HealBonfire() {
        CantChoose();
        bonfireScript.Heal();
    }

    public void UpgradeBonfire() {
        CantChoose();
        bonfireScript.Upgrade();
    }

    public void UpgradeLights() {
        CantChoose();
        foreach (var obj in GameObject.FindGameObjectsWithTag("Light")) {
            obj.GetComponentInChildren<Light>().AddDamage();
        }
    }
    
    public void SpawnTorch() {
        setLightPositionUI.SetActive(true);
        Instantiate(torchPrefab, Vector3.zero, Quaternion.identity);
        lightGameObject = GameObject.FindGameObjectWithTag("MoveByMouse");
        lightGameObject.GetComponentInChildren<Light>().waveAlive = 15;
        setLightPositionNameTextUI.text = "Torch";
        foreach (var btn in setLightPositionButtonsUI) {
            btn.GetComponent<Button>().interactable = false;
        }
        isSecondUpgrade = true;
    }

    public void SpawnLamp() {
        setLightPositionUI.SetActive(true);
        Instantiate(lampPrefab, Vector3.zero, Quaternion.identity);
        lightGameObject = GameObject.FindGameObjectWithTag("MoveByMouse");
        SetSize(10);
        setLightPositionNameTextUI.text = "Lamp";
        foreach (var btn in setLightPositionButtonsUI) {
            btn.GetComponent<Button>().interactable = true;
        }
        isSecondUpgrade = true;
    }

    public void SetSize(float size) {
        lightGameObject.GetComponentInChildren<Light>().SetSize(size);
    }
    
    public void UpgradeGuns() {
        CantChoose();
        gunSystemScript.UpgradeGuns();
    }

    public void AddAmmo() {
        CantChoose();
        gunSystemScript.AddAmmo();
    }
    
    private void CantChoose() {
        Time.timeScale = 1f;
        UpgradesUI.SetActive(false);
        setLightPositionUI.SetActive(false);
        canUpgrade = false;
        isSecondUpgrade = false;
        wavesScript.StartWave();
        GetComponent<PlayerMotor>().walk.Enable();
        Cursor.visible = false;
    }

    private void TransformLight() {
        var mousePos = Input.mousePosition;
        mousePos.z = 0;
        var newPos = new Vector2(mousePos.x - mapBorderXUI, mousePos.y - mapBorderYUI);
        newPos.x /= mapWidthUI;
        newPos.y /= mapHeightUI;
        newPos *= 200;
        newPos.x -= 100;
        newPos.y -= 100;
        lightGameObject.transform.position = new Vector3(newPos.x, 0f, newPos.y);
    }

    public void EventTrigger(bool arg) {
        mouseOn = arg switch {
            true => true,
            false => false
        };
    }

    public void PutLight() {
        lightGameObject.tag = "Light";
        CantChoose();
    }
    
}
