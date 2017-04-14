using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SphereSpawn : MonoBehaviour
{
    //public static SphereSpawn SS;
    public  int leftMax = -2;
    public  int rightMax = 2;
    public  int upMax = 3;
    public  int downMax = -3;

    public GameObject prefabExplosion;
    public GameObject prefabPlusText;

    int rangeOnCanvas = 160;


    public GameObject scoreText;
    public Canvas GameBoard;
    int x;
    int y;

    public void SetNewPosition()
    {
        x = Random.Range(leftMax, rightMax + 1) * rangeOnCanvas;
        y = Random.Range(downMax, upMax + 1) * rangeOnCanvas;
        Debug.Log("x=" + x + " y=" + y);

        GetComponent<RectTransform>().localPosition = new Vector3(x, y, 0);

        Debug.Log("x=" + gameObject.transform.position.x + " y=" + gameObject.transform.position.y + " z=" + gameObject.transform.position.z);
    }

    public void OnMouseDown()
    {
        AudioManager.manager.PlayFoundSound();

        CreateExplosion();

        SetNewPosition();

        ScoreManager.score += 10;
        GameObject plus = Instantiate(prefabPlusText, new Vector3(0, 0, 0), Quaternion.identity);
        plus.transform.SetParent(scoreText.transform.parent, false);
        plus.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        

        if (GameManager.manager.mode == Mode.Endless)
        {
            GameManager.manager.time = GameManager.manager.kEndlessModeTime;
        }
        HintManager.manager.DeleteEffects(false, true, true);

        //на позицию влияет масштаб материнского обьекта
    }


    public void CreateExplosion() {
        GameObject exp = Instantiate(prefabExplosion, new Vector3(0, 0, 0), Quaternion.identity);
        exp.transform.SetParent(transform.parent);
        exp.GetComponent<RectTransform>().localPosition = new Vector3(x, y, 0);
        exp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);
    }



}

