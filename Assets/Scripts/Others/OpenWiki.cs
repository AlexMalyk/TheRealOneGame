using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWiki : MonoBehaviour {

    public void Open() {
        Application.OpenURL("https://en.wikipedia.org/wiki/Grid_illusion");
    }
}
