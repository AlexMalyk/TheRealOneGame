using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BankManager : MonoBehaviour {

    public static int bank;
    public Text BankText;

    int pricePU1 = 10;
    int pricePU2 = 5;
    int pricePU3 = 10;

    // Update is called once per frame
    void Update () {
        BankText.text = bank.ToString();
	}
}
