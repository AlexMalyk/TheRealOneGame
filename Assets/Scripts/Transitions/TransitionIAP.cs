using UnityEngine;
using System.Collections;

public class TransitionIAP : MonoBehaviour {

    private Animator IAPCanvasAnimator;

    // Use this for initialization
    void Start () {
        IAPCanvasAnimator = GetComponent<Animator>();
	}

    public void OpenCanvas() {
        if (ScreenManager.screenManager.isTransition == false)
        {
            IAPCanvasAnimator.SetBool("Open", true);
        }
    }
    public void CloseCanvas()
    {
        IAPCanvasAnimator.SetBool("Open", false);
    }
}
