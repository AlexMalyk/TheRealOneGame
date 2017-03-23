using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using UnityEngine.UI;

public class AdControl : MonoBehaviour, IRewardedVideoAdListener{

    public static AdControl control;
    public string zoneId;
    public Text testResult;
    public Text score;
    int scoreInt;

    public Text kPromoMessage;

    string kNewRecordMessage = "+5 timers";
    string kZeroAmountOfHint = "+5 flanks";
    string kDefault = "+5 flashes";
    string kRewardType1 = "record";
    string kRewardType2 = "out of hint";
    string kRewardType3 = "default";

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
        System.String appKey = "e133979ce241903311829677d991673aa4880304e9224e0f";
        Appodeal.disableLocationPermissionCheck();
        Appodeal.initialize(appKey, Appodeal.REWARDED_VIDEO);
        Appodeal.setTesting(true);
        scoreInt = 0;
    }

    public void ShowRewardedAd() {
        Appodeal.setRewardedVideoCallbacks(this);
        Appodeal.show(Appodeal.REWARDED_VIDEO);
        onRewardedVideoFinished(rewardAmount, rewardType);
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
        print("Video loaded");
        testResult.text += "L";
    }

    public void onRewardedVideoFailedToLoad() {
        print("Video failed");
        testResult.text += "E";
    }
    public void onRewardedVideoShown() {
        print("Video shown");
        testResult.text += "S";
    }
    public void onRewardedVideoClosed() {
        print("Video closed");
        testResult.text += "C";
    }
    public void onRewardedVideoFinished(int amount, string name) {
        print("Reward: finished");
        HintManager.manager.amountPU1 += amount;
        HintManager.manager.amountPU2 += amount;
        HintManager.manager.amountPU3 += amount;

        DataControl.control.SavePU();
    }
    #endregion
}

