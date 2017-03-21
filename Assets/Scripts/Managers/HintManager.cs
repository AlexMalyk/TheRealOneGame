using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HintManager : MonoBehaviour {
    public static HintManager manager;

    int baseSparkleSpeed;
    int doubleSparkleSpeed;
    
    int newX, newZ;

    public GameObject gameScreen;
    public GameObject iapScreen;
    public GameObject powerUp1;
    public GameObject powerUp2;
    public GameObject powerUp3;

    public Button pu1Button;
    public GameObject pu1ButtonDisabledImage;
    public Button pu2Button;
    public GameObject pu2ButtonDisabledImage;
    public Button pu3Button;
    public GameObject pu3ButtonDisabledImage;

    public GameObject sphere;
    public GameObject leftSide;
    public GameObject rightSide;

    public Text hintMessage;

    [HideInInspector]
    public static bool isPU1UsedInMatch;
    [HideInInspector]
    public static bool isPU2UsedInRound;
    [HideInInspector]
    public static bool isPU3UsedInRound;

    static string kOncePerMatchMessage = "Can only be used once a match";
    static string kOncePerRoundMessage = "Already used";
    static string kTimeStopDescription = "Time stopped for 5 seconds";
    static string kFlashesDescription = "Increases flashing";
    static string kFlankerDescription = "Shows the side to which the dot is closer";

    int priceTimeStops = 2000;
    int priceFlankers = 1000;
    int priceFlashes = 500;


    public  int amountPU1;

    public  int amountPU2;

    public  int amountPU3;

    private Animator sphereAnimator;
    private Animator leftSideAnimator;
    private Animator rightSideAnimator;
    private Animator PU1animator;
    private Animator PU2animator;
    private Animator PU3animator;

    void Awake()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if (manager != null)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    { 
        sphereAnimator = sphere.GetComponent<Animator>();
        leftSideAnimator = leftSide.GetComponent<Animator>();
        rightSideAnimator = rightSide.GetComponent<Animator>();

        PU1animator = powerUp1.GetComponent<Animator>();
        PU2animator = powerUp2.GetComponent<Animator>();
        PU3animator = powerUp3.GetComponent<Animator>();

        isPU1UsedInMatch = false;
        isPU2UsedInRound = false;
        isPU3UsedInRound = false;

    }
    //добавление времени (можно использовать раз в игру)
    public void AddTime()
    {
        //проверка что еще не использовалась в матче => вывод сообщения
        if (!ScreenManager.screenManager.isMenuOpen && isPU1UsedInMatch) {
            ShowMessage(kOncePerMatchMessage);
        }
        //проверка количества и что меню не открыто => добавление времени
        else if (!ScreenManager.screenManager.isMenuOpen && amountPU1 > 0)
        {
            Debug.Log("AddTime");
            GameManager.manager.time += 5;
            amountPU1--;
            pu1ButtonDisabledImage.SetActive(true);//добавление штриховки
            isPU1UsedInMatch = true; //флаг что использована в этом матче

            ShowMessage(kTimeStopDescription); 
        }
        //проверка количества и что меню не открыто и количество 0 => открытие окна поверапа
        else if ( (!ScreenManager.screenManager.isMenuOpen && amountPU1 ==0) || ScreenManager.screenManager.GetOpenScreen() != powerUp1)
        {
            GameManager.manager.PauseGame(true);
            ScreenManager.screenManager.isMenuOpen = true;
            gameScreen.GetComponent<Animator>().SetTrigger("HideUp");
            ScreenManager.screenManager.WithoutAdditionalAnimator();
            ScreenManager.screenManager.OpenScreen(powerUp1);
        }
    }
    //сверкание точки (можно использовать раз в раунд)
    public void IncreaseSparkleSpeed() {
        //проверка что еще не использовалась в раунде
        if (!ScreenManager.screenManager.isMenuOpen && isPU2UsedInRound)
        {
            ShowMessage(kOncePerRoundMessage);
        }
        else if (!ScreenManager.screenManager.isMenuOpen && amountPU2 > 0)
        {
            Debug.Log("IncreaseSparkleSpeed");
            sphereAnimator.SetBool("HintOn", true);

            amountPU2--;
            pu2ButtonDisabledImage.SetActive(true);//добавление штриховки
            isPU2UsedInRound = true; //флаг что использована в этом раунде

            ShowMessage(kFlashesDescription);
        }
        else if ( (!ScreenManager.screenManager.isMenuOpen && amountPU2 == 0) || ScreenManager.screenManager.GetOpenScreen() != powerUp2)
        {
            GameManager.manager.PauseGame(true);
            gameScreen.GetComponent<Animator>().SetTrigger("HideUp");
            ScreenManager.screenManager.WithoutAdditionalAnimator();
            ScreenManager.screenManager.OpenScreen(powerUp2);
        }
    }
    //определение позиции (можно использовать раз в раунд)
    public void ShowSide() {
        //проверка что еще не использовалась в раунде
        if (!ScreenManager.screenManager.isMenuOpen && isPU3UsedInRound)
        {
            ShowMessage(kOncePerRoundMessage);
        }
        else if (!ScreenManager.screenManager.isMenuOpen && amountPU3 > 0)
        {
            Debug.Log("ShowSide");
            Debug.Log("SpherePosition = "+ sphere.GetComponent<RectTransform>().localPosition.x);
            if (sphere.GetComponent<RectTransform>().localPosition.x <= 0)
            {
                leftSideAnimator.SetBool("HintOn", true);
            }

            if (sphere.GetComponent<RectTransform>().localPosition.x >= 0){
                rightSideAnimator.SetBool("HintOn", true);
            }

            amountPU3--;
            pu3ButtonDisabledImage.SetActive(true);//добавление штриховки
            isPU3UsedInRound = true; //флаг что использована в этом раунде

            ShowMessage(kFlankerDescription);
        }
        else if ( (!ScreenManager.screenManager.isMenuOpen && amountPU3 == 0) || ScreenManager.screenManager.GetOpenScreen() != powerUp3)
        {
            GameManager.manager.PauseGame(true);
            gameScreen.GetComponent<Animator>().SetTrigger("HideUp");
            ScreenManager.screenManager.WithoutAdditionalAnimator();
            ScreenManager.screenManager.OpenScreen(powerUp3);
        }
    }

    public void DeleteEffects(bool first, bool second, bool third) {
        if (first) {
            pu1ButtonDisabledImage.SetActive(false);
            isPU1UsedInMatch = false;
        }
        if (second) {
            pu2ButtonDisabledImage.SetActive(false);
            isPU2UsedInRound = false;
        }
        if (third) {
            pu3ButtonDisabledImage.SetActive(false);
            isPU3UsedInRound = false;

            RemoveFlankers();
        }
    }
    public void DeleteEffects()
    {
        pu1ButtonDisabledImage.SetActive(false);
        isPU1UsedInMatch = false;

        pu2ButtonDisabledImage.SetActive(false);
        isPU2UsedInRound = false;
        

        pu3ButtonDisabledImage.SetActive(false);
        isPU3UsedInRound = false;

        RemoveFlankers();   
    }

    public void RemoveFlankers() {
        leftSideAnimator.SetBool("HintOn", false);
        rightSideAnimator.SetBool("HintOn", false);
    }

    public void HideFlankers(bool value) {
        leftSide.GetComponent<Image>().enabled = !value;
        rightSide.GetComponent<Image>().enabled = !value;
    }

    public void BuyTimeStops() {
        if (BankManager.bank >= priceTimeStops)
        {
            amountPU1 += 5;
            DataControl.control.SavePU();
        }
        else {
            iapScreen.GetComponent<Animator>().SetBool("Open", true);
        }
    }

    public void BuyFlashes()
    {
        if (BankManager.bank >= priceFlashes)
        {
            amountPU2 += 5;
            DataControl.control.SavePU();
        }
        else
        {
            iapScreen.GetComponent<Animator>().SetBool("Open", true);
        }
    }

    public void BuyFlankers()
    {
        if (BankManager.bank >= priceFlankers)
        {
            amountPU3 += 5;
            DataControl.control.SavePU();
        }
        else
        {
            iapScreen.GetComponent<Animator>().SetBool("Open", true);
        }
    }

    void ShowMessage(string message) {
        hintMessage.GetComponent<Animator>().SetTrigger("Hide");
        hintMessage.text = message;
        hintMessage.GetComponent<Animator>().SetTrigger("Show");
    }
}
