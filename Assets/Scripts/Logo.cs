using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Logo : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject buttons;
    public GameObject movingEyePart;

    public GameObject texts;
    
    float time;
    float timeLimit = 2f;

	// Use this for initialization
	void Start () {
        time = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (time < timeLimit)
        {
            time += Time.deltaTime;
        }
        else {
            StartCoroutine(LogoCoroutine());         
        }
	}

    public void ResetTimer() {
        time = 0;
    }

    IEnumerator LogoCoroutine() {
        buttons.SetActive(false);
        movingEyePart.GetComponent<Animator>().SetTrigger("Center");
        texts.GetComponent<Animator>().SetTrigger("Show");
        yield return CoroutineUtil.WaitForRealSeconds(2f);

        this.GetComponent<Canvas>().enabled = false;
        mainMenu.SetActive(true);
    }
}
