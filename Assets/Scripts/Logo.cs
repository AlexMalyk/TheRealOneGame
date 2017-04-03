using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Logo : MonoBehaviour {

    public GameObject mainMenu;

    float time;
    float timeLimit = 5f;
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
            this.GetComponent<Canvas>().enabled = false;
            mainMenu.SetActive(true);
        }
	}

    public void ResetTimer() {
        time = 0;
    }
}
