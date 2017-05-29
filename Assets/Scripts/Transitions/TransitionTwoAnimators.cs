using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransitionTwoAnimators : MonoBehaviour {

    public Button button;

    public GameObject firstGameObject;
    public GameObject secondGameObject;

    private Animator firstAnimator;
    private Animator secondAnimator;

    void Start () {
        if(firstGameObject.GetComponent<Animator>())
            firstAnimator = firstGameObject.GetComponent<Animator>();
        if(secondGameObject.GetComponent<Animator>())
            secondAnimator = secondGameObject.GetComponent<Animator>();

        button.onClick.AddListener(() => CustomClick());
	}

    public void CustomClick() {
        StartCoroutine(MyCoroutine(firstAnimator, secondAnimator));
    }

    public static IEnumerator MyCoroutine(Animator first, Animator second) {
        ScreenManager.screenManager.isTransition = true;

        first.SetBool("Open", false);
        if (second.gameObject.GetComponent<Canvas>()) { 
            second.gameObject.GetComponent<Canvas>().enabled = true;
        }
        yield return CoroutineUtil.WaitForRealSeconds(1f);

        if (first.gameObject.GetComponent<Canvas>()) { 
            first.gameObject.GetComponent<Canvas>().enabled = false;
        }
        second.SetBool("Open", true);

        ScreenManager.screenManager.isTransition = false;
    }
}
