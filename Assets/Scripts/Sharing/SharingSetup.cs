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

    string kTimedKey = "timed_mode";
    string kZenKey = "zen_mode";
    string kEndlessKey = "endless_mode";
    string kBestScoreKey = "score_best_caps";
    string kEmpty = "";

    void Start () {
	
	}

    public void SetModeAndOpenShareScreen(Mode md) {
        if (md == Mode.Timed)
        {
            titleText.text = LocalizationManager.manager.GetLocalizedValue(kTimedKey);
            bestScore = DataControl.control.bestScoreTimed;
        }
        else if (md == Mode.Endless)
        {
            titleText.text = LocalizationManager.manager.GetLocalizedValue(kEndlessKey);
            bestScore = DataControl.control.bestScoreEndless;
        }
        else if (md == Mode.Zen) {
            titleText.text = LocalizationManager.manager.GetLocalizedValue(kZenKey);
            bestScore = DataControl.control.bestScoreZen;
        }

        if (isBestScore) {
            scoreText.text = bestScore.ToString();
            scoreTitleText.text = LocalizationManager.manager.GetLocalizedValue(kBestScoreKey);
        }
        else {
            scoreText.text = (ScoreManager.manager.score).ToString();
            scoreTitleText.text = kEmpty;
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
