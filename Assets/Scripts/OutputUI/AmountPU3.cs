using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmountPU3 : MonoBehaviour {

    void Update()
    {
        GetComponent<Text>().text = HintManager.manager.amountPU3.ToString();
    }
}
