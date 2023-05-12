using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Waves : MonoBehaviour {

    public int currentWave;

    public List<GameObject> enemiesToBonfire;
    public List<GameObject> enemiesToPlayer;
    public List<GameObject> enemiesToBoth;

    private List<GameObject> enemiesCanSpawn = new List<GameObject>();
    private int enemiesOnWave;
    private int enemiesLeft;
    private int enemiesDefeated;

    public float timeBetweenSpawn = 2;
    public Upgrades upgradesScript;
    
    private int randomI;

    public TextMeshProUGUI waveNumTextUI;
    public Slider waveProgressSliderUI;
    public TextMeshProUGUI waveProgressTextUI;

    private void Start() {
        StartWave();
    }

    public void StartWave() {
        currentWave += 1;

        switch (currentWave % 3) {
            case 1:
                if (enemiesToPlayer.Count > 0) {
                    randomI = Random.Range(0, enemiesToPlayer.Count - 1);
                    enemiesCanSpawn.Add(enemiesToPlayer[0]);
                    enemiesToPlayer.Remove(enemiesToPlayer[randomI]);
                }
                break;
            case 2:
                if (enemiesToBoth.Count > 0) {
                    randomI = Random.Range(0, enemiesToBoth.Count);
                    enemiesCanSpawn.Add(enemiesToBoth[randomI]);
                    enemiesToBoth.Remove(enemiesToBoth[randomI]);
                }
                break;
            case 0:
                if (enemiesToBonfire.Count > 0) {
                    randomI = Random.Range(0, enemiesToBonfire.Count);
                    enemiesCanSpawn.Add(enemiesToBonfire[randomI]);
                    enemiesToBonfire.Remove(enemiesToBonfire[randomI]);
                }
                break;
        }

        if (currentWave <= 5) {
            enemiesOnWave = currentWave * 2;
        }
        else {
            enemiesOnWave = 10 + currentWave;
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
        Instantiate(enemiesCanSpawn[Random.Range(0, enemiesCanSpawn.Count - 1)], spawnPoint, Quaternion.identity);
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
