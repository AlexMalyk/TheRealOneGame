using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmountPU2 : MonoBehaviour {

    void Update()
    {
        GetComponent<Text>().text = HintManager.manager.amountPU2.ToString();
    }
}
