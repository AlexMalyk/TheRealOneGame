using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager manager;
    public int score;
    public bool isScoreChanged;

    public bool isScoreDoublerEnabled;

    public Text[] scoreTexts;
    public Text plusScoreText;

    public Text payDoublerText;

    const string kEnabled = "Enabled";
    const string kPay = "0,99$";

    public int pointsNumber;

    void Awake()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if (manager != null)
        {
            Destroy(gameObject);
        }
    }


    // Use this for initialization
    void Start () {
        SetToZero();
    }

    public void ChangeScore() {
        score += pointsNumber;
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

    public void SetToZero() {
        score = 0;
        isScoreChanged = true;
    }

    public void SetPointsNumber() {
        if (isScoreDoublerEnabled)
        {
            pointsNumber = 20;
            plusScoreText.text = "+20";
            payDoublerText.text = kEnabled;

        }
        else {
            pointsNumber = 10;
            plusScoreText.text = "+10";
            payDoublerText.text = kPay;

        }
    }

    public void EnableScoreDoubler() {
        if (!isScoreDoublerEnabled)
        {
            AudioManager.manager.PlayPositiveSound();
            isScoreDoublerEnabled = true;
            SetPointsNumber();
            DataControl.control.SaveAll();
        }
    }
}
