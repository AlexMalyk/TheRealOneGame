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
    public void Miss() {
        gameBoardAnimator.SetTrigger("Miss");
        AudioManager.manager.PlayMissedSound();
	
        
        if (GameManager.manager.mode == Mode.Zen)
        {
            if (lifesCount == 3)
            {
                anima = life1.GetComponent<Animator>();
                anima.SetTrigger("Hide");
                lifesCount--;
            }
            else if (lifesCount == 2)
            {
                anima = life2.GetComponent<Animator>();
                anima.SetTrigger("Hide");
                lifesCount--;
            }
            else if (lifesCount == 1)
            {
                anima = life3.GetComponent<Animator>();
                anima.SetTrigger("Hide");
                lifesCount--;

                GameManager.manager.EndGame();
            }
        }
    }
}
