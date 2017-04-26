using UnityEngine;
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

        data.amountTimeStops = HintManager.manager.amountTimeStops;
        data.amountFlashes = HintManager.manager.amountFlashes;
        data.amountFlankers = HintManager.manager.amountFlankers;

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

        bf.Serialize(file, data);
        file.Close();

        Debug.Log("All data saved. PU1=" + data.amountTimeStops + ", PU2=" + data.amountFlashes + ", PU3=" + data.amountFlankers +
                        ", bestScoreTimed=" + data.bestScoreTimed + ", bestScoreZen=" + data.bestScoreZen + ", bestScoreEndless=" + data.bestScoreEndless +
                        ", bank=" + data.bank + ", isVibratioOn=" + data.isVibrationOn + ", isAudioOn=" + data.isAudioOn +
                        ", isEndlessModePlayed=" + data.isEndlessModePlayed + ", isTimedModePlayed=" + data.isTimedModePlayed +
                        ", isZenModePlayed=" + data.isZenModePlayed + ", isTutorialPlayed=" + data.isTutorialPlayed);
    }
    public void LoadAll()
    {
        if (ifFileExist())
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + path, FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            HintManager.manager.amountTimeStops = data.amountTimeStops;
            HintManager.manager.amountFlashes = data.amountFlashes;
            HintManager.manager.amountFlankers = data.amountFlankers;
            HintManager.manager.SetFlankersAmount();
            HintManager.manager.SetFlashesAmount();
            HintManager.manager.SetTimeStopsAmount();


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
            SettingsManager.manager.SetTexts();

            GameManager.manager.isEndlessModePlayed = data.isEndlessModePlayed;
            GameManager.manager.isTimedModePlayed = data.isTimedModePlayed;
            GameManager.manager.isZenModePlayed = data.isZenModePlayed;
            isTutorialFinished = data.isTutorialPlayed;

            Debug.Log("All data loaded. PU1=" + HintManager.manager.amountTimeStops + ", PU2=" + HintManager.manager.amountFlashes + ", PU3=" + HintManager.manager.amountFlankers +
                        ", bestScoreTimed=" + bestScoreTimed + ", bestScoreZen=" + bestScoreZen + ", bestScoreEndless=" + bestScoreEndless +
                        ", bank=" + BankManager.bank + ", isVibratioOn=" + SettingsManager.manager.isVibrationOn + ", isAudioOn=" + SettingsManager.manager.isAudioOn +
                        ", isEndlessModePlayed=" + GameManager.manager.isEndlessModePlayed + ", isTimedModePlayed=" + GameManager.manager.isTimedModePlayed +
                        ", isZenModePlayed=" + GameManager.manager.isZenModePlayed + ", isTutorialPlayed=" + isTutorialFinished);
        }
    }

    public void SetDefaultData()
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + path);
        PlayerData data = new PlayerData();

        data.amountTimeStops = 5;
        HintManager.manager.amountTimeStops = 5;
        data.amountFlashes = 5;
        HintManager.manager.amountFlashes = 5;
        data.amountFlankers = 5;
        HintManager.manager.amountFlankers = 5;
        HintManager.manager.SetFlankersAmount();
        HintManager.manager.SetFlashesAmount();
        HintManager.manager.SetTimeStopsAmount();



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
        SettingsManager.manager.SetTexts();

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
    public int amountFlashes;
    public int amountFlankers;

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
    public bool isZenModeEnabled;

}
