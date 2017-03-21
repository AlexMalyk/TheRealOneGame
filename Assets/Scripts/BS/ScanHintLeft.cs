using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScanHintLeft : MonoBehaviour {

    //public GameObject sphere;
    //public GameObject scanPlaneFinish;

    //public Material newMaterialSuccess;
    //public Material newMaterialFail;

    //public Text buttonText;

    //Animator anima;
    //MeshRenderer meshRend;

    //private int hintAmount;

    //void Start() {
    //    anima = GetComponent<Animator>();
    //    meshRend = GetComponent<MeshRenderer>();
    //    hintAmount = DataControl.control.amountScanLeft;
    //    buttonText.text = "" + DataControl.control.amountScanLeft;
    //}

    //public void OnTouchDown() {
    //    if (sphere.transform.position.x <= 0){
    //        scanPlaneFinish.GetComponent<Renderer>().material = newMaterialSuccess;
    //    }
    //    else{
    //        scanPlaneFinish.GetComponent<Renderer>().material = newMaterialFail;
    //    }
    //    StartCoroutine(MyCoroutine());
    //    DataControl.control.amountScanLeft -= 1;
    //    buttonText.text = "" + DataControl.control.amountScanLeft;
    //}

    //private IEnumerator MyCoroutine()
    //{
    //    meshRend.enabled = true;
    //    anima.SetTrigger("PlayAnimation");
    //    yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1f));

    //    anima.ResetTrigger("PlayAnimation");
    //    scanPlaneFinish.SetActive(true);

    //    yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.5f));
    //    scanPlaneFinish.SetActive(false);
    //    meshRend.enabled = false;
    //}

    //public void AmountUp()
    //{
    //    DataControl.control.amountScanLeft += 1;
    //    buttonText.text = "" + DataControl.control.amountScanLeft;
    //    DataControl.control.Save();
    //}

    //public void AmountDown()
    //{
    //    DataControl.control.amountScanLeft -= 1;
    //    buttonText.text = "" + DataControl.control.amountScanLeft;
    //    DataControl.control.Save();
    //}

    //public void Load()
    //{
    //    DataControl.control.Load();
    //    buttonText.text = "" + DataControl.control.amountScanLeft;
    //}
}
