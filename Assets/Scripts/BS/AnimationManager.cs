using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimationManager : MonoBehaviour {
    public static AnimationManager AM;

    public Canvas MenuCanvas;
    private Animator MenuCanvasAnimator;

    public Canvas SettingsCanvas;
    private Animator SettingsCanvasAnimator;

    public Canvas PowerUpsCanvas;
    private Animator PowerUpsCanvasAnimator;

    public GameObject GameBoard;
    private Animator GameBoardAnimator;

    public Canvas GameCanvas;
    private Animator GameCanvasAnimator;

    public Canvas EndGame;
    private Animator EndGameCanvasAnimator;

    public GameObject PU1;
    public GameObject PU2;
    public GameObject PU3;
    int lastOpenedPU;

    public static bool isGameMenuOpen;

    void Awake()
    {
        if (AM == null)
        {
            DontDestroyOnLoad(gameObject);
            AM = this;
        }
        else if (AM != null)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        MenuCanvasAnimator = MenuCanvas.GetComponent<Animator>();
        SettingsCanvasAnimator = SettingsCanvas.GetComponent<Animator>();
        PowerUpsCanvasAnimator = PowerUpsCanvas.GetComponent<Animator>();

        GameCanvasAnimator = GameCanvas.GetComponent<Animator>();
        GameBoardAnimator = GameBoard.GetComponent<Animator>();
        EndGameCanvasAnimator = EndGame.GetComponent<Animator>();

       

        isGameMenuOpen = false;
    }
    //public void StartGame()
    //{
    //    Debug.Log("AM startGame");
    //    MenuCanvasAnimator.SetTrigger("Animation");
    //    GameBoardAnimator.SetTrigger("Animation");
    //    GameCanvasAnimator.SetTrigger("Start");
    //}
    //public void RestartGame()
    //{
    //    Debug.Log("AM restartGame");
    //    GameBoardAnimator.SetTrigger("Animation");
    //    GameCanvasAnimator.SetTrigger("Start");
    //    GameMenuAnimation();
    //}
    //public void ExitToMainMenu()
    //{
    //    Debug.Log("AM exitToMain");
    //    GameCanvasAnimator.SetTrigger("ExitToMainMenu");
    //    MenuCanvasAnimator.SetTrigger("Animation");
    //}
    //public void GameMenuAnimation()
    //{      
    //    isGameMenuOpen = !isGameMenuOpen;
    //    GameBoardAnimator.SetTrigger("Animation");
    //    GameCanvasAnimator.SetTrigger("MenuAnimation");
    //    if (GameManager.gameIsRunning == true)
    //    {
    //        Debug.Log("AM gameMenu if");
    //        GameManager.gameIsRunning = false;
    //    }

    //    else
    //    {
    //        Debug.Log("AM gameMenu else");
    //        GameManager.gameIsRunning = true;
    //    }          
    //}

    //public void SettingsAnimation() {
    //    if (isGameMenuOpen)
    //    {
    //        Debug.Log("AM settings if");
    //        SettingsCanvasAnimator.SetTrigger("SettingsAnimation");
    //        GameCanvasAnimator.SetTrigger("Settings");
    //    }
    //    else {
    //        Debug.Log("AM settings else");
    //        SettingsCanvasAnimator.SetTrigger("SettingsAnimation");
    //        MenuCanvasAnimator.SetTrigger("Animation");
    //    }
    //}

    //public void PowerUpOpen()
    //{
    //    MenuCanvasAnimator.SetTrigger("Animation");
    //    PowerUpsCanvasAnimator.SetTrigger("Animation");
    //}
    //public void PowerUpClose()
    //{
    //    MenuCanvasAnimator.SetTrigger("Animation");
    //    PowerUpsCanvasAnimator.SetTrigger("Animation");
    //}

    //public void EndGameOpen() {
    //    Debug.Log("AM endgameopen");
    //    GameBoardAnimator.SetTrigger("Animation");
    //    GameCanvasAnimator.SetTrigger("EndGame");
    //    EndGameCanvasAnimator.SetTrigger("Animation");
    //}
    //public void EndGameClose()
    //{
    //    Debug.Log("AM EndGameClose");
    //    GameCanvasAnimator.SetTrigger("ExitToMainMenu");
    //    MenuCanvasAnimator.SetTrigger("Animation");
    //    EndGameCanvasAnimator.SetTrigger("Animation");
    //}
    //public void EndGamePlayAgain()
    //{
    //    Debug.Log("AM EndGamePlayAgain");
    //    GameBoardAnimator.SetTrigger("Animation");
    //    GameCanvasAnimator.SetTrigger("Play Again");
    //    EndGameCanvasAnimator.SetTrigger("Animation");
    //}


    //public void PowerUp1()
    //{
    //    GameCanvasAnimator.SetTrigger("ClosePU");
    //    GameCanvasAnimator.SetTrigger("PU1");
    //}

    //public void PowerUp2()
    //{
    //    GameCanvasAnimator.SetTrigger("ClosePU");
    //    GameCanvasAnimator.SetTrigger("PU2");
    //}

    //public void PowerUpDone()
    //{
    //    GameCanvasAnimator.SetTrigger("ClosePU");
    //    GameCanvasAnimator.SetTrigger("MenuAnimation");
    //}

    //public void PowerUpOpen(int PUnumber) {
    //    Debug.Log("PowerUpOpen start");
    //    if (isGameMenuOpen) {
    //        Debug.Log("PowerUpOpen if");


    //        if (lastOpenedPU == PUnumber) {
    //            Debug.Log("PowerUpOpen if if");

    //            GameCanvasAnimator.SetTrigger("ToMenu");
    //            lastOpenedPU = 0;
    //        }
    //        else {
    //            Debug.Log("PowerUpOpen if else");
    //            //PU1.SetActive(false);
    //            //PU2.SetActive(false);
    //            //PU3.SetActive(false);
    //            //GameCanvasAnimator.SetTrigger("OpenPU");
    //            //StartCoroutine(PUanimation(PUnumber));
    //            //if (PUnumber == 1)
    //            //{
    //            //    Debug.Log("PowerUpOpen if else if1");
    //            //    PU1.SetActive(true);
    //            //}
    //            //else if (PUnumber == 2)
    //            //{
    //            //    Debug.Log("PowerUpOpen if else if2");
    //            //    PU2.SetActive(true);
    //            //}
    //            //else if (PUnumber == 3)
    //            //{
    //            //    Debug.Log("PowerUpOpen if else if3");
    //            //    PU3.SetActive(true);
    //            //}
    //            //GameCanvasAnimator.SetTrigger("ClosePU");
    //            //lastOpenedPU = PUnumber;
    //            StartCoroutine(PUanimation(PUnumber));
    //        } 
    //    }
    //    Debug.Log("PowerUpOpen exit");
    //}
    //public void PowerUpClose(int PUnumber)
    //{
    //    GameCanvasAnimator.SetTrigger("ToMenu");
    //}

    //IEnumerator PUanimation(int PUnumber)
    //{
    //    //PU1.SetActive(false);
    //   // PU2.SetActive(false);
    //   // PU3.SetActive(false);
    //    yield return CoroutineUtil.WaitForRealSeconds(1f);


    //    GameCanvasAnimator.SetTrigger("OpenPU");
    //    yield return CoroutineUtil.WaitForRealSeconds(1f);


    //    GameCanvasAnimator.SetTrigger("ClosePU");
    //    lastOpenedPU = PUnumber;

    //}






    //--------------------------------------------------------

    public void StartGame()
    {
        Debug.Log("AM startGame");
        StartCoroutine(MyCoroutine(MenuCanvasAnimator, 1f, GameCanvasAnimator, 1f, GameBoardAnimator));
    }


    private Animator animOut;
    private Animator animIn;

    private GameObject objOut;
    private GameObject objIn;


    public void SetOutAnimator(GameObject anim) {
        animOut = anim.GetComponent<Animator>();
        objOut = anim;
    }
    public void SetInAnimator(GameObject anim)
    {
        animIn = anim.GetComponent<Animator>();
        objIn = anim;
    }

    public void AnimationCoroutine() {
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        animOut.SetBool("Open", !animOut.GetBool("Open"));
        yield return CoroutineUtil.WaitForRealSeconds(1f);
        animIn.SetBool("Open", !animIn.GetBool("Open"));
    }
    IEnumerator MyCoroutine(Animator first, float firstInterval, Animator second, float secondInterval, Animator third) {
        first.SetTrigger("Close");
        yield return CoroutineUtil.WaitForRealSeconds(firstInterval);
        second.SetTrigger("Open");
        yield return CoroutineUtil.WaitForRealSeconds(secondInterval);
        third.SetTrigger("Open");
    }
}
