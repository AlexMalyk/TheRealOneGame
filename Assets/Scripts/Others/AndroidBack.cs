﻿using UnityEngine;
using System.Collections;

public class AndroidBack: MonoBehaviour {

    public GameObject mainMenu;
    public GameObject gameBoard;
    public GameObject gameMenu;
    public GameObject endGame;
    public GameObject bankCanvas;
    public GameObject achievementScreen;
    public GameObject pu1Screen;
    public GameObject pu2Screen;
    public GameObject pu3Screen;
    public GameObject gameScreen;

    GameObject openedScreen;
    TransitionGameMenu transition;

    void Start() {
        transition = new TransitionGameMenu();
        transition.Constructor(gameMenu, gameBoard);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            openedScreen = ScreenManager.screenManager.GetOpenScreen();
            if (openedScreen == mainMenu)
            {
                Application.Quit();
            }
            else if (bankCanvas.GetComponent<Animator>().GetBool("Open")) {
                bankCanvas.GetComponent<Animator>().SetBool("Open", false);
            }
            else if (openedScreen == gameBoard || openedScreen == gameMenu)
            {
                transition.Transition();
            }
            else if (openedScreen == endGame || openedScreen == achievementScreen)
            {
                ScreenManager.screenManager.WithoutAdditionalAnimator();
                ScreenManager.screenManager.OpenScreen(mainMenu);
            }
            else if (openedScreen == pu1Screen || openedScreen == pu2Screen || openedScreen == pu3Screen) {
                ScreenManager.screenManager.OpenScreen(gameBoard);
                gameScreen.GetComponent<Animator>().SetTrigger("ShowUp");
                ScreenManager.screenManager.SetAdditionalAnimator(gameScreen);
                GameManager.manager.PauseGame(false);
                ScreenManager.screenManager.SetMenu(false);
            }
            else {
                ScreenManager.screenManager.BackToPreviousScreen();
            }
        }     
    }
}
