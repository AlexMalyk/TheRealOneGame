using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlayServicesScript : MonoBehaviour
{
    static bool isSigned;

    // Use this for initialization
    void Start()
    {
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
        SignIn();
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SignIn()
    {
        // authenticate user:
        Social.localUser.Authenticate((bool success) =>
        {
            // handle success or failure
            if (success)
            {
                Debug.Log("Success authentificate");
                isSigned = true;
            }
            else
            {
                Debug.Log("Failure authentificate");
                isSigned = false;
            }
        });
    }

    public static void PostScoreToLeaderboard(int score, Mode mode) {
        string modeId;
        if (mode == Mode.Timed) {
            modeId = GPGSIds.leaderboard_best_timed_score;
        }
        else if (mode == Mode.Endless) {
            modeId = GPGSIds.leaderboard_best_endless_score;
        }
        else if (mode == Mode.Zen) {
            modeId = GPGSIds.leaderboard_best_zen_score;
        }
        else {
            Debug.Log("Wrong mode");
            return;
        }


        Social.ReportScore(score, modeId, (bool success) => {
            if (success) Debug.Log("Success score report: " + modeId + " " + score);
            //else Debug.Log("Failure score report");
            // handle success or failure
        });
    }

    public void ShowLeaderboard() {
        if (isSigned)
        {
            Debug.Log("Success show leaderboard");
            Social.ShowLeaderboardUI();
        }
        else
        {
            Debug.Log("Failure authentificate");
            SignIn();
        }
    }
}
