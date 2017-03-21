using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MissInput : MonoBehaviour {

    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    public static int lifesCount = 3;
    private Animator anima;

    void Start() {

    }
    public void OnMouseDown() {
        //if (lifesCount == 3)
        //{
        //    Debug.Log("2 lifes left");
        //    anima = life1.GetComponent<Animator>();
        //    anima.SetTrigger("Play");
        //    lifesCount--;
        //}
        //else if (lifesCount == 2)
        //{
        //    Debug.Log("1 lifes left");
        //    anima = life2.GetComponent<Animator>();
        //    anima.SetTrigger("Play");
        //    lifesCount--;
        //}
        //else if (lifesCount == 1)
        //{
        //    Debug.Log("0 lifes left");
        //    anima = life3.GetComponent<Animator>();
        //    anima.SetTrigger("Play");
        //    lifesCount--;
        //    Debug.Log("game over");
        //}
        //else {
        //    Debug.Log("game over");
        //}
        if (SettingsManager.manager.isVibrationOn)
        {
            Handheld.Vibrate();
        }
        Debug.Log("Miss");

    }
}
