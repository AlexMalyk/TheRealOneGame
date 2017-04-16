using UnityEngine;
using System.Collections;

public class TransitionIAP : MonoBehaviour {

    private Animator IAPCanvasAnimator;

    // Use this for initialization
    void Start () {
        IAPCanvasAnimator = GetComponent<Animator>();
	}

    public void OpenCanvas() {
        //GetComponent<Canvas>().enabled = true;
        IAPCanvasAnimator.SetBool("Open", true);
    }
    public void CloseCanvas()
    {
        IAPCanvasAnimator.SetBool("Open", false);
        //GetComponent<Canvas>().enabled = true;
    }
}
