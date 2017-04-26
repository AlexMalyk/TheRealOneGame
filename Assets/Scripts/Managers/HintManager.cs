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
    public GameObject powerUpScreen;

    public GameObject timeStopsGameObject;
    public GameObject flashesGameObject;
    public GameObject flankersGameObject;

    public Button timeStopButton;
    public Button flashButton;
    public Button flankerButton;
    public GameObject disabledImagePrefab;
    public string prefabName = "Disabled Image(Clone)";

    public GameObject plus5GameScreenPrefab;
    public GameObject plus5PowerUpScreenPrefab;

    public Text[] flankersAmountTexts;
    public Text[] flashesAmountTexts;
    public Text[] timeStopsAmountTexts;

    public GameObject sphere;
    public GameObject leftSide;
    public GameObject rightSide;

    public GameObject hintMessageParent;
    public GameObject hintMessagePrefab;
    public string hintMessageName = "HintMessage(Clone)";

    [HideInInspector]
    public static bool isTimeStopUsedInMatch;
    [HideInInspector]
    public static bool isFlashUsedInRound;
    [HideInInspector]
    public static bool isFlankerUsedInRound;

    static string kOncePerMatchMessage = "Can only be used once a match";
    static string kOncePerRoundMessage = "Already used";
    static string kTimeStopDescription = "Time stopped for 10 seconds";
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

        isTimeStopUsedInMatch = false;
        isFlashUsedInRound = false;
        isFlankerUsedInRound = false;

    }
    public void TimeStopHint()
    {
        if (!ScreenManager.screenManager.isMenuOpen && isTimeStopUsedInMatch) {
            ShowMessage(kOncePerMatchMessage);
        }
        else if (!ScreenManager.screenManager.isMenuOpen && amountTimeStops > 0)
        {
            GameManager.manager.timeStopsUsed = true;

            amountTimeStops--;
            SetTimeStopsAmount();
            DataControl.control.SaveAll();

            CreateDisabledImage(timeStopButton);

            isTimeStopUsedInMatch = true;

            ShowMessage(kTimeStopDescription); 
        }
        else if ( (!ScreenManager.screenManager.isMenuOpen && amountTimeStops == 0) || ScreenManager.screenManager.GetOpenScreen() != timeStopsGameObject)
        {
            GameManager.manager.PauseGame(true);
            ScreenManager.screenManager.isMenuOpen = true;
            gameScreen.GetComponent<Animator>().SetTrigger("HideUp");
            ScreenManager.screenManager.WithoutAdditionalAnimator();
            ScreenManager.screenManager.OpenScreen(timeStopsGameObject);
        }
    }
    public void FlashHint() {
        if (!ScreenManager.screenManager.isMenuOpen && isFlashUsedInRound)
        {
            ShowMessage(kOncePerRoundMessage);
        }
        else if (!ScreenManager.screenManager.isMenuOpen && amountFlashes > 0)
        {
            sphereAnimator.SetBool("HintOn", true);

            amountFlashes--;
            SetFlashesAmount();
            DataControl.control.SaveAll();

            CreateDisabledImage(flashButton);

            isFlashUsedInRound = true;

            ShowMessage(kFlashesDescription);
        }
        else if ( (!ScreenManager.screenManager.isMenuOpen && amountFlashes == 0) || ScreenManager.screenManager.GetOpenScreen() != flashesGameObject)
        {
            GameManager.manager.PauseGame(true);
            ScreenManager.screenManager.isMenuOpen = true;
            gameScreen.GetComponent<Animator>().SetTrigger("HideUp");
            ScreenManager.screenManager.WithoutAdditionalAnimator();
            ScreenManager.screenManager.OpenScreen(flashesGameObject);
        }
    }
    public void FlankerHint() {
        if (!ScreenManager.screenManager.isMenuOpen && isFlankerUsedInRound)
        {
            ShowMessage(kOncePerRoundMessage);
        }
        else if (!ScreenManager.screenManager.isMenuOpen && amountFlankers > 0)
        {
            if (sphere.transform.parent.GetComponent<RectTransform>().localPosition.x <= 0)
            {
                leftSide.SetActive(true);
            }

            if (sphere.transform.parent.GetComponent<RectTransform>().localPosition.x >= 0){
                rightSide.SetActive(true);
            }

            amountFlankers--;
            SetFlankersAmount();
            DataControl.control.SaveAll();

            CreateDisabledImage(flankerButton);

            isFlankerUsedInRound = true; 

            ShowMessage(kFlankerDescription);
        }
        else if ( (!ScreenManager.screenManager.isMenuOpen && amountFlankers == 0) || ScreenManager.screenManager.GetOpenScreen() != flankersGameObject)
        {
            GameManager.manager.PauseGame(true);
            ScreenManager.screenManager.isMenuOpen = true;
            gameScreen.GetComponent<Animator>().SetTrigger("HideUp");
            ScreenManager.screenManager.WithoutAdditionalAnimator();
            ScreenManager.screenManager.OpenScreen(flankersGameObject);
        }
    }

    public void DeleteEffects(bool timeStops, bool flashes, bool flankers) {
        if (timeStops) {
            DeleteDisabledImage(timeStopButton.transform.FindChild(prefabName));
            isTimeStopUsedInMatch = false;
        }
		if (flashes) {
            DeleteDisabledImage(flashButton.transform.FindChild(prefabName));

            isFlashUsedInRound = false;
            RemoveFlash();
        }     
		if (flankers) {
            DeleteDisabledImage(flankerButton.transform.FindChild(prefabName));

            isFlankerUsedInRound = false;
            RemoveFlankers();
        }
    }

	public void RemoveFlash(){
		sphereAnimator.SetBool("HintOn", false);
	}

    public void RemoveFlankers() {
        leftSide.SetActive(false);
        rightSide.SetActive(false);
    }

    public void HideFlankers(bool value) {
        leftSide.GetComponent<Image>().enabled = !value;
        rightSide.GetComponent<Image>().enabled = !value;
    }

    public void BuyTimeStops() {
        if (BankManager.bank >= priceTimeStops)
        {
            Plus5Animation(powerUpScreen.transform.FindChild("Mid").FindChild("Mid").FindChild("Time Stops"), timeStopButton);

            amountTimeStops += 5;
            SetTimeStopsAmount();

            BankManager.bank -= priceTimeStops;
            BankManager.isBankChanged = true;

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
            Plus5Animation(powerUpScreen.transform.FindChild("Mid").FindChild("Mid").FindChild("Flashes"), flashButton);

            amountFlashes += 5;
            SetFlashesAmount();

            BankManager.bank -= priceFlashes;
            BankManager.isBankChanged = true;

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
            Plus5Animation(powerUpScreen.transform.FindChild("Mid").FindChild("Mid").FindChild("Flankers"), flankerButton);
            
            amountFlankers += 5;
            SetFlankersAmount();

            BankManager.bank -= priceFlankers;
            BankManager.isBankChanged = true;

            DataControl.control.SaveAll();
        }
        else
        {
            iapScreen.GetComponent<Animator>().SetBool("Open", true);
        }
    }

    void ShowMessage(string message) {
        if (hintMessageParent.transform.FindChild(hintMessageName)) {
            Destroy(hintMessageParent.transform.FindChild(hintMessageName).gameObject);
        }
        
        
        GameObject mess = Instantiate(hintMessagePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        mess.GetComponent<Text>().text = message;
        mess.transform.SetParent(hintMessageParent.transform, false);
        mess.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
    }

    void CreateDisabledImage(Button parent) {
        GameObject disImage = Instantiate(disabledImagePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        disImage.transform.SetParent(parent.transform, false);
        disImage.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    void DeleteDisabledImage(Transform prefabObject) {
        if (prefabObject)
        {
            Destroy(prefabObject.gameObject);
        }
    }

    void Plus5Animation(Transform parent, Button parent2) {
        GameObject plus;
        if (ScreenManager.screenManager.GetOpenScreen() == powerUpScreen)
        {
            plus = Instantiate(plus5PowerUpScreenPrefab, new Vector3(0,0,0), Quaternion.identity);
            plus.transform.SetParent(parent, false);
        }
        else
        {
            plus = Instantiate(plus5GameScreenPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            plus.transform.SetParent(parent2.transform, false);
        }
        plus.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    public void SetFlankersAmount() {
        foreach (Text item in flankersAmountTexts) {
            item.text = amountFlankers.ToString();
        }
    }
    public void SetFlashesAmount()
    {
        foreach (Text item in flashesAmountTexts)
        {
            item.text = amountFlashes.ToString();
        }
    }
    public void SetTimeStopsAmount()
    {
        foreach (Text item in timeStopsAmountTexts)
        {
            item.text = amountTimeStops.ToString();
        }
    }
}
