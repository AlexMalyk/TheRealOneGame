using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SphereSpawn : MonoBehaviour {
    //public static SphereSpawn SS;
    public static int leftMax = -2;
    public static int rightMax = 2;
    public static int upMax = 3;
    public static int downMax = -3;

    int rangeOnCanvas = 160;

    public Image im;

    public Canvas GameBoard;
    int x;
    int y;

    public void SetNewPosition() {
        Debug.Log("setnewposition");
        x = Random.Range(leftMax, rightMax + 1) * rangeOnCanvas;
        y = Random.Range(downMax, upMax + 1) * rangeOnCanvas;
        Debug.Log("x=" + x + " y=" + y);

        GetComponent<RectTransform>().localPosition = new Vector3(x,y,0);

        Debug.Log("x=" + gameObject.transform.position.x + " y=" + gameObject.transform.position.y + " z=" + gameObject.transform.position.z);
    }

    public void OnMouseDown() {
        Debug.Log("SphereSpawn OnMouseDown");
        SetNewPosition();
        ScoreManager.score += 10;
        if (GameManager.manager.mode == Mode.Endless)
        {
            GameManager.manager.time = GameManager.manager.kEndlessModeTime;
        }
        HintManager.manager.DeleteEffects(false, true, true);
        //на позицию влияет масштаб материнского обьекта
    }
}

