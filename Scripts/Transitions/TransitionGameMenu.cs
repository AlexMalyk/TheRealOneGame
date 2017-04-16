﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransitionGameMenu : MonoBehaviour {

    public Button button;

    public GameObject Menu;
    public GameObject Board;

    private Animator menuAnimator;
    private Animator boardAnimator;

    public void Constructor(GameObject menu, GameObject board) {
        Menu = menu;
        Board = board;
    }
    void Start()
    {
        ScreenManager.screenManager.isMenuOpen = false;
        button.onClick.AddListener(() => Transition());
    }

    public void Transition()
    {
        AudioManager.manager.PlayClickSound();

        menuAnimator = Menu.GetComponent<Animator>();
        boardAnimator = Board.GetComponent<Animator>();

        Menu.GetComponent<Canvas>().enabled = true;

        Debug.Log("Menu transition");

        boardAnimator.SetBool("Open", !boardAnimator.GetBool("Open"));
        menuAnimator.SetBool("Open", !menuAnimator.GetBool("Open"));
        
        if (ScreenManager.screenManager.isMenuOpen)
        {
            Debug.Log("Menu transition if");
            ScreenManager.screenManager.SetPrevious(Menu);
            ScreenManager.screenManager.SetOpen(Board);
            GameManager.manager.PauseGame(false);

            HintManager.manager.HideFlankers(false);
        }
        else {
            Debug.Log("Menu transition les");
            ScreenManager.screenManager.SetPrevious(Board);
            ScreenManager.screenManager.SetOpen(Menu);
            GameManager.manager.PauseGame(true);

            HintManager.manager.HideFlankers(true);
        }
        ScreenManager.screenManager.isMenuOpen = !ScreenManager.screenManager.isMenuOpen;
    }
}
