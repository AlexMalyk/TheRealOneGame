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

    bool logoAnimationFinished;

	// Use this for initialization
	void Start () {
        time = 0;
        onFinish = false;
        logoAnimationFinished = false;
       
        StartCoroutine(LoadingCoroutine());
    }
	
	// Update is called once per frame
	void Update () {
        if (time < timeLimit)
        {
            time += Time.deltaTime;
        }
        else if(!onFinish) {
            
            StartCoroutine(LogoAnimationCoroutine());           
        }
	}

    public void ResetTimer() {
        time = 0;
    }

    IEnumerator LogoAnimationCoroutine() {
        Debug.Log("LogoAnimation Coroutine starts");
        onFinish = true;
        buttons.SetActive(false);
        GetComponent<Animator>().SetTrigger("Center");
        GetComponent<Animator>().SetTrigger("ShowText");
        Debug.Log("LogoAnimation Coroutine eye on center");
        yield return CoroutineUtil.WaitForRealSeconds(2f);
        GetComponent<Animator>().SetTrigger("Hide");
        Debug.Log("LogoAnimation Coroutine hide all");
        yield return CoroutineUtil.WaitForRealSeconds(.5f);
        logoAnimationFinished = true;
        Debug.Log("LogoAnimation Coroutine finishes");

        //LocalizationManager.manager.SetLanguage();
        //while (!LocalizationManager.manager.GetIsReady())
        //{
        //    yield return null;
        //}

        //SceneManager.LoadScene("Game");

    }
    IEnumerator LoadingCoroutine() {
        Debug.Log("Loading Coroutine starts");
        LocalizationManager.manager.SetLanguage();
        Debug.Log("Loading Coroutine set language");
        Debug.Log("Loading Coroutine before manager=" + LocalizationManager.manager.GetIsReady()+", animation="+ logoAnimationFinished);
        while (!LocalizationManager.manager.GetIsReady() || !logoAnimationFinished)
        {
            yield return new WaitForSeconds(.05f);
        }
        Debug.Log("Loading Coroutine after  manager=" + LocalizationManager.manager.GetIsReady() + ", animation=" + logoAnimationFinished);
        Debug.Log("Loading Coroutine load scene");
        SceneManager.LoadScene("Game");
    }
}
