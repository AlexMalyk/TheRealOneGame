using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BankManager : MonoBehaviour
{

    public static int bank;
    public static bool isBankChanged;

    public Text[] texts;

    int thousands;
    int hundreds;


    void Update()
    {
        if (isBankChanged)
        {
            if (bank >= 1000)
            {
                thousands = bank / 1000;
                hundreds = bank - thousands * 1000;
                if (hundreds < 10)
                {
                    foreach (Text item in texts)
                    {
                        item.text = thousands + ",00" + hundreds;
                    }
                }
                else if (hundreds < 100)
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
                        item.text = thousands + "," + hundreds;
                    }
                }
            }
            else
            {
                foreach (Text item in texts)
                {
                    item.text = bank.ToString();
                    Debug.Log(item.text);
                }
            }
            isBankChanged = false;
        }
    }
}
