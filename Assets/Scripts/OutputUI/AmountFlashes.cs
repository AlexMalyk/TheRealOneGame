using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmountFlashes : MonoBehaviour {

    void Update()
    {
        GetComponent<Text>().text = HintManager.manager.amountFlashes.ToString();
    }
}
