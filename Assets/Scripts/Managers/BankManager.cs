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
	
	string bankText;


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
					bankText = thousands + ",00" + hundreds;
                }
                else if (hundreds < 100)
                {
					bankText = thousands + ",0" + hundreds;
                }              
                else
                {
					bankText = thousands + "," + hundreds;
                }
            }
            else
            {
				bankText = bank.ToString();
                
            }
			foreach (Text item in texts)
            {
                item.text = bankText;
            }
			
            isBankChanged = false;
        }
    }
}
