using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Waves : MonoBehaviour {

    public int currentWave;

    public List<GameObject> enemiesToBonfire;
    public List<GameObject> enemiesToPlayer;
    public List<GameObject> enemiesToBoth;
    
    public List<GameObject> enemiesToBonfireT = new();
    public List<GameObject> enemiesToPlayerT = new();
    public List<GameObject> enemiesToBothT = new();
    
    public string enemiesToBonfireCode;
    public string enemiesToPlayerCode;
    public string enemiesToBothCode;

    public List<GameObject> enemiesCanSpawn = new List<GameObject>();
    private int enemiesOnWave;
    private int enemiesLeft;
    private int enemiesDefeated;

    public float timeBetweenSpawn = 2;
    public Upgrades upgradesScript;
    
    private int randomI;

    public TextMeshProUGUI waveNumTextUI;
    public Slider waveProgressSliderUI;
    public TextMeshProUGUI waveProgressTextUI;

    public Flashlight flashlightScript;

    private void Start() {
        enemiesToBonfireT.AddRange(enemiesToBonfire);
        enemiesToPlayerT.AddRange(enemiesToPlayer);
        enemiesToBothT.AddRange(enemiesToBoth);

        StartWave();
    }

    public void StartWave() {
        currentWave += 1;

        flashlightScript.Fill();
        
        switch (currentWave % 3) {
            case 1:
                if (enemiesToPlayerT.Count > 0) {
                    randomI = Random.Range(0, enemiesToPlayerT.Count - 1);
                    enemiesCanSpawn.Add(enemiesToPlayerT[randomI]);
                    enemiesToPlayerCode += enemiesToPlayer.IndexOf(enemiesToPlayerT[randomI]);
                    enemiesToPlayerT.Remove(enemiesToPlayerT[randomI]);
                }
                break;
            case 2:
                if (enemiesToBothT.Count > 0) {
                    randomI = Random.Range(0, enemiesToBothT.Count);
                    enemiesCanSpawn.Add(enemiesToBothT[randomI]);
                    enemiesToBothCode += enemiesToBoth.IndexOf(enemiesToBothT[randomI]);
                    enemiesToBothT.Remove(enemiesToBothT[randomI]);
                }
                break;
            case 0:
                if (enemiesToBonfireT.Count > 0) {
                    randomI = Random.Range(0, enemiesToBonfireT.Count);
                    enemiesCanSpawn.Add(enemiesToBonfireT[randomI]);
                    enemiesToBonfireCode += enemiesToBonfire.IndexOf(enemiesToBonfireT[randomI]);
                    enemiesToBonfireT.Remove(enemiesToBonfireT[randomI]);
                }
                break;
        }

        if (currentWave <= 5) {
            enemiesOnWave = currentWave * 2;
        }
        else {
            enemiesOnWave = 10 + currentWave;
        }

        if (currentWave % 5 == 0) {
            upgradesScript.shufflesRemaining += 1;
            upgradesScript.shufflesRemainingTextUI.text = "Remaining: " + upgradesScript.shufflesRemaining;
        }
        
        enemiesLeft = enemiesOnWave;
        enemiesDefeated = 0;

        waveNumTextUI.text = "Wave: " + currentWave;
        waveProgressSliderUI.value = (float) enemiesDefeated / enemiesOnWave;
        waveProgressTextUI.text = enemiesDefeated + "/" + enemiesOnWave;
        
        StartCoroutine(DuringWave());
    }

    private IEnumerator DuringWave() {
        var spawnPoint = Vector3.zero;
        while (Vector3.Distance(spawnPoint, Vector3.zero) < 15f) {
            spawnPoint = new Vector3(Random.Range(-20f, 20f), 1f, Random.Range(-20f, 20f));
        }
        
        Instantiate(enemiesCanSpawn[Random.Range(0, enemiesCanSpawn.Count)], spawnPoint, Quaternion.identity);
        enemiesLeft -= 1;

        if (enemiesLeft > 0) {
            yield return new WaitForSeconds(timeBetweenSpawn);
            StartCoroutine(DuringWave());
        }
    }

    public void EnemyKilled() {
        enemiesDefeated += 1;
        
        waveNumTextUI.text = "Wave: " + currentWave;
        waveProgressSliderUI.value = (float) enemiesDefeated / enemiesOnWave;
        waveProgressTextUI.text = enemiesDefeated + "/" + enemiesOnWave;
        
        if (enemiesDefeated == enemiesOnWave) {
            upgradesScript.CanChoose();
        }
    }
    

}
