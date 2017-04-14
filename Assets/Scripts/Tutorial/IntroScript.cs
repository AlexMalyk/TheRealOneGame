using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour {

    public GameObject mainScreen;

    public Text kFirstUpText;
    public Text kFirstDownText;

    public Text kSecondUpText;
    public Text kSecondDownText;

    public Text kThirdUpText;
    public Text kThirdDowntext;

    void Start() {
        GetComponent<Animator>().SetBool("1", true);
    }

    public void PlaySecond() {
        
    }

}
