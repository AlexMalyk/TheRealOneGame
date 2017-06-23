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
    public float time;
    [HideInInspector]
    public bool timeStopsUsed;
    [HideInInspector]
    public float timeStopsTimer = 10f;
    [HideInInspector]
    public int kTimedModeTime = 61;
    [HideInInspector]
    public int kEndlessModeTime = 11;

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
    public bool isTutorialFinished;

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

        PowerUpsManager.manager.DeleteEffects(true, true, true);

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
        AudioManager.manager.PlayPositiveSound();
        PowerUpsManager.manager.RemoveWings();
        

        isGameOver = true;
        if (mode == Mode.Timed)
        {
            if (ScoreManager.manager.score > ScoreManager.manager.bestScoreTimed)
            {
                ScoreManager.manager.SetNewBestTimedScore (ScoreManager.manager.score);

                isNewRecord = true;
            }
            
            isTimedModePlayed = true;
            BestScoreText.text = (ScoreManager.manager.bestScoreTimed).ToString();
        }
        else if (mode == Mode.Endless) {
            if (ScoreManager.manager.score > ScoreManager.manager.bestScoreEndless)
            {
                ScoreManager.manager.SetNewBestEndlessScore (ScoreManager.manager.score);

                isNewRecord = true;
            }
            isEndlessModePlayed = true;
            BestScoreText.text = (ScoreManager.manager.bestScoreEndless).ToString();
        }
        else if (mode == Mode.Zen) {
            if (ScoreManager.manager.score > ScoreManager.manager.bestScoreZen)
            {
                ScoreManager.manager.SetNewBestZenScore (ScoreManager.manager.score);

                isNewRecord = true;
            }
            isZenModePlayed = true;
            BestScoreText.text = (ScoreManager.manager.bestScoreZen).ToString();
        }

        Analytics.CustomEvent("Played game info", new Dictionary<string, object> {
                    { "mode", mode },
                    { "theme", SettingsManager.manager.theme },
                    { "score", ScoreManager.manager.score },
                    { "language", LocalizationManager.manager.language }
                });

        EndScoreText.text = (ScoreManager.manager.score).ToString();

        BankManager.bank += ScoreManager.manager.score;
        BankManager.isBankChanged = true;

        DataManager.manager.SaveAll();
        AdManager.manager.SetupSpecialAd();
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

    public void TimedMode()
    {
        time = kTimedModeTime;
        mode = Mode.Timed;

        SetZenModeUI(false);

        StartGame();
    }

    public void EndlessMode()
    {
        time = kEndlessModeTime;
        mode = Mode.Endless;

        SetZenModeUI(false);

        StartGame();
    }

    public void ZenMode()
    {
        mode = Mode.Zen;

        SetZenModeUI(true);

        StartGame();
    }

    void SetZenModeUI(bool value) {
        TimeStop.SetActive(!value);
        eyeContainer.SetActive(value);
        if (value)
        {
			timerDisplay.text = "";
			
            eyesAnimators = eyeContainer.GetComponentsInChildren<Animator>();
            foreach (Animator anim in eyesAnimators)
                anim.SetBool("showed", true);
        }
        infinitySymbol.SetActive(value);
    }

    void Update()
    {
        if (!isGameOver && isGameRunning && mode != Mode.Zen && !timeStopsUsed && ScreenManager.screenManager.isTransition == false) { 
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
