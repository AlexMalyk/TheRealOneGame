using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmountFlankers : MonoBehaviour {

    void Update()
    {
        GetComponent<Text>().text = HintManager.manager.amountFlankers.ToString();
    }
}
