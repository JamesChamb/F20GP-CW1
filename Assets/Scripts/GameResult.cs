using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{

    public int score = 0;
    public int timeScore = 0;
    public int totalScore = 0;
    public Text gameOverText;
    public Text scoreText;

    void Start()
    {
        //scoreText = gameObject.GetComponent<Text>();
        scoreText.text = "Score: 0";
    }

    void Update()
    {
        scoreText.text = "Score: " + (score.ToString());
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            timeScore = 100000/(int)Time.time;
            totalScore = timeScore + score;
            gameOverText.text = "Score: " + (score.ToString()) + "\n Speed Score: " + (timeScore.ToString()) + "\n Total Score: " + (totalScore.ToString());
        }
    }

}
