using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {

    public TextMeshProUGUI scoreText;
    public int score;

    public void AddScore(int ctx) {
        score += ctx;
        scoreText.text = score.ToString();
    }

}
