﻿using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataControl : MonoBehaviour
{
    public static DataControl control;

    public int bestScoreTimed;
    public int bestScoreEndless;
    public int bestScoreZen;

    public int scoreMultiplier;

    public bool isTutorialFinished;

    const string path = "/playerInfo.dat";

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;

        }
        else if (control != null)
        {
            Destroy(gameObject);
        }
    }

    bool ifFileExist()
    {
        if (File.Exists(Application.persistentDataPath + path))
        {
            return true;
        }
        else
        {
            Debug.Log("Failed to load data. Set default");
            SetDefaultData();
            return false;
        }
    }

    public void SaveAll()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + path);
        PlayerData data = new PlayerData();

        data.amountTimeStops = PowerUpsManager.manager.amountTimeStops;
        data.amountSparks = PowerUpsManager.manager.amountSparks;
        data.amountWings = PowerUpsManager.manager.amountWings;

        data.bestScoreTimed = bestScoreTimed;
        data.bestScoreEndless = bestScoreEndless;
        data.bestScoreZen = bestScoreZen;

        data.bank = BankManager.bank;

        data.isScoreDoublerEnabled = ScoreManager.manager.isScoreDoublerEnabled;

        data.isVibrationOn = SettingsManager.manager.isVibrationOn;
        data.isAudioOn = SettingsManager.manager.isAudioOn;

        data.isEndlessModePlayed = GameManager.manager.isEndlessModePlayed;
        data.isTimedModePlayed = GameManager.manager.isTimedModePlayed;
        data.isZenModePlayed = GameManager.manager.isZenModePlayed;
        data.isTutorialPlayed = isTutorialFinished;

        data.theme = SettingsManager.manager.theme;

        bf.Serialize(file, data);
        file.Close();
    }
    public void LoadAll()
    {
        if (ifFileExist())
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + path, FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            PowerUpsManager.manager.amountTimeStops = data.amountTimeStops;
            PowerUpsManager.manager.amountSparks = data.amountSparks;
            PowerUpsManager.manager.amountWings = data.amountWings;
            PowerUpsManager.manager.SetWingsAmount();
            PowerUpsManager.manager.SetSparksAmount();
            PowerUpsManager.manager.SetTimeStopsAmount();

            bestScoreTimed = data.bestScoreTimed;
            BestTimedScore.isUpdated = true;
            bestScoreZen = data.bestScoreZen;
            BestZenScore.isUpdated = true;
            bestScoreEndless = data.bestScoreEndless;
            BestEndlessScore.isUpdated = true;

            BankManager.bank = data.bank;
            BankManager.isBankChanged = true;

            ScoreManager.manager.isScoreDoublerEnabled = data.isScoreDoublerEnabled;
            ScoreManager.manager.SetPointsNumber();

            SettingsManager.manager.isVibrationOn = data.isVibrationOn;
            SettingsManager.manager.isAudioOn = data.isAudioOn;
            SettingsManager.manager.theme = data.theme;
            if (data.theme == Theme.Light)
            {
                SettingsManager.manager.SetLightTheme();
            }
            else if (data.theme == Theme.Dark) {
                SettingsManager.manager.SetDarkTheme();
            }
            SettingsManager.manager.SetTexts();

            GameManager.manager.isEndlessModePlayed = data.isEndlessModePlayed;
            GameManager.manager.isTimedModePlayed = data.isTimedModePlayed;
            GameManager.manager.isZenModePlayed = data.isZenModePlayed;
            isTutorialFinished = data.isTutorialPlayed;
        }
    }

    public void SetDefaultData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + path);
        PlayerData data = new PlayerData();

        data.amountTimeStops = 5;
        PowerUpsManager.manager.amountTimeStops = 5;
        data.amountSparks = 5;
        PowerUpsManager.manager.amountSparks = 5;
        data.amountWings = 5;
        PowerUpsManager.manager.amountWings = 5;
        PowerUpsManager.manager.SetWingsAmount();
        PowerUpsManager.manager.SetSparksAmount();
        PowerUpsManager.manager.SetTimeStopsAmount();

        data.bestScoreTimed = 0;
        bestScoreTimed = 0;
        data.bestScoreEndless = 0;
        bestScoreEndless = 0;
        data.bestScoreZen = 0;
        bestScoreZen = 0;
        BestTimedScore.isUpdated = true;
        BestZenScore.isUpdated = true;
        BestEndlessScore.isUpdated = true;

        data.bank = 500;
        BankManager.bank = 500;
        BankManager.isBankChanged = true;
    
        data.isVibrationOn = true;
        SettingsManager.manager.isVibrationOn = true;
        data.isAudioOn = true;
        SettingsManager.manager.isAudioOn = true;
        SettingsManager.manager.SetLightTheme();    

        data.isEndlessModePlayed = false;
        GameManager.manager.isEndlessModePlayed = false;
        data.isTimedModePlayed = false;
        GameManager.manager.isTimedModePlayed = false;
        data.isZenModePlayed = false;
        GameManager.manager.isZenModePlayed = false;
        data.isTutorialPlayed = false;
        isTutorialFinished = false;

        data.isScoreDoublerEnabled = false;
        ScoreManager.manager.isScoreDoublerEnabled = false;
        ScoreManager.manager.SetPointsNumber();

        bf.Serialize(file, data);
        file.Close();
    }

    public void SetNewBestTimedScore(int newScore)
    {
        bestScoreTimed = newScore;
        BestTimedScore.isUpdated = true;
    }
    public void SetNewBestEndlessScore(int newScore)
    {
        bestScoreEndless = newScore;
        BestEndlessScore.isUpdated = true;
    }
    public void SetNewBestZenScore(int newScore)
    {
        bestScoreZen = newScore;
        BestZenScore.isUpdated = true;
    }
}

[Serializable]
class PlayerData
{
    public int amountTimeStops;
    public int amountSparks;
    public int amountWings;

    public int bestScoreTimed;
    public int bestScoreEndless;
    public int bestScoreZen;

    public int bank;

    public bool isAudioOn;
    public bool isVibrationOn;

    public bool isTutorialPlayed;
    public bool isTimedModePlayed;
    public bool isEndlessModePlayed;
    public bool isZenModePlayed;

    public bool isScoreDoublerEnabled;

    public Theme theme;
}
