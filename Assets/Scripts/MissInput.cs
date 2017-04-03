using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MissInput : MonoBehaviour {

    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    public static int lifesCount = 3;
    public GameObject gameBoard;

    private Animator anima;
    private Animator gameBoardAnimator;
    void Start() {
        gameBoardAnimator = gameBoard.GetComponent<Animator>();
    }
    public void OnMouseDown() {
        gameBoardAnimator.SetTrigger("Miss");
        AudioManager.manager.PlayMissedSound();

        if (SettingsManager.manager.isVibrationOn)
        {
            Handheld.Vibrate();
            
        }
        
        if (GameManager.manager.mode == Mode.Zen)
        {
            if (lifesCount == 3)
            {
                Debug.Log("2 lifes left");
                anima = life1.GetComponent<Animator>();
                anima.SetTrigger("Hide");
                lifesCount--;
            }
            else if (lifesCount == 2)
            {
                Debug.Log("1 lifes left");
                anima = life2.GetComponent<Animator>();
                anima.SetTrigger("Hide");
                lifesCount--;
            }
            else if (lifesCount == 1)
            {
                Debug.Log("0 lifes left");
                anima = life3.GetComponent<Animator>();
                anima.SetTrigger("Hide");
                lifesCount--;
                Debug.Log("game over");

                GameManager.manager.EndGame();
            }
            else
            {
                Debug.Log("game over");
            }
        }


        
        Debug.Log("Miss");
    }
}
