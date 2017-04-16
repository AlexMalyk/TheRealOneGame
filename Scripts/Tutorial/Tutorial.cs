using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

    public GameObject MainScreenGameObject;

    Animator mainAnimator;
	// Use this for initialization
	void Start () {
        mainAnimator = MainScreenGameObject.GetComponent<Animator>();
	}

    void FirstStage() {

    }
}
