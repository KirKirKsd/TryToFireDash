using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI moneyText;
    private int score;
    private int tempScore;
    private int money;
    private int tempMoney;
    public GameObject skipButtonUI;

    private void Start() {
        Cursor.visible = true;
        score = PlayerPrefs.GetInt("LastScore");
        money = score / 10;
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + money);
    }

    private void Update() {
        if (tempScore < score) {
            tempScore += 1;
            scoreText.text = tempScore.ToString();
        }

        if (tempScore == score) {
            if (tempMoney < money) {
                tempMoney += 1;
                moneyText.text = "+" + tempMoney;
            }
        }

        if (money == tempMoney) {
            skipButtonUI.SetActive(false);
        } 
    }

    public void ToMenu() {
        SceneManager.LoadScene(0);
    }

    public void Skip() {
        skipButtonUI.SetActive(false);
        tempScore = score;
        scoreText.text = tempScore.ToString();
        tempMoney = money;
        moneyText.text = "+" + tempMoney;
    }

}
