using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsManager : MonoBehaviour {
    public static SettingsManager manager;

    public bool isAudioOn;
    public Text audioButton;

    public bool isVibrationOn;
    public Text vibroButton;

    string kValueOn = "on";
    string kValueOff = "off";

    string kLightTheme = "light";
    string kDarkTheme = "dark";

    public Theme theme;
    public Text themeButton;

    public Material materialFirst;
    public Material materialSecond;

    public Color colorFirst = new Color(0,0,0,255);
    public Color colorSecond = new Color (22,22,29,255);

    public Camera mainCamera;

    public Text versionText;

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

        if (isAudioOn)
        {
            audioButton.text = LocalizationManager.manager.GetLocalizedValue(kValueOn);
        }
        else
        {
            audioButton.text = LocalizationManager.manager.GetLocalizedValue(kValueOff);
        }
    }

    public void VibroSwitch()
    {
        isVibrationOn = !isVibrationOn;

        if (isVibrationOn)
        {
            vibroButton.text = LocalizationManager.manager.GetLocalizedValue(kValueOn);
        }
        else
        {
            vibroButton.text = LocalizationManager.manager.GetLocalizedValue(kValueOff);
        }
    }

    public void ThemeSwitch() { 
        if (theme == Theme.Light)
        {
            SetDarkTheme();          
        }
        else if (theme == Theme.Dark)
        {
            SetLightTheme();       
        }
    }

    public void SetDarkTheme() {
        theme = Theme.Dark;

        materialFirst.SetColor("_Color", colorSecond);
        materialSecond.SetColor("_Color", colorFirst);
        mainCamera.backgroundColor = colorSecond;
        themeButton.text = LocalizationManager.manager.GetLocalizedValue(kDarkTheme);
    }

    public void SetLightTheme() {
        theme = Theme.Light;

        materialFirst.SetColor("_Color", colorFirst);
        materialSecond.SetColor("_Color", colorSecond);
        mainCamera.backgroundColor = colorFirst;
        themeButton.text = LocalizationManager.manager.GetLocalizedValue(kLightTheme);
    }

    public void Vibrate() {
        if (isVibrationOn) {
            Handheld.Vibrate();
        }
    }

    //used in DataControl
    public void SetTexts() {
        if (isAudioOn)
        {
            audioButton.text = LocalizationManager.manager.GetLocalizedValue(kValueOn);
        }
        else
        {
            audioButton.text = LocalizationManager.manager.GetLocalizedValue(kValueOff);
        }
        if (isVibrationOn)
        {
            vibroButton.text = LocalizationManager.manager.GetLocalizedValue(kValueOn);
        }
        else
        {
            vibroButton.text = LocalizationManager.manager.GetLocalizedValue(kValueOff);
        }
        if (theme == Theme.Dark)
        {
            themeButton.text = LocalizationManager.manager.GetLocalizedValue(kDarkTheme);
        }
        else {
            themeButton.text = LocalizationManager.manager.GetLocalizedValue(kLightTheme);
        }
        versionText.text = Application.version;
    }
}

public enum Theme
{
    Light, Dark
}

