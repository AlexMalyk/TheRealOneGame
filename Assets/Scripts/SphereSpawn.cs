using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SphereSpawn : MonoBehaviour
{
    //public static SphereSpawn SS;
    public static int leftMax = -2;
    public static int rightMax = 2;
    public static int upMax = 3;
    public static int downMax = -3;

    int rangeOnCanvas = 160;


    public GameObject scoreText;
    public Canvas GameBoard;
    int x;
    int y;
    Animator sphereAnimator;

    void Start()
    {
        sphereAnimator = GetComponent<Animator>();
    }

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
        GameObject exp = Instantiate(Resources.Load("Sphere Explosion", typeof(GameObject))) as GameObject;
        exp.transform.parent = transform.parent;
        exp.GetComponent<RectTransform>().localPosition = new Vector3(x,y,0);
        exp.GetComponent<RectTransform>().localScale = new Vector3(1,1,0);

        SetNewPosition();

        ScoreManager.score += 10;
        GameObject plus = Instantiate(Resources.Load("Plus Score", typeof(GameObject))) as GameObject;
        plus.transform.parent = scoreText.transform.parent;
        plus.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        if (GameManager.manager.mode == Mode.Endless)
        {
            GameManager.manager.time = GameManager.manager.kEndlessModeTime;
        }
        HintManager.manager.DeleteEffects(false, true, true);

        //на позицию влияет масштаб материнского обьекта
    }


}

