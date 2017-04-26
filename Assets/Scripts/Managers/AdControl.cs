using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using UnityEngine.UI;

public class AdControl : MonoBehaviour, IRewardedVideoAdListener{

    public static AdControl control;
    public string zoneId;
    public Text testResult;
    public Text score;

    public Text kPromoMessage;

    string kNewRecordMessage = "+5 timers";
    string kZeroAmountOfHint = "+5 flanks";
    string kDefault = "+5 flashes";
    string kRewardType1 = "record";
    string kRewardType2 = "out of hint";
    string kRewardType3 = "default";
    string kRewardType4 = "points";

    string rewardType;
    int rewardAmount;

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
        Appodeal.initialize(appKey, Appodeal.REWARDED_VIDEO);
        Appodeal.setTesting(true);
    }

    public void ShowRewardedAd() {
        Appodeal.setRewardedVideoCallbacks(this);
        Appodeal.show(Appodeal.REWARDED_VIDEO);
    }

    public void SetupSpecialAd() {
        if (GameManager.manager.isNewRecord)
        {
            kPromoMessage.text = kNewRecordMessage;
            rewardType = kRewardType1;
            rewardAmount = 3;
        }
        else if (HintManager.manager.amountTimeStops == 0 || HintManager.manager.amountFlashes == 0 || HintManager.manager.amountFlankers == 0)
        {
            kPromoMessage.text = kZeroAmountOfHint;
            rewardType = kRewardType2;
            rewardAmount = 2;
        }
        else {
            kPromoMessage.text = kDefault;
            rewardType = kRewardType3;
            rewardAmount = 1;
        }       
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

        BankManager.bank += 100;
        DataControl.control.SaveAll();
        
    }
    #endregion
}

public enum AdType {
    newRecord, outOfPowerUp, smallScore, gamesPlayed5, gamesPlayed10,
}

