﻿using UnityEngine;
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
        AudioManager.manager.PlayNegativeSound();
        

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
        else if (mode == Mode.Endless)
        {
            EndlessMode();
        }
        else if (mode == Mode.Zen) {
            ZenMode();
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
        timeLimit = kTimedModeTime;
        time = kTimedModeTime;
        Debug.Log("time="+time+" limit="+timeLimit);
        mode = Mode.Timed;

        SetZenModeUI(false);

        StartGame();
    }

    public void EndlessMode()
    {
        Debug.Log("GM five second mode");
        timeLimit = kEndlessModeTime;
        time = kEndlessModeTime;
        mode = Mode.Endless;

        SetZenModeUI(false);

        StartGame();
    }

    //бесконечное время и три жизни
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
        if (!isGameOver && isGameRunning && mode != Mode.Zen)
        {
            if (time < 1)
            {
                Debug.Log("update if if ");
                EndGame();
            }
            else
            {
                // Decrease timeLimit.
                time -= Time.deltaTime;
                timerDisplay.text = ((int)time).ToString();
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