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
    // Use this for initialization
    void Start () {
 
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
        if (isAudioOn)
        {
            audioButton.text = kValueOn;
        }
        else
        {
            audioButton.text = kValueOff;
        }

        if (isVibrationOn)
        {
            vibroButton.text = kValueOn;
        }
        else
        {
            vibroButton.text = kValueOff;
        }
    }
}
