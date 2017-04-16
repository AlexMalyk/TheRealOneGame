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
    public GameObject timeStopsGameObject;
    public GameObject flashesGameObject;
    public GameObject flankersGameObject;

    public Button timeStopButton;
    public GameObject timeStopButtonDisabledImage;
    public Button flashButton;
    public GameObject flashButtonDisabledImage;
    public Button flankerButton;
    public GameObject flankerButtonDisabledImage;

    public GameObject sphere;
    public GameObject leftSide;
    public GameObject rightSide;

    public Text hintMessage;

    public Text plus5TimeStops;
    public Text plus5Flashes;
    public Text plus5Flankers;

    public Text plus5TimeStopsPUscreen;
    public Text plus5FlashesPUscreen;
    public Text plus5FlankersPUscreen;

    [HideInInspector]
    public static bool isTimeStopUsedInMatch;
    [HideInInspector]
    public static bool isFlashUsedInRound;
    [HideInInspector]
    public static bool isFlankerUsedInRound;

    static string kOncePerMatchMessage = "Can only be used once a match";
    static string kOncePerRoundMessage = "Already used";
    static string kTimeStopDescription = "Time stopped for 5 seconds";
    static string kFlashesDescription = "Increases flashing";
    static string kFlankerDescription = "Shows the side to which the dot is closer";

    int priceTimeStops = 2000;
    int priceFlankers = 500;
    int priceFlashes = 1000;

    [HideInInspector]
    public  int amountTimeStops;
    [HideInInspector]
    public  int amountFlashes;
    [HideInInspector]
    public  int amountFlankers;

    private Animator sphereAnimator;
    private Animator leftSideAnimator;
    private Animator rightSideAnimator;
    private Animator timeStopAnimator;
    private Animator flashAnimator;
    private Animator flankerAnimator;

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

        timeStopAnimator = timeStopsGameObject.GetComponent<Animator>();
        flashAnimator = flashesGameObject.GetComponent<Animator>();
        flankerAnimator = flankersGameObject.GetComponent<Animator>();

        isTimeStopUsedInMatch = false;
        isFlashUsedInRound = false;
        isFlankerUsedInRound = false;

    }
    //добавление времени (можно использовать раз в игру)
    public void TimeStopHint()
    {
        //проверка что еще не использовалась в матче => вывод сообщения
        if (!ScreenManager.screenManager.isMenuOpen && isTimeStopUsedInMatch) {
            ShowMessage(kOncePerMatchMessage);
        }
        //проверка количества и что меню не открыто => добавление времени
        else if (!ScreenManager.screenManager.isMenuOpen && amountTimeStops > 0)
        {
            Debug.Log("AddTime");
            GameManager.manager.time += 5;

            amountTimeStops--;
            DataControl.control.SaveAll();

            timeStopButtonDisabledImage.SetActive(true);//добавление штриховки
            isTimeStopUsedInMatch = true; //флаг что использована в этом матче

            ShowMessage(kTimeStopDescription); 
        }
        //проверка количества и что меню не открыто и количество 0 => открытие окна поверапа
        else if ( (!ScreenManager.screenManager.isMenuOpen && amountTimeStops == 0) || ScreenManager.screenManager.GetOpenScreen() != timeStopsGameObject)
        {
            GameManager.manager.PauseGame(true);
            ScreenManager.screenManager.isMenuOpen = true;
            gameScreen.GetComponent<Animator>().SetTrigger("HideUp");
            ScreenManager.screenManager.WithoutAdditionalAnimator();
            ScreenManager.screenManager.OpenScreen(timeStopsGameObject);
        }
    }
    //сверкание точки (можно использовать раз в раунд)
    public void FlashHint() {
        //проверка что еще не использовалась в раунде
        if (!ScreenManager.screenManager.isMenuOpen && isFlashUsedInRound)
        {
            ShowMessage(kOncePerRoundMessage);
        }
        else if (!ScreenManager.screenManager.isMenuOpen && amountFlashes > 0)
        {
            Debug.Log("IncreaseSparkleSpeed");
            sphereAnimator.SetBool("HintOn", true);

            amountFlashes--;
            DataControl.control.SaveAll();

            flashButtonDisabledImage.SetActive(true);//добавление штриховки
            isFlashUsedInRound = true; //флаг что использована в этом раунде

            ShowMessage(kFlashesDescription);
        }
        else if ( (!ScreenManager.screenManager.isMenuOpen && amountFlashes == 0) || ScreenManager.screenManager.GetOpenScreen() != flashesGameObject)
        {
            GameManager.manager.PauseGame(true);
            gameScreen.GetComponent<Animator>().SetTrigger("HideUp");
            ScreenManager.screenManager.WithoutAdditionalAnimator();
            ScreenManager.screenManager.OpenScreen(flashesGameObject);
        }
    }
    //определение позиции (можно использовать раз в раунд)
    public void FlankerHint() {
        //проверка что еще не использовалась в раунде
        if (!ScreenManager.screenManager.isMenuOpen && isFlankerUsedInRound)
        {
            ShowMessage(kOncePerRoundMessage);
        }
        else if (!ScreenManager.screenManager.isMenuOpen && amountFlankers > 0)
        {
            Debug.Log("ShowSide");
            Debug.Log("SpherePosition = "+ sphere.GetComponent<RectTransform>().localPosition.x);
            if (sphere.transform.parent.GetComponent<RectTransform>().localPosition.x <= 0)
            {
                leftSideAnimator.SetBool("HintOn", true);
            }

            if (sphere.transform.parent.GetComponent<RectTransform>().localPosition.x >= 0){
                rightSideAnimator.SetBool("HintOn", true);
            }

            amountFlankers--;
            DataControl.control.SaveAll();

            flankerButtonDisabledImage.SetActive(true);//добавление штриховки
            isFlankerUsedInRound = true; //флаг что использована в этом раунде

            ShowMessage(kFlankerDescription);
        }
        else if ( (!ScreenManager.screenManager.isMenuOpen && amountFlankers == 0) || ScreenManager.screenManager.GetOpenScreen() != flankersGameObject)
        {
            GameManager.manager.PauseGame(true);
            gameScreen.GetComponent<Animator>().SetTrigger("HideUp");
            ScreenManager.screenManager.WithoutAdditionalAnimator();
            ScreenManager.screenManager.OpenScreen(flankersGameObject);
        }
    }

    public void DeleteEffects(bool first, bool second, bool third) {
        if (first) {
            timeStopButtonDisabledImage.SetActive(false);
            isTimeStopUsedInMatch = false;
        }
        if (second) {
            flashButtonDisabledImage.SetActive(false);
            isFlashUsedInRound = false;
        }
        if (third) {
            flankerButtonDisabledImage.SetActive(false);
            isFlankerUsedInRound = false;

            RemoveFlankers();
        }
    }
    public void DeleteEffects()
    {
        timeStopButtonDisabledImage.SetActive(false);
        isTimeStopUsedInMatch = false;

        flashButtonDisabledImage.SetActive(false);
        isFlashUsedInRound = false;


        flankerButtonDisabledImage.SetActive(false);
        isFlankerUsedInRound = false;

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
            if (ScreenManager.screenManager.GetOpenScreen() == timeStopsGameObject)
            {
                plus5TimeStopsPUscreen.GetComponent<Animator>().SetTrigger("Show");
            }
            else { 
                plus5TimeStops.GetComponent<Animator>().SetTrigger("Show");
            }
            amountTimeStops += 5;
            BankManager.bank -= priceTimeStops;
            DataControl.control.SaveAll();           
        }
        else {
            iapScreen.GetComponent<Animator>().SetBool("Open", true);
        }
    }

    public void BuyFlashes()
    {
        if (BankManager.bank >= priceFlashes)
        {
            if (ScreenManager.screenManager.GetOpenScreen() == flashesGameObject)
            {
                plus5FlashesPUscreen.GetComponent<Animator>().SetTrigger("Show");
            }
            else
            {
                plus5Flashes.GetComponent<Animator>().SetTrigger("Show");
            }
            amountFlashes += 5;
            BankManager.bank -= priceFlashes;
            DataControl.control.SaveAll();         
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
            if (ScreenManager.screenManager.GetOpenScreen() == flankersGameObject)
            {
                plus5FlankersPUscreen.GetComponent<Animator>().SetTrigger("Show");
            }
            else
            {
                plus5Flankers.GetComponent<Animator>().SetTrigger("Show");
            }
            amountFlankers += 5;
            BankManager.bank -= priceFlankers;
            DataControl.control.SaveAll();      
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
