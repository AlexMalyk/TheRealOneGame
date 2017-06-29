using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateOnStore : MonoBehaviour {

    string urlGoogle = "https://play.google.com/store/apps/details?id=com.ogs.therealone";

    public void OpenStore() {
        Application.OpenURL(urlGoogle);   
    }
}
