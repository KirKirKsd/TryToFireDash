using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waves : MonoBehaviour {

    private int currentWave;

    public List<GameObject> enemiesToBonfire;
    public List<GameObject> enemiesToPlayer;
    public List<GameObject> enemiesToBoth;

    private List<GameObject> enemiesCanSpawn = new List<GameObject>();
    private int enemiesOnWave;
    private int enemiesLeft;
    private int enemiesDefeated;

    public float timeBetweenSpawn = 2;
    public float timeBetweenWaves = 10f;
    
    private int randomI;

    private void Start() {
        StartWave();
    }

    private void StartWave() {
        currentWave += 1;

        switch (currentWave % 3) {
            case 1:
                randomI = Random.Range(0, enemiesToPlayer.Count - 1);
                Debug.Log(enemiesToPlayer[0]);
                enemiesCanSpawn.Add(enemiesToPlayer[0]);
                enemiesToPlayer.Remove(enemiesToPlayer[randomI]);
                break;
            case 2:
                randomI = Random.Range(0, enemiesToBoth.Count);
                enemiesCanSpawn.Add(enemiesToBoth[randomI]);
                enemiesToBoth.Remove(enemiesToBoth[randomI]);
                break;
            case 0:
                randomI = Random.Range(0, enemiesToBonfire.Count);
                enemiesCanSpawn.Add(enemiesToBonfire[randomI]);
                enemiesToBonfire.Remove(enemiesToBonfire[randomI]);
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
        
        StartCoroutine(DuringWave());
    }

    private IEnumerator DuringWave() {
        var spawnPoint = new Vector3(Random.Range(-20f, 20f), 1f, Random.Range(-20f, 20f));
        Instantiate(enemiesCanSpawn[Random.Range(0, enemiesCanSpawn.Count - 1)], spawnPoint, Quaternion.identity);
        enemiesLeft -= 1;

        if (enemiesLeft > 0) {
            yield return new WaitForSeconds(timeBetweenSpawn);
            StartCoroutine(DuringWave());
        }
    }

    public void EnemyKilled() {
        enemiesDefeated += 1;
        if (enemiesDefeated == enemiesOnWave) {
            StartCoroutine(AfterWave());
        }
    }

    private IEnumerator AfterWave() {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartWave();
    }

}
