using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager screenManager;
    //Screen to open automatically at the start of the Scene
    public GameObject StartScreen;
    public GameObject introScreen;

    //Currently Open Screen
    private GameObject m_Open;
    //Previous Screen
    private GameObject m_PreviousOpen;

    private GameObject m_AdditionalScreen;

    private Animator StartScreenAnimator;
    private Animator OpenScreenAnimator;
    private Animator PreviousScreenAnimator;
    private Animator AdditionalScreenAnimator;

    //Animator State and Transition names we need to check against.
    const string k_OpenTransitionName = "Open";
    const string k_ClosedStateName = "Closed";

    public bool isMenuOpen;
    public bool isTransition;

    void Awake()
    {
        if (screenManager == null)
        {
            DontDestroyOnLoad(gameObject);
            screenManager = this;
        }
        else if (screenManager != null)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        DataControl.control.LoadAll();
        if (DataControl.control.isTutorialFinished)
        {

            if (StartScreen.GetComponent<Animator>())
                StartScreenAnimator = StartScreen.GetComponent<Animator>();
            OpenScreenAnimator = StartScreenAnimator;
            StartScreen.SetActive(true);
        }
        else {
            introScreen.SetActive(true);
        }
        isTransition = false;
    }

    //Closes the currently open panel and opens the provided one.
    //It also takes care of handling the navigation, setting the new Selected element.
    public void OpenScreen(GameObject obj)
    {
        if (!isTransition)
        {    
            Animator animator = obj.GetComponent<Animator>();
            if (AdditionalScreenAnimator == null)
            {
                StartCoroutine(TransitionTwoAnimators.MyCoroutine(OpenScreenAnimator, animator));
            }
            else
            {
                if (AdditionalScreenAnimator.gameObject.GetComponent<Canvas>().isActiveAndEnabled)
                {
                    StartCoroutine(TransitionThreeAnimators.MyCoroutine(OpenScreenAnimator, 0, AdditionalScreenAnimator, 1, animator));
                }
                else
                {
                    StartCoroutine(TransitionThreeAnimators.MyCoroutine(OpenScreenAnimator, 1, AdditionalScreenAnimator, 0, animator));
                }
            }
            PreviousScreenAnimator = OpenScreenAnimator;
            OpenScreenAnimator = animator;
        }
    }

    public void BackToPreviousScreen()
    {
        OpenScreen(PreviousScreenAnimator.gameObject);
    }

    public void SetAdditionalAnimator(GameObject obj)
    {
        AdditionalScreenAnimator = obj.GetComponent<Animator>();
    }
    public void WithoutAdditionalAnimator()
    {
        AdditionalScreenAnimator = null;
    }
    public void SetPrevious(GameObject obj)
    {
        PreviousScreenAnimator = obj.GetComponent<Animator>();
    }
    public void SetOpen(GameObject obj)
    {
        OpenScreenAnimator = obj.GetComponent<Animator>();
    }
    public GameObject GetOpenScreen() {
        return OpenScreenAnimator.gameObject;
    }
    public void SetMenu(bool value) {
        isMenuOpen = value;
    }

    public void CloseAllOpenedScreens() {
        if (OpenScreenAnimator) {
            OpenScreenAnimator.SetBool("Open", false);
        }
        if (AdditionalScreenAnimator) {
            AdditionalScreenAnimator.SetBool("Open", false);
            AdditionalScreenAnimator.gameObject.GetComponent<Canvas>().enabled = false;
            AdditionalScreenAnimator = null;          
        }
    }
}
