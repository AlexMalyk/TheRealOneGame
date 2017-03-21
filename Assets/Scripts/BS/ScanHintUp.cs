using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScanHintUp : MonoBehaviour {

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
    //    hintAmount = DataControl.control.amountScanUp;
    //    buttonText.text = "" + DataControl.control.amountScanUp;
    //}

    //public void OnTouchDown() {
    //    if (sphere.transform.position.z >= 0){
    //        scanPlaneFinish.GetComponent<Renderer>().material = newMaterialSuccess;
    //    }
    //    else {
    //        scanPlaneFinish.GetComponent<Renderer>().material = newMaterialFail;
    //    }
    //    StartCoroutine(MyCoroutine());
    //    DataControl.control.amountScanUp -= 1;
    //    buttonText.text = "" + DataControl.control.amountScanUp;
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


    //public void AmountUp() {
    //    DataControl.control.amountScanUp += 1; 
    //    buttonText.text = "" + DataControl.control.amountScanUp;
    //    DataControl.control.Save();
    //}

    //public void AmountDown()
    //{
    //    DataControl.control.amountScanUp -= 1;
    //    buttonText.text = "" + DataControl.control.amountScanUp;
    //    DataControl.control.Save();
    //}

    //public void Load() {
    //    DataControl.control.Load();
    //    buttonText.text = "" + DataControl.control.amountScanUp;
    //}
}
