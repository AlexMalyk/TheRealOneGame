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

    void Start()
    {
        Debug.Log("DataManager Start to loadALL");
        LoadAll();
    }

    bool ifFileExist()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            return true;
        }
        else
        {
            Debug.Log("Failed to load");
            return false;
        }
    }

    public void SaveAll()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();

        data.amountTimeStops = HintManager.manager.amountTimeStops;
        data.amountFlashes = HintManager.manager.amountFlashes;
        data.amountFlankers = HintManager.manager.amountFlankers;

        data.bestScoreTimed = bestScoreTimed;
        data.bestScoreEndless = bestScoreEndless;
        data.bestScoreZen = bestScoreZen;

        data.bank = BankManager.bank;

        data.isVibrationOn = SettingsManager.manager.isVibrationOn;
        data.isAudioOn = SettingsManager.manager.isAudioOn;

        data.isEndlessModePlayed = GameManager.manager.isEndlessModePlayed;
        data.isTimedModePlayed = GameManager.manager.isTimedModePlayed;
        data.isZenModePlayed = GameManager.manager.isZenModePlayed;
        data.isTutorialPlayed = GameManager.manager.isTutorialPlayed;



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
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            HintManager.manager.amountTimeStops = data.amountTimeStops;
            HintManager.manager.amountFlashes = data.amountFlashes;
            HintManager.manager.amountFlankers = data.amountFlankers;

            bestScoreTimed = data.bestScoreTimed;
            bestScoreZen = data.bestScoreZen;
            bestScoreEndless = data.bestScoreEndless;

            BankManager.bank = data.bank;

            SettingsManager.manager.isVibrationOn = data.isVibrationOn;
            SettingsManager.manager.isAudioOn = data.isAudioOn;

            GameManager.manager.isEndlessModePlayed = data.isEndlessModePlayed;
            GameManager.manager.isTimedModePlayed = data.isTimedModePlayed;
            GameManager.manager.isZenModePlayed = data.isZenModePlayed;
            GameManager.manager.isTutorialPlayed = data.isTutorialFinished;

            Debug.Log("All data loaded. PU1=" + HintManager.manager.amountTimeStops + ", PU2=" + HintManager.manager.amountFlashes + ", PU3=" + HintManager.manager.amountFlankers +
                        ", bestScoreTimed=" + bestScoreTimed + ", bestScoreZen=" + bestScoreZen + ", bestScoreEndless=" + bestScoreEndless +
                        ", bank=" + BankManager.bank + ", isVibratioOn=" + SettingsManager.manager.isVibrationOn + ", isAudioOn=" + SettingsManager.manager.isAudioOn +
                        ", isEndlessModePlayed=" + GameManager.manager.isEndlessModePlayed + ", isTimedModePlayed=" + GameManager.manager.isTimedModePlayed +
                        ", isZenModePlayed=" + GameManager.manager.isZenModePlayed + ", isTutorialPlayed=" + GameManager.manager.isTutorialPlayed);
        }
    }

    public void SavePU()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();

        data.amountPU1 = HintManager.manager.amountPU1;
        data.amountPU2 = HintManager.manager.amountPU2;
        data.amountPU3 = HintManager.manager.amountPU3;

        bf.Serialize(file, data);
        file.Close();

        Debug.Log("PU data saved. PU1=" + data.amountPU1 + ", PU2=" + data.amountPU2 + ", PU3=" + data.amountPU3);
    }
    public void LoadPU()
    {
        if (ifFileExist())
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            HintManager.manager.amountPU1 = data.amountPU1;
            HintManager.manager.amountPU2 = data.amountPU2;
            HintManager.manager.amountPU3 = data.amountPU3;

            Debug.Log("PU data loaded. PU1=" + HintManager.manager.amountPU1 + ", PU2=" + HintManager.manager.amountPU2 + ", PU3=" + HintManager.manager.amountPU3);
        }

    }
    public void SaveBank()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();

        data.bank = BankManager.bank;

        bf.Serialize(file, data);
        file.Close();

        Debug.Log("bank data saved. bank=" + data.bank);
    }

    public void LoadBank()
    {
        if (ifFileExist())
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            BankManager.bank = data.bank;
            Debug.Log("Bank data loaded. bank=" + BankManager.bank);
        }
    }
    public void SaveBestScores()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();

        data.bestScoreTimed = bestScoreTimed;
        data.bestScoreZen = bestScoreZen;
        data.bestScoreEndless = bestScoreEndless;

        bf.Serialize(file, data);
        file.Close();

        //Debug.Log("Score data saved. best=" + bestScore);
    }

    public void LoadBestScores()
    {
        if (ifFileExist())
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            bestScoreTimed = data.bestScoreTimed;
            bestScoreZen = data.bestScoreZen;
            bestScoreEndless = data.bestScoreEndless;

            //Debug.Log("Score data loaded. best=" + bestScore);
        }
    }

    public void SaveSettings()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();

        data.isVibrationOn = SettingsManager.manager.isVibrationOn;
        data.isAudioOn = SettingsManager.manager.isAudioOn;

        bf.Serialize(file, data);
        file.Close();
    }
    public void LoadSettings()
    {
        if (ifFileExist())
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            SettingsManager.manager.isAudioOn = data.isAudioOn;
            SettingsManager.manager.isVibrationOn = data.isVibrationOn;
        }
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

    public bool isTutorialFinished;
    public bool isTimedModePlayed;
    public bool isEndlessModePlayed;
    public bool isZenModePlayed;
}
