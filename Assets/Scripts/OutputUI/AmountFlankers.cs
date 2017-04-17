using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmountFlankers : MonoBehaviour {
    public static bool isAmountChanged;
    public Text[] texts;

    void Start() {
        isAmountChanged = true;
    }

    void Update()
    {
        if (isAmountChanged) {
            foreach (Text item in texts) {
                item.text = HintManager.manager.amountFlankers.ToString();
            }
            isAmountChanged = false;
        }
    }
}
