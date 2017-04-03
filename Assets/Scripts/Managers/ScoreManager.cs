using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public static int score;
    public Text scoreText;

	// Use this for initialization
	void Start () {
        score = 0;
	}
	
    void Update()
    {
        scoreText.text = score.ToString();
    }

    
}
