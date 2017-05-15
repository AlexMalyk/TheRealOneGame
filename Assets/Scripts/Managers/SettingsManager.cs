using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsManager : MonoBehaviour {
    public static SettingsManager manager;

    public bool isAudioOn;
    public Text audioButton;

    public bool isVibrationOn;
    public Text vibroButton;

    string kValueOn = "ON";
    string kValueOff = "OFF";

    string kLightTheme = "Light";
    string kDarkTheme = "Dark";

    public Theme theme;
    public Text themeButton;

    public Material materialFirst;
    public Material materialSecond;

    public Color colorFirst = new Color(0,0,0,255);
    public Color colorSecond = new Color (22,22,29,255);

    public Camera mainCamera;

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

    public void AudioSwitch() {
        isAudioOn = !isAudioOn;
        SetTexts();
    }

    public void VibroSwitch()
    {
        isVibrationOn = !isVibrationOn;
        SetTexts();
    }

    string SetValue(bool field) {
        if (field) {
            return kValueOn;
        }
        else {
            return kValueOff;
        }
    }

    public void SetTexts() {
        if (isAudioOn){
            audioButton.text = kValueOn;
        }
        else{
            audioButton.text = kValueOff;
        }

        if (isVibrationOn){
            vibroButton.text = kValueOn;
        }
        else{
            vibroButton.text = kValueOff;
        }

        if (theme == Theme.Light) {
            themeButton.text = kLightTheme;
        }
        else if(theme == Theme.Dark)
        {
            themeButton.text = kDarkTheme;
        }
    }

    public void ThemeSwitch() { 
        if (theme == Theme.Light)
        {
            Debug.Log("light theme");

            SetDarkTheme();          
        }
        else if (theme == Theme.Dark)
        {
            Debug.Log("dark theme");

            SetLightTheme();       
        }
    }

    public void SetDarkTheme() {
        theme = Theme.Dark;

        materialFirst.SetColor("_Color", colorSecond);
        materialSecond.SetColor("_Color", colorFirst);

        mainCamera.backgroundColor = colorSecond;
        SetTexts();
    }

    public void SetLightTheme() {
        theme = Theme.Light;

        materialFirst.SetColor("_Color", colorFirst);
        materialSecond.SetColor("_Color", colorSecond);

        mainCamera.backgroundColor = colorFirst;
        SetTexts();
    }
}

public enum Theme
{
    Light, Dark
}

