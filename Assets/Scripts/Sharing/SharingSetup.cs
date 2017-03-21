using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SharingSetup : MonoBehaviour {

    public Text titleText;
    public Text scoreTitleText;
    public Text scoreText;
    public bool isBestScore;
    public GameObject SharingCanvas;
    public Mode md;

    int bestScore;

    string kTimedTitle = "Timed Mode";
    string kZenTitle = "Zen Mode";
    string kEndlessTitle = "Endless Mode";
    string kBestScore = "BEST SCORE";
    string kScore = "";

    void Start () {
	
	}

    public void SetModeAndOpenShareScreen(Mode md) {
        if (md == Mode.Timed)
        {
            titleText.text = kTimedTitle;
            bestScore = DataControl.control.bestScoreTimed;
        }
        else if (md == Mode.Endless)
        {
            titleText.text = kEndlessTitle;
            bestScore = DataControl.control.bestScoreEndless;
        }
        else if (md == Mode.Zen) {
            titleText.text = kZenTitle;
            bestScore = DataControl.control.bestScoreZen;
        }

        if (isBestScore) {
            scoreText.text = bestScore.ToString();
            scoreTitleText.text = kBestScore;
        }
        else {
            scoreText.text = (ScoreManager.score).ToString();
            scoreTitleText.text = kScore;
        }

        ScreenManager.screenManager.WithoutAdditionalAnimator();
        ScreenManager.screenManager.OpenScreen(SharingCanvas);
        
    }
    public void GetModeAndOpenShareScreen() {
        if (GameManager.manager.mode != Mode.Unassigned)
        {
            SetModeAndOpenShareScreen(GameManager.manager.mode);
        }
        else {
            SetModeAndOpenShareScreen(md);
        }
    }
}
