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
        if (StartScreen.GetComponent<Animator>())
            StartScreenAnimator = StartScreen.GetComponent<Animator>();
        OpenScreenAnimator = StartScreenAnimator;

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
                Debug.Log("AdditionalScreenAnimator = none");
                StartCoroutine(TransitionTwoAnimators.MyCoroutine(OpenScreenAnimator, animator));
            }
            else
            {
                if (AdditionalScreenAnimator.gameObject.GetComponent<Canvas>().isActiveAndEnabled)
                {
                    Debug.Log("AdditionalScreenAnimator = active");
                    StartCoroutine(TransitionThreeAnimators.MyCoroutine(OpenScreenAnimator, 0, AdditionalScreenAnimator, 1, animator));
                }
                else
                {
                    Debug.Log("AdditionalScreenAnimator = not active");
                    StartCoroutine(TransitionThreeAnimators.MyCoroutine(OpenScreenAnimator, 1, AdditionalScreenAnimator, 0, animator));
                }
            }
            //Set the new Screen as then open one.
            PreviousScreenAnimator = OpenScreenAnimator;
            OpenScreenAnimator = animator;

            Debug.Log("PreviousScreenAnimator = " + PreviousScreenAnimator.gameObject.name);
            Debug.Log("OpenScreenAnimator = " + OpenScreenAnimator.gameObject.name);
        }
    }

    public void BackToPreviousScreen()
    {
        Debug.Log("back to rpevious");
        OpenScreen(PreviousScreenAnimator.gameObject);
    }

    public void SetAdditionalAnimator(GameObject obj)
    {
        AdditionalScreenAnimator = obj.GetComponent<Animator>();
        Debug.Log("AdditionalScreenAnimator = " + AdditionalScreenAnimator.gameObject.name);
    }
    public void WithoutAdditionalAnimator()
    {
        Debug.Log("AdditionalScreenAnimator = none");
        AdditionalScreenAnimator = null;
    }
    public void SetPrevious(GameObject obj)
    {
        Debug.Log("PreviousScreenAnimator = " + obj.GetComponent<Animator>().gameObject.name);
        PreviousScreenAnimator = obj.GetComponent<Animator>();
    }
    public void SetOpen(GameObject obj)
    {
        Debug.Log("OpenScreenAnimator = " + obj.GetComponent<Animator>().gameObject.name);
        OpenScreenAnimator = obj.GetComponent<Animator>();
    }
    public GameObject GetOpenScreen() {
        Debug.Log("GetOpenScreen = " + OpenScreenAnimator.gameObject.name);
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
