using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BankManager : MonoBehaviour {

    public static int bank;
    public Text BankText;

    int thousands;
    int hundreds;

    // Update is called once per frame
    void Update () {
        if (bank >= 1000)
        {
            thousands = bank / 1000;
            hundreds = bank - thousands * 1000;
            if (hundreds < 100)
            {
                BankText.text = thousands + ",0" + hundreds;
            }
            else {
                BankText.text = thousands + "," + hundreds;
            }
        }
        else {
            BankText.text = bank.ToString();
        }
    }
}
