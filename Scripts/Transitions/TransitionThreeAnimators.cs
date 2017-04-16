using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransitionThreeAnimators : MonoBehaviour {

    public Button button;

    public GameObject firstGameObject;
    public float firstInterval;
    public GameObject secondGameObject;
    public float secondInterval;
    public GameObject thirdGameObject;

    private Animator firstAnimator;
    private Animator secondAnimator;
    private Animator thirdAnimator;

    void Start()
    {
        firstAnimator = firstGameObject.GetComponent<Animator>();
        secondAnimator = secondGameObject.GetComponent<Animator>();
        thirdAnimator = thirdGameObject.GetComponent<Animator>();
        
        button.onClick.AddListener(() => CustomClick());
    }

    public void CustomClick()
    {
        StartCoroutine(MyCoroutine(firstAnimator, firstInterval, secondAnimator, secondInterval, thirdAnimator));
    }

    public static IEnumerator MyCoroutine(Animator first, float interval1, Animator second, float interval2, Animator third)
    {
        ScreenManager.screenManager.isTransition = true;

        first.SetBool("Open", false);
        //if (!second.gameObject.GetComponent<Canvas>().isActiveAndEnabled)
        //{
        //    second.gameObject.GetComponent<Canvas>().enabled = !second.gameObject.GetComponent<Canvas>().enabled;
        //}
        //yield return CoroutineUtil.WaitForRealSeconds(interval1);
        //second.SetBool("Open", !second.GetBool("Open"));

        yield return CoroutineUtil.WaitForRealSeconds(interval1);

        if (second.gameObject.GetComponent<Canvas>().isActiveAndEnabled)
        { 
            second.SetBool("Open", false);
            yield return CoroutineUtil.WaitForRealSeconds(interval2);
            second.gameObject.GetComponent<Canvas>().enabled = false;
        }
        else {
            second.gameObject.GetComponent<Canvas>().enabled = true;
            yield return CoroutineUtil.WaitForRealSeconds(interval2);
            second.SetBool("Open", true);
        }



        if (third.gameObject.GetComponent<Canvas>())
            third.gameObject.GetComponent<Canvas>().enabled = true;
        //yield return CoroutineUtil.WaitForRealSeconds(interval2);

        third.SetBool("Open", true);

        if (first.gameObject.GetComponent<Canvas>())
            first.gameObject.GetComponent<Canvas>().enabled = false;

        ScreenManager.screenManager.isTransition = false;
    }
}
