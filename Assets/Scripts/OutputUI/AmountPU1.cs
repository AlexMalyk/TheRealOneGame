using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmountPU1 : MonoBehaviour {


	void Update () {
        GetComponent<Text>().text = HintManager.manager.amountPU1.ToString();
	}
}
