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

    string kStart = "Start";
    string kPay = "$1,99";

    static string kTimedDescription = "You'll have 60 seconds to find as many real dots as you can.";
    static string kEndlessDescription = "You'll have 5 seconds to find the real dot. After you found it the time restarts";
    static string kZenDescription = "Play without the limitations of time. In this mode you'll have three lifes, unlimited flankers and flashes.";

    public void TimedMode() {
        //if this modeis  already played then start the game
        if (GameManager.manager.isTimedModePlayed)
        {
            GameManager.manager.TimedMode();
        }
        //if this mode is never played then show description 
        else {
            titleText.text = "Timed Mode";
            descriptionText.text = kTimedDescription;
            image.GetComponent<Image>().sprite = TimedModeImage;
            buttonText.text = kStart;
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
            titleText.text = "Endless Mode";
            descriptionText.text = kEndlessDescription;
            image.GetComponent<Image>().sprite = EndlessModeImage;

            buttonText.text = kStart;
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
            titleText.text = "Zen Mode";
            descriptionText.text = kZenDescription;
            image.GetComponent<Image>().sprite = ZenModeImage;
            buttonText.text = kStart;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => GameManager.manager.ZenMode());

            ScreenManager.screenManager.OpenScreen(GameModeCanvas);
        }
    }
}
