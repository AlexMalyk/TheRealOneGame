﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{

    public static LocalizationManager manager;

    public bool rus = false;
    public bool eng = false;
    public bool ukr = false;

    public Language language;

    private Dictionary<string, string> localizedText;
    private bool isReady = false;
    private string missingTextString = "Localized text not found";

    private string filenameUkr = "localizedText_ua.json";
    private string filenameRus = "localizedText_ru.json";
    private string filenameEng = "localizedText_en.json";

    // Use this for initialization
    void Awake()
    {
        if (manager == null)
        {
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        string dataAsJson;

        if (Application.platform == RuntimePlatform.Android)
        {
            WWW reader = new WWW(filePath);
            while (!reader.isDone) { }
            dataAsJson = reader.text;
        }
        else if (File.Exists(filePath))
        {
            dataAsJson = File.ReadAllText(filePath);         
        }
        else {
            Debug.LogError("Cannot find file!");
            return;
        }
        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
        for (int i = 0; i < loadedData.items.Length; i++)
        {
            localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }
        isReady = true;
    }

    public void SetLanguage() {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Application.systemLanguage == SystemLanguage.Ukrainian)
            {
                language = Language.ukrainian;
                LoadLocalizedText(filenameUkr);
            }
            else if (Application.systemLanguage == SystemLanguage.Russian)
            {
                language = Language.russian;
                LoadLocalizedText(filenameRus);
            }
            else
            {
                language = Language.english;
                LoadLocalizedText(filenameEng);
            }
        }
        else {
            if (rus)
            {
                LoadLocalizedText(filenameRus);
                language = Language.russian;
            }
            else if (ukr)
            {
                LoadLocalizedText(filenameUkr);
                language = Language.ukrainian;
            }
            else {
                LoadLocalizedText(filenameEng);
                language = Language.english;
            }
        }
    }

    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }

        return result;

    }

    public bool GetIsReady()
    {
        return isReady;
    }

}

public enum Language { english, russian, ukrainian}