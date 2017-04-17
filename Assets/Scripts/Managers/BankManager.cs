using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BankManager : MonoBehaviour {

    public static int bank;
    public static bool isBankChanged;

    public Text[] texts;

    int thousands;
    int hundreds;

    void Start() {
        isBankChanged = false;
    }

    void Update () {
        if (isBankChanged)
        {
            if (bank >= 1000)
            {
                Debug.Log("bank changed if");
                thousands = bank / 1000;
                hundreds = bank - thousands * 1000;
                if (hundreds< 100)
                {
                    foreach (Text item in texts)
                    {
                        item.text = thousands + ",0" + hundreds;
                    }
                }
                else
                {
                    foreach (Text item in texts)
                    {
                        item.text = thousands + "," + hundreds; ;
                    }
                }
            }
            else
            {
                Debug.Log("bank changed else");
                foreach (Text item in texts) {
                    item.text = bank.ToString();
                }
            }
            isBankChanged = false;
        }
    }
}
