using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waves : MonoBehaviour {

    private int currentWave;

    public List<GameObject> enemiesToBonfire;
    public List<GameObject> enemiesToPlayer;
    public List<GameObject> enemiesToBoth;

    private List<GameObject> enemiesCanSpawn;
    private int enemiesOnWave;
    private int enemiesLeft;
    private int enemiesDefeated = 2f;

    public float timeBetweenSpawn;
    
    private int randomI;

    private void Start() {
        StartWave();
    }

    private void StartWave() {
        currentWave += 1;

        switch (currentWave % 3) {
            case 1:
                randomI = Random.Range(0, enemiesToPlayer.Count - 1);
                enemiesCanSpawn.Append(enemiesToPlayer[randomI]);
                enemiesToPlayer.Remove(enemiesToPlayer[randomI]);
                break;
            case 2:
                randomI = Random.Range(0, enemiesToBoth.Count - 1);
                enemiesCanSpawn.Append(enemiesToBoth[randomI]);
                enemiesToBoth.Remove(enemiesToBoth[randomI]);
                break;
            case 0:
                randomI = Random.Range(0, enemiesToBonfire.Count - 1);
                enemiesCanSpawn.Append(enemiesToBonfire[randomI]);
                enemiesToBonfire.Remove(enemiesToBonfire[randomI]);
                break;
        }

        if (currentWave <= 5) {
            enemiesOnWave = currentWave * 2;
        }
        else {
            enemiesOnWave = 10 + currentWave;
        }

        StartCoroutine(DuringWave());
    }

    private IEnumerator DuringWave() {
        var spawnPoint = new Vector3(Random.Range(-20f, 20f), 1f, Random.Range(-20f, 20f));
        Instantiate(enemiesCanSpawn[Random.Range(0, enemiesCanSpawn.Count - 1)], spawnPoint, Quaternion.identity);
        enemiesLeft -= 1;

        if (enemiesLeft > 0) {
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    } 

}
