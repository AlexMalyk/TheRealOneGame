using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using UnityEngine.UI;

public class AdControl : MonoBehaviour, IRewardedVideoAdListener{

    public static AdControl control;

    public Text kRewardMessage;

    public GameObject endGameSpark;
    public GameObject endGameTimeStop;
    public GameObject endGameWing;

    string kRewardDefault = "ad_points";
    string kRewardLower = "ad_spark_wing";
    string kRewardMiddle = "ad_time_stop_wing";
    string kRewardHigher = "ad_time_stop_spark";

    int rewardAmount = 1;
    int rewarsPoints = 250;

    delegate void AdDelegate();
    AdDelegate adReward;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != null)
        {
            Destroy(gameObject);
        }
    }

    void Start() {
        System.String appKey = "55135bf0c0b7525422af600c5a746d85e028da1628431d85";
        Appodeal.disableLocationPermissionCheck();
        Appodeal.disableWriteExternalStoragePermissionCheck();
        Appodeal.initialize(appKey, Appodeal.REWARDED_VIDEO);
        Appodeal.setTesting(true);
    }

    public void ShowRewardedAd() {
        if (Application.platform == RuntimePlatform.Android)
        {
            Appodeal.setRewardedVideoCallbacks(this);
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
        else {
            adReward();
            DataControl.control.SaveAll();
        }
    }

    public void SetupSpecialAd() {
        if (GameManager.manager.isNewRecord)
        {
            kRewardMessage.text = LocalizationManager.manager.GetLocalizedValue(kRewardHigher);
            adReward = HigherReward;
        }
        else if (PowerUpsManager.manager.amountTimeStops == 0 )
        {
            kRewardMessage.text = LocalizationManager.manager.GetLocalizedValue(kRewardMiddle);
            adReward = MiddleReward;
        }
        else if (PowerUpsManager.manager.amountSparks == 0 || PowerUpsManager.manager.amountWings == 0)
        {
            kRewardMessage.text = LocalizationManager.manager.GetLocalizedValue(kRewardLower);
            adReward = LowerReward;
        }
        else {
            kRewardMessage.text = LocalizationManager.manager.GetLocalizedValue(kRewardDefault);
            adReward = DefaultReward;
        }       
    }

    public void HigherReward() {
        PowerUpsManager.manager.amountTimeStops += rewardAmount;
        PowerUpsManager.manager.PlusAnimation(endGameTimeStop, endGameTimeStop, rewardAmount);
        PowerUpsManager.manager.SetTimeStopsAmount();

        PowerUpsManager.manager.amountSparks += rewardAmount;
        PowerUpsManager.manager.PlusAnimation(endGameSpark, endGameSpark, rewardAmount);
        PowerUpsManager.manager.SetSparksAmount();
    }
    public void MiddleReward()
    {
        PowerUpsManager.manager.amountTimeStops += rewardAmount;
        PowerUpsManager.manager.PlusAnimation(endGameTimeStop, endGameTimeStop, rewardAmount);
        PowerUpsManager.manager.SetTimeStopsAmount();

        PowerUpsManager.manager.amountWings += rewardAmount;
        PowerUpsManager.manager.PlusAnimation(endGameWing, endGameWing, rewardAmount);
        PowerUpsManager.manager.SetWingsAmount();
    }
    public void LowerReward()
    {
        PowerUpsManager.manager.amountSparks += rewardAmount;
        PowerUpsManager.manager.PlusAnimation(endGameSpark, endGameSpark, rewardAmount);
        PowerUpsManager.manager.SetSparksAmount();

        PowerUpsManager.manager.amountWings += rewardAmount;
        PowerUpsManager.manager.PlusAnimation(endGameWing, endGameWing, rewardAmount);
        PowerUpsManager.manager.SetWingsAmount();
    }
    public void DefaultReward() {
        BankManager.bank += rewarsPoints;
        BankManager.isBankChanged = true;
    }



    #region Rewarded Video callback handlers
    public void onRewardedVideoLoaded() {

    }

    public void onRewardedVideoFailedToLoad() {

    }
    public void onRewardedVideoShown() {

    }
    public void onRewardedVideoClosed() {

    }
    public void onRewardedVideoFinished(int amount, string name) {
        adReward();
        DataControl.control.SaveAll();      
    }
    #endregion
}

public enum AdType {
    newRecord, outOfPowerUp, smallScore, gamesPlayed5, gamesPlayed10,
}

