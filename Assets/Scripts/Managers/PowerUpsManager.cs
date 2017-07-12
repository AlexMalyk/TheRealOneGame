using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUpsManager : MonoBehaviour {
    public static PowerUpsManager manager;

    int baseSparkleSpeed;
    int doubleSparkleSpeed;
    
    int newX, newZ;

    public GameObject gameScreen;
    public GameObject iapScreen;
    public GameObject powerUpScreen;

    public GameObject timeStopsGameObject;
    public GameObject sparksGameObject;
    public GameObject wingsGameObject;

    public GameObject timeStopButton;
    public GameObject sparkButton;
    public GameObject wingButton;
    public GameObject disabledImagePrefab;
    public string prefabName = "Disabled Image(Clone)";

    public GameObject plus5GameScreenPrefab;
    public GameObject plus5PowerUpScreenPrefab;

    public Text[] wingsAmountTexts;
    public Text[] sparksAmountTexts;
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
    public static bool isSparkUsedInRound;
    [HideInInspector]
    public static bool isWingUsedInRound;

    static string kOncePerMatchMessage = "once_a_game";
    static string kOncePerRoundMessage = "already_used";
    static string kTimeStopDescription = "time_stop_description_short";
    static string kSparkDescription = "spark_description_short";
    static string kWingDescription = "wing_description_short";

    int priceTimeStops = 2000;
    int priceWings = 500;
    int priceSparks = 1000;

    [HideInInspector]
    public  int amountTimeStops;
    [HideInInspector]
    public  int amountSparks;
    [HideInInspector]
    public  int amountWings;

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
        isSparkUsedInRound = false;
        isWingUsedInRound = false;

    }
    public void TimeStopPowerUp()
    {      
        if (!ScreenManager.screenManager.isMenuOpen && isTimeStopUsedInMatch) {
            ShowMessage(LocalizationManager.manager.GetLocalizedValue(kOncePerMatchMessage));
        }
        else if (!ScreenManager.screenManager.isMenuOpen && amountTimeStops > 0)
        {
            GameManager.manager.timeStopsUsed = true;

            amountTimeStops--;
            SetTimeStopsAmount();
            DataManager.manager.SaveAll();

            CreateDisabledImage(timeStopButton);

            isTimeStopUsedInMatch = true;

            ShowMessage(LocalizationManager.manager.GetLocalizedValue(kTimeStopDescription)); 
        }
        else if ( ((!ScreenManager.screenManager.isMenuOpen && amountTimeStops == 0) || ScreenManager.screenManager.GetOpenScreen() != timeStopsGameObject) && ScreenManager.screenManager.isTransition == false)
        {
            GameManager.manager.PauseGame(true);
            ScreenManager.screenManager.isMenuOpen = true;
            gameScreen.GetComponent<Animator>().SetTrigger("HideUp");
            ScreenManager.screenManager.WithoutAdditionalAnimator();
            ScreenManager.screenManager.OpenScreen(timeStopsGameObject);
        }
    }
    public void SparkPowerUp() {
        if (!ScreenManager.screenManager.isMenuOpen && isSparkUsedInRound)
        {
            ShowMessage(LocalizationManager.manager.GetLocalizedValue(kOncePerRoundMessage));
        }
        else if (!ScreenManager.screenManager.isMenuOpen && amountSparks > 0)
        {
            sphereAnimator.SetBool("HintOn", true);

            amountSparks--;
            SetSparksAmount();
            DataManager.manager.SaveAll();

            CreateDisabledImage(sparkButton);

            isSparkUsedInRound = true;

            ShowMessage(LocalizationManager.manager.GetLocalizedValue(kSparkDescription));
        }
        else if ( ((!ScreenManager.screenManager.isMenuOpen && amountSparks == 0) || ScreenManager.screenManager.GetOpenScreen() != sparksGameObject) && ScreenManager.screenManager.isTransition == false)
        {
            GameManager.manager.PauseGame(true);
            ScreenManager.screenManager.isMenuOpen = true;
            gameScreen.GetComponent<Animator>().SetTrigger("HideUp");
            ScreenManager.screenManager.WithoutAdditionalAnimator();
            ScreenManager.screenManager.OpenScreen(sparksGameObject);
        }
    }
    public void WingPowerUp() {
        if (!ScreenManager.screenManager.isMenuOpen && isWingUsedInRound)
        {
            ShowMessage(LocalizationManager.manager.GetLocalizedValue(kOncePerRoundMessage));
        }
        else if (!ScreenManager.screenManager.isMenuOpen && amountWings > 0)
        {
            SetWings();

            amountWings--;
            SetWingsAmount();
            DataManager.manager.SaveAll();

            CreateDisabledImage(wingButton);

            isWingUsedInRound = true; 

            ShowMessage(LocalizationManager.manager.GetLocalizedValue(kWingDescription));
        }
        else if ( ((!ScreenManager.screenManager.isMenuOpen && amountWings == 0) || ScreenManager.screenManager.GetOpenScreen() != wingsGameObject) && ScreenManager.screenManager.isTransition == false)
        {
            GameManager.manager.PauseGame(true);
            ScreenManager.screenManager.isMenuOpen = true;
            gameScreen.GetComponent<Animator>().SetTrigger("HideUp");
            ScreenManager.screenManager.WithoutAdditionalAnimator();
            ScreenManager.screenManager.OpenScreen(wingsGameObject);
        }
    }

    public void DeleteEffects(bool timeStops, bool flashes, bool flankers) {
        if (timeStops) {
            DeleteDisabledImage(timeStopButton.transform.Find(prefabName));
            isTimeStopUsedInMatch = false;
        }
		if (flashes) {
            DeleteDisabledImage(sparkButton.transform.Find(prefabName));

            isSparkUsedInRound = false;
            RemoveSpark();
        }     
		if (flankers) {
            DeleteDisabledImage(wingButton.transform.Find(prefabName));

            isWingUsedInRound = false;
            RemoveWings();
        }
    }

	public void RemoveSpark(){
		sphereAnimator.SetBool("HintOn", false);
	}

    public void RemoveWings() {
        leftSide.SetActive(false);
        rightSide.SetActive(false);
    }

    public void HideWings(bool value) {
        leftSide.GetComponent<Image>().enabled = !value;
        rightSide.GetComponent<Image>().enabled = !value;
    }

    public void SetWings() {
        if (sphere.transform.parent.GetComponent<RectTransform>().localPosition.x <= 0)
        {
            leftSide.SetActive(true);
        }

        if (sphere.transform.parent.GetComponent<RectTransform>().localPosition.x >= 0)
        {
            rightSide.SetActive(true);
        }
    }

    public void BuyTimeStops() {
        if (BankManager.bank >= priceTimeStops)
        {
            PlusAnimation(powerUpScreen.transform.Find("Mid").Find("Power Ups").Find("Time Stop").gameObject, timeStopButton, 5);
            AudioManager.manager.PlayPositiveSound();
            amountTimeStops += 5;
            SetTimeStopsAmount();

            BankManager.bank -= priceTimeStops;
            BankManager.isBankChanged = true;

            DataManager.manager.SaveAll();
        }
        else {
            iapScreen.GetComponent<Animator>().SetBool("Open", true);
        }
    }

    public void BuySparks()
    {
        if (BankManager.bank >= priceSparks)
        {
            PlusAnimation(powerUpScreen.transform.Find("Mid").Find("Power Ups").Find("Spark").gameObject, sparkButton, 5);
            AudioManager.manager.PlayPositiveSound();
            amountSparks += 5;
            SetSparksAmount();

            BankManager.bank -= priceSparks;
            BankManager.isBankChanged = true;

            DataManager.manager.SaveAll();
        }
        else
        {
            iapScreen.GetComponent<Animator>().SetBool("Open", true);
        }
    }

    public void BuyWings()
    {
        if (BankManager.bank >= priceWings)
        {
            PlusAnimation(powerUpScreen.transform.Find("Mid").Find("Power Ups").Find("Wing").gameObject, wingButton, 5);
            AudioManager.manager.PlayPositiveSound();
            amountWings += 5;
            SetWingsAmount();

            BankManager.bank -= priceWings;
            BankManager.isBankChanged = true;

            DataManager.manager.SaveAll();
        }
        else
        {
            iapScreen.GetComponent<Animator>().SetBool("Open", true);
        }
    }

    void ShowMessage(string message) {
        if (hintMessageParent.transform.Find(hintMessageName)) {
            Destroy(hintMessageParent.transform.Find(hintMessageName).gameObject);
        }
        
        
        GameObject mess = Instantiate(hintMessagePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        mess.GetComponent<Text>().text = message;
        mess.transform.SetParent(hintMessageParent.transform, false);
        mess.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
    }

    void CreateDisabledImage(GameObject parent) {
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

    public void PlusAnimation(GameObject parent, GameObject parent2, int amount) {
        GameObject plus;
        if (ScreenManager.screenManager.GetOpenScreen() == powerUpScreen)
        {
            plus = Instantiate(plus5PowerUpScreenPrefab, new Vector3(0,0,0), Quaternion.identity);
            plus.GetComponent<Text>().text = "+" + amount;
            plus.transform.SetParent(parent.transform, false);
            
        }
        else
        {
            plus = Instantiate(plus5GameScreenPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            plus.GetComponent<Text>().text = "+" + amount;
            plus.transform.SetParent(parent2.transform, false);
        }
        plus.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    public void SetWingsAmount() {
        foreach (Text item in wingsAmountTexts) {
            item.text = amountWings.ToString();
        }
    }
    public void SetSparksAmount()
    {
        foreach (Text item in sparksAmountTexts)
        {
            item.text = amountSparks.ToString();
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
