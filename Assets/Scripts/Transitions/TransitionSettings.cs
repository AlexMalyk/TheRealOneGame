using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransitionSettings : MonoBehaviour
{

    public Button button;

    public static bool isSettingsOpen;

    public GameObject settings;
    public GameObject mainMenu;
    public GameObject gameMenu;
    public GameObject game;

    private Animator settingsAnimator;
    private Animator mainMenuAnimator;
    private Animator gameMenuAnimator;
    private Animator gameAnimator;

    // Use this for initialization
    void Start()
    {
        if (settings.GetComponent<Animator>())
        {
            settingsAnimator = settings.GetComponent<Animator>();
        }
        if (mainMenu.GetComponent<Animator>())
        {
            mainMenuAnimator = mainMenu.GetComponent<Animator>();
        }
        if (gameMenu.GetComponent<Animator>())
        {
            gameMenuAnimator = gameMenu.GetComponent<Animator>();
        }
        if (game.GetComponent<Animator>())
        {
            gameAnimator = game.GetComponent<Animator>();
        }

        button.onClick.AddListener(() => CustomClick());
        isSettingsOpen = false;
    }

    public void CustomClick()
    {
        if (!ScreenManager.screenManager.isTransition)
        {
            if (ScreenManager.screenManager.isMenuOpen)
            {
                if (isSettingsOpen)
                {
                    Debug.Log("if if");
                    StartCoroutine(TransitionThreeAnimators.MyCoroutine(settingsAnimator, 1, gameMenuAnimator, 0, gameAnimator));
                }
                else
                {
                    Debug.Log("if else");
                    StartCoroutine(TransitionThreeAnimators.MyCoroutine(gameAnimator, 0, gameMenuAnimator, 1, settingsAnimator));
                }
            }
            else
            {

                if (isSettingsOpen)
                {
                    Debug.Log("else if");
                    StartCoroutine(TransitionTwoAnimators.MyCoroutine(settingsAnimator, mainMenuAnimator));
                }
                else
                {
                    Debug.Log("else else");
                    StartCoroutine(TransitionTwoAnimators.MyCoroutine(mainMenuAnimator, settingsAnimator));
                }
            }
            isSettingsOpen = !isSettingsOpen;
        }
    }
}
