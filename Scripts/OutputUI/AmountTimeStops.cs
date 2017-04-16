using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmountTimeStops : MonoBehaviour {


	void Update () {
        GetComponent<Text>().text = HintManager.manager.amountTimeStops.ToString();
	}
}
