using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameModesSetup : MonoBehaviour {

    public Sprite TimedModeImage;
    public Sprite EndlessModeImage;
    public Sprite ZenModeImage; 

    public Image image;
    public Text titleText;
    public Text descriptionText;
    public Button button;
    public Text buttonText;
    public GameObject GameModeCanvas;

    string kStart = "start";
    string kTimedDescription = "timed_description";
    string kEndlessDescription = "endless_description";
    string kZenDescription = "zen_description";
    string kTimedMode = "timed_mode";
    string kEndlessMode = "endless_mode";
    string kZenMode = "zen_mode";

    public void TimedMode() {
        //if this mode is  already played then start the game
        if (GameManager.manager.isTimedModePlayed)
        {
            GameManager.manager.TimedMode();
        }
        //if this mode is never played then show description 
        else {
            titleText.text = LocalizationManager.manager.GetLocalizedValue(kTimedMode);
            descriptionText.text = LocalizationManager.manager.GetLocalizedValue(kTimedDescription);
            image.GetComponent<Image>().sprite = TimedModeImage;
            buttonText.text = LocalizationManager.manager.GetLocalizedValue(kStart);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => GameManager.manager.TimedMode());

            ScreenManager.screenManager.OpenScreen(GameModeCanvas);
        }
    }

    public void EndlessMode()
    {
        //if this mode is already played then start the game
        if (GameManager.manager.isEndlessModePlayed)
        {
            GameManager.manager.EndlessMode();
        }
        else
        {
            titleText.text = LocalizationManager.manager.GetLocalizedValue(kEndlessMode);
            descriptionText.text = LocalizationManager.manager.GetLocalizedValue(kEndlessDescription);
            image.GetComponent<Image>().sprite = EndlessModeImage;

            buttonText.text = LocalizationManager.manager.GetLocalizedValue(kStart);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => GameManager.manager.EndlessMode());

            ScreenManager.screenManager.OpenScreen(GameModeCanvas);
        }
    }

    public void ZenMode()
    {
        //if this mode is already played(and purchased) then start the game
        if (GameManager.manager.isZenModePlayed)
        {
            GameManager.manager.ZenMode();
        }
        else
        {
            titleText.text = LocalizationManager.manager.GetLocalizedValue(kZenMode);
            descriptionText.text = LocalizationManager.manager.GetLocalizedValue(kZenDescription);
            image.GetComponent<Image>().sprite = ZenModeImage;
            buttonText.text = LocalizationManager.manager.GetLocalizedValue(kStart);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => GameManager.manager.ZenMode());

            ScreenManager.screenManager.OpenScreen(GameModeCanvas);
        }
    }
}
