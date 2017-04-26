using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BestEndlessScore : MonoBehaviour {

	public static bool isUpdated;

	// Update is called once per frame
	void Update () {
		if (isUpdated) {
			GetComponent<Text> ().text = DataControl.control.bestScoreEndless.ToString ();
			isUpdated = false;
		}
	}
}
