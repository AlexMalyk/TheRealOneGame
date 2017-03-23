using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager manager;
    [HideInInspector]
    public Mode mode;
    [HideInInspector]
    public float timeLimit;
    [HideInInspector]
    public float time;
    [HideInInspector]
    public float lifes;

    [HideInInspector]
    public bool isGameOver;
    [HideInInspector]
    public bool isGameRunning;
    [HideInInspector]
    public bool isGameRestarted;
    [HideInInspector]
    public bool isNewRecord;
    [HideInInspector]
    public bool isTimedModePlayed;
    [HideInInspector]
    public bool isEndlessModePlayed;
    [HideInInspector]
    public bool isZenModePlayed;
    [HideInInspector]
    public bool isTutorialPlayed;

    public GameObject endGameCanvas;
    public GameObject GameBoard;
    public GameObject GameScreen;

    public Text BestScoreText;
    public Text EndScoreText;
    public Text ScoreText;
    public Text timerDisplay;

    [HideInInspector]
    public int kTimedModeTime = 61;
    [HideInInspector]
    public int kEndlessModeTime = 6;

    int timeInt;

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

    void Start() {
        mode = Mode.Unassigned;

        isGameOver = false;
        isGameRunning = false;
        isGameRestarted = false;
        isNewRecord = false;
    }
    public void StartGame()
    {
        Debug.Log("GM Start game");
        ScoreManager.score = 0;

        MissInput.lifesCount = 3;

        Analytics.CustomEvent("start game", new Dictionary<string, object>
        {
            {"pu1", HintManager.manager.amountTimeStops },
            {"pu2", HintManager.manager.amountFlashes },
            {"pu3", HintManager.manager.amountFlankers }
        });

        HintManager.manager.DeleteEffects();

        ScreenManager.screenManager.SetAdditionalAnimator(GameScreen);
        ScreenManager.screenManager.OpenScreen(GameBoard);

        isGameRunning = true;
        isGameOver = false;
        isNewRecord = false;
        isGameRestarted = false;
    }

    public void PauseGame(bool value) {
        isGameRunning = !value;
    }


    public void EndGame()
    {
        isGameOver = true;
        if (mode == Mode.Timed)
        {
            if (ScoreManager.score > DataControl.control.bestScoreTimed)
            {
                DataControl.control.bestScoreTimed = ScoreManager.score;

                isNewRecord = true;
            }

            isTimedModePlayed = true;
            BestScoreText.text = (DataControl.control.bestScoreTimed).ToString();
        }
        else if (mode == Mode.Endless) {
            if (ScoreManager.score > DataControl.control.bestScoreEndless)
            {
                DataControl.control.bestScoreEndless = ScoreManager.score;

                isNewRecord = true;
            }
            isEndlessModePlayed = true;
            BestScoreText.text = (DataControl.control.bestScoreEndless).ToString();
        }
        else if (mode == Mode.Zen) {
            if (ScoreManager.score > DataControl.control.bestScoreZen)
            {
                DataControl.control.bestScoreZen = ScoreManager.score;             

                isNewRecord = true;
            }
            isZenModePlayed = true;
            BestScoreText.text = (DataControl.control.bestScoreZen).ToString();
        }

        
        AdControl.control.SetupSpecialAd();
        EndScoreText.text = (ScoreManager.score).ToString();

        BankManager.bank += ScoreManager.score;
        DataControl.control.SaveAll();
        ScreenManager.screenManager.OpenScreen(endGameCanvas);
    }
    public void RestartGame()
    {
        Debug.Log("GM restart game");
        isGameRestarted = true;
        isGameOver = true;

        ScreenManager.screenManager.CloseAllOpenedScreens();
        ScreenManager.screenManager.isMenuOpen = false;
        if (mode == Mode.Timed)
        {
            TimedMode();
        }
        else if(mode == Mode.Endless)
        {
            timeLimit = kEndlessModeTime;
            time = kEndlessModeTime;
        }

        
        //ScreenManager.screenManager.SetAdditionalAnimator(GameScreen);
        //StartGame();
    }
    public void PlayAgain()
    {
        Debug.Log("GM play again");
        RestartGame();
    }

    public void TimedMode()
    {
        Debug.Log("GM one minute mod");
        timeLimit = 62f;
        time = 62f;
        Debug.Log("time="+time+" limit="+timeLimit);
        mode = Mode.Timed;

        
        StartGame();
    }

    public void EndlessMode()
    {
        Debug.Log("GM five second mode");
        timeLimit = 7f;
        time = 7f;
        mode = Mode.Endless;

        
        StartGame();
    }

    //бесконечное время и три жизни
    public void ZenMode()
    {
        lifes = 3;
        mode = Mode.Zen;

        StartGame();
    }

    void Update()
    {
        if (!isGameOver && isGameRunning)
        {
            if (mode == Mode.Zen) {
                if (lifes == 0) {
                    EndGame();
                }
            }
            else {
                if (time < 1)
                {
                    Debug.Log("update if if ");
                    EndGame();
                }
                else
                {
                    // Decrease timeLimit.
                    time -= Time.deltaTime;
                    // translate backward.
                    transform.Translate(Vector3.back * Time.deltaTime, Space.World);
                    timeInt = (int)time;
                    //timerDisplay.text = timeInt.ToString();
                    timerDisplay.text = ((int)time).ToString();
                }
            }
            
        }
    }
    public void SetUnassignedMode() {
        mode = Mode.Unassigned;
    }
}

public enum Mode {
    Timed, Endless, Zen, Unassigned
}