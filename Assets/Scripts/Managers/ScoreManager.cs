using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public static int score;
    public static bool isScoreChanged;

    public Text[] scoreTexts;

	// Use this for initialization
	void Start () {
        score = 0;
        isScoreChanged = false;

    }

    public static void ChangeScore() {
        score += 10;
        isScoreChanged = true;
    }

    void Update()
    {
        if (isScoreChanged)
        {
            foreach (Text item in scoreTexts)
            {
                item.text = score.ToString();
            }
            isScoreChanged = false;
        }
    }

    
}
