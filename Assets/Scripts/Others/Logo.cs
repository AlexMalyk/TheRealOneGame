using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Logo : MonoBehaviour {

    public GameObject introScreen;
    public GameObject mainMenu;
    public GameObject buttons;
    
    float time;
    float timeLimit = 2f;
    bool onFinish;

	// Use this for initialization
	void Start () {
        time = 0;
        onFinish = false;

        DataControl.control.LoadAll();
    }
	
	// Update is called once per frame
	void Update () {
        if (time < timeLimit)
        {
            time += Time.deltaTime;
        }
        else if(!onFinish) {
            StartCoroutine(LogoCoroutine());
        }
	}

    public void ResetTimer() {
        time = 0;
    }

    IEnumerator LogoCoroutine() {
        onFinish = true;
        buttons.SetActive(false);
        GetComponent<Animator>().SetTrigger("Center");
        GetComponent<Animator>().SetTrigger("ShowText");
        yield return CoroutineUtil.WaitForRealSeconds(2f);
        GetComponent<Animator>().SetTrigger("Hide");
        yield return CoroutineUtil.WaitForRealSeconds(2f);
        this.GetComponent<Canvas>().enabled = false;
        
        if (DataControl.control.isTutorialFinished)
        {
            mainMenu.SetActive(true);        
        }
        else {
            introScreen.SetActive(true);
        }
        DataControl.control.SaveAll();
        this.gameObject.SetActive(false);
    }
}
