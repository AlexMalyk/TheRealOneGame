﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BestZenScore : MonoBehaviour {

	public static bool isUpdated;

    // Update is called once per frame
    void Update()
	{
		if (isUpdated) {
			GetComponent<Text> ().text = ScoreManager.manager.bestScoreZen.ToString ();
			isUpdated = false;
		}
	}
}

