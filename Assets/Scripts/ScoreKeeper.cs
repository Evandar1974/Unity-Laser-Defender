using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
    private Text text;
    private int score;
	// Use this for initialization
	void Start ()
    {
        text = GetComponent<Text>();
        Reset();
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    public void Score(int points)
    {
        score += points;
        text.text = score.ToString();
    }

    public void Reset()
    {
        score = 0;
        text.text = score.ToString();
    }
}
