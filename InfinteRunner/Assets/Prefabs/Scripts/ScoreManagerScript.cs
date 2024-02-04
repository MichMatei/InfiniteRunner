using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class ScoreManagerScript : MonoBehaviour
{
    private ScoreData sd;
    private void Awake()
    {
        var json = PlayerPrefs.GetString(key: "scores", defaultValue: "{}");
        sd = JsonUtility.FromJson<ScoreData>(json);
    }
    public List<Score> GetHighScores()
    {
        return sd.scores.OrderByDescending(keySelector: x => x.score).ToList<Score>();

    }

    public void AddScore(Score score)
    {
        sd.scores.Add(score);
    }
    private void OnDestroy()
    {
        SaveScore();
    }
    public void SaveScore()
    {
        var json = JsonUtility.ToJson(sd);
        PlayerPrefs.SetString("score", json);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
   
}
