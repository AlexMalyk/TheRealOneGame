using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransitionGameMenu : MonoBehaviour {

    public Button button;

    public GameObject gameMenu;
    public GameObject gameBoard;

    private Animator menuAnimator;
    private Animator boardAnimator;

    public void Constructor(GameObject menu, GameObject board) {
        gameMenu = menu;
        gameBoard = board;
    }
    void Start()
    {
        ScreenManager.screenManager.isMenuOpen = false;
        button.onClick.AddListener(() => Transition());
    }

    public void Transition()
    {
        if (ScreenManager.screenManager.isTransition)
        {
            return;
        }
        AudioManager.manager.PlayClickSound();

        menuAnimator = gameMenu.GetComponent<Animator>();
        boardAnimator = gameBoard.GetComponent<Animator>();

        gameMenu.GetComponent<Canvas>().enabled = true;

        boardAnimator.SetBool("Open", !boardAnimator.GetBool("Open"));
        menuAnimator.SetBool("Open", !menuAnimator.GetBool("Open"));
        
        if (ScreenManager.screenManager.isMenuOpen)
        {
            ScreenManager.screenManager.SetPrevious(gameMenu);
            ScreenManager.screenManager.SetOpen(gameBoard);
            GameManager.manager.PauseGame(false);

            PowerUpsManager.manager.HideWings(false);
        }
        else {
            ScreenManager.screenManager.SetPrevious(gameBoard);
            ScreenManager.screenManager.SetOpen(gameMenu);
            GameManager.manager.PauseGame(true);

            PowerUpsManager.manager.HideWings(true);
        }
        ScreenManager.screenManager.isMenuOpen = !ScreenManager.screenManager.isMenuOpen;
    }

}
