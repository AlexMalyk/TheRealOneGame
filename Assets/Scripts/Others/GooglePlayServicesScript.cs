using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlayServicesScript : MonoBehaviour
{

    private string lbIdTimed = "CgkIlOeA57ICEAIQAA";
    private string lbIdEndless = "CgkIlOeA57ICEAIQAg";
    private string lbIdZen = "CgkIlOeA57ICEAIQAQ";

    // Use this for initialization
    void Start()
    {
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
        });
    }

    public void PostScoreToLeaderboard(int score, Mode mode) {
        string modeId;
        if (mode == Mode.Timed) {
            modeId = lbIdTimed;
        }
        else if (mode == Mode.Endless) {
            modeId = lbIdEndless;
        }
        else if (mode == Mode.Zen) {
            modeId = lbIdZen;
        }
        else {
            Debug.Log("Wrong mode");
            return;
        }


        Social.ReportScore(score, "modeId", (bool success) => {
            // handle success or failure
        });
    }

    public void ShowLeaderboard() {
        Social.ShowLeaderboardUI();
    }
}
