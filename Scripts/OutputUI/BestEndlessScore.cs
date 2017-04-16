using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BestEndlessScore : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = DataControl.control.bestScoreEndless.ToString();
    }
}
