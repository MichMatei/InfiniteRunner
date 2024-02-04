using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    public Text ticketText;
    public Text highscoreText;
    
    // Sets the score to 0 at the start of a run.
    public int score = 0;
    public int ticket = 0;
    public int highscore = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ticket = PlayerPrefs.GetInt("ticket", 0);
        highscore = PlayerPrefs.GetInt("highscore", 0);
        //Displays Tickets on screen
        scoreText.text = score.ToString() + " SCORE";
        ticketText.text = ticket.ToString() + " TICKETS";
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }

    public void AddPoint()
    {
        //Score added per chunk ran.
        score += 5;
        ticket += 1;
        ticketText.text = ticket.ToString() + " TICKETS";
        scoreText.text = score.ToString() + " POINTS";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        ticketText.text = "Tickets: " + ticket;

        if (ManagerScript.Instance.playerDead)
        {
            if (highscore < score)
            {
                PlayerPrefs.SetInt("highscore", score);
            }
        }
    }
}

