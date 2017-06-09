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
    public bool timeStopsUsed;
    [HideInInspector]
    public float timeStopsTimer = 10f;

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

    public GameObject endGameCanvas;
    public GameObject GameBoard;
    public GameObject GameScreen;

    public Text BestScoreText;
    public Text EndScoreText;
    public Text ScoreText;
    public Text timerDisplay;

    public GameObject eyeContainer;
    public GameObject TimeStop;
    public GameObject infinitySymbol;

    [HideInInspector]
    public int kTimedModeTime = 61;
    [HideInInspector]
    public int kEndlessModeTime = 11;

    int timeInt;
    Animator[] eyesAnimators;

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
        ScoreManager.manager.SetToZero();

        MissInput.lifesCount = 3;

        Analytics.CustomEvent("start game", new Dictionary<string, object>
        {
            {"pu1", PowerUpsManager.manager.amountTimeStops },
            {"pu2", PowerUpsManager.manager.amountSparks },
            {"pu3", PowerUpsManager.manager.amountWings }
        });

        PowerUpsManager.manager.DeleteEffects(true, true, true);

        ScreenManager.screenManager.SetAdditionalAnimator(GameScreen);
        ScreenManager.screenManager.OpenScreen(GameBoard);

        while (ScreenManager.screenManager.isTransition) {}
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
        AudioManager.manager.PlayNegativeSound();
        

        isGameOver = true;
        if (mode == Mode.Timed)
        {
            if (ScoreManager.manager.score > DataControl.control.bestScoreTimed)
            {
				DataControl.control.SetNewBestTimedScore (ScoreManager.manager.score);

                isNewRecord = true;
            }

            isTimedModePlayed = true;
            BestScoreText.text = (DataControl.control.bestScoreTimed).ToString();
        }
        else if (mode == Mode.Endless) {
            if (ScoreManager.manager.score > DataControl.control.bestScoreEndless)
            {
				DataControl.control.SetNewBestEndlessScore (ScoreManager.manager.score);

                isNewRecord = true;
            }
            isEndlessModePlayed = true;
            BestScoreText.text = (DataControl.control.bestScoreEndless).ToString();
        }
        else if (mode == Mode.Zen) {
            if (ScoreManager.manager.score > DataControl.control.bestScoreZen)
            {
				DataControl.control.SetNewBestZenScore (ScoreManager.manager.score);

                isNewRecord = true;
            }
            isZenModePlayed = true;
            BestScoreText.text = (DataControl.control.bestScoreZen).ToString();
        }    
        EndScoreText.text = (ScoreManager.manager.score).ToString();

        BankManager.bank += ScoreManager.manager.score;
        BankManager.isBankChanged = true;

        DataControl.control.SaveAll();
        AdControl.control.SetupSpecialAd();
        ScreenManager.screenManager.OpenScreen(endGameCanvas);
    }
    public void RestartGame()
    {
        isGameRestarted = true;
        isGameOver = true;

        ScreenManager.screenManager.CloseAllOpenedScreens();
        ScreenManager.screenManager.isMenuOpen = false;
        if (mode == Mode.Timed)
        {
            TimedMode();
        }
        else if (mode == Mode.Endless)
        {
            EndlessMode();
        }
        else if (mode == Mode.Zen) {
            ZenMode();
        }
    }
    public void PlayAgain()
    {
        RestartGame();
    }

    public void TimedMode()
    {
        timeLimit = kTimedModeTime;
        time = kTimedModeTime;
        mode = Mode.Timed;

        SetZenModeUI(false);

        StartGame();
    }

    public void EndlessMode()
    {
        timeLimit = kEndlessModeTime;
        time = kEndlessModeTime;
        mode = Mode.Endless;

        SetZenModeUI(false);

        StartGame();
    }

    public void ZenMode()
    {
        MissInput.lifesCount = 3;
        mode = Mode.Zen;
        timerDisplay.text = "";

        SetZenModeUI(true);

        StartGame();
    }

    void SetZenModeUI(bool value) {
        TimeStop.SetActive(!value);
        eyeContainer.SetActive(value);
        eyesAnimators = eyeContainer.GetComponentsInChildren<Animator>();
        foreach (Animator anim in eyesAnimators)
            anim.SetTrigger("Show");
        infinitySymbol.SetActive(value);
    }

    void Update()
    {
        if (!isGameOver && isGameRunning && mode != Mode.Zen && !timeStopsUsed)
        {
            if (time < 1)
            {
                EndGame();
            }
            else
            {
                time -= Time.deltaTime;
                timerDisplay.text = ((int)time).ToString();
            }
        }
        else if (timeStopsUsed) {
            if (timeStopsTimer > 0)
            {
                timeStopsTimer -= Time.deltaTime;
            }
            else {
                timeStopsUsed = false;
                timeStopsTimer = 10f;
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
