using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUi : MonoBehaviour
{
    public RowUI rowUi;
    public ScoreManagerScript scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ScoreManagerScript>().AddScore(new Score(name: "eran", score: 6));
        GetComponent<ScoreManagerScript>().AddScore(new Score(name: "eran", score: 66));
        //scoreManager.AddScore(new Score(name: "eran", score: 6));
        //scoreManager.AddScore(new Score(name: "Mikasa", score: 66));
        List<Score> scores = scoreManager.GetHighScores();
        for (int i = 0; i < scores.Count; i++)
        {
            var row = Instantiate(rowUi, transform).GetComponent<RowUI>();
            row.rank.text = (i + 1).ToString();
            row.name.text = scores[i].name;
            row.score.text = scores[i].score.ToString();
        }
        //int i = 0;
        //foreach( Score scorerow in scores)
        //{
        //    var row = Instantiate(rowUi, transform).GetComponent<RowUI>();
        //    row.rank.text = (i + 1).ToString();
        //    row.name.text = scorerow.name;
        //    row.score.text = scorerow.score.ToString();
        //    i++;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
