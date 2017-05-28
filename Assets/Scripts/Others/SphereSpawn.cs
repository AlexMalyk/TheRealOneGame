using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SphereSpawn : MonoBehaviour
{
    int leftMax = -2;
    int rightMax = 2;
    int upMax = 3;
    int downMax = -3;

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

        GetComponent<RectTransform>().localPosition = new Vector3(x, y, 0);
    }

    public void OnMouseDown()
    {
        AudioManager.manager.PlayFoundSound();

        CreateExplosion();

        SetNewPosition();

        ScoreManager.manager.ChangeScore();

        GameObject plus = Instantiate(prefabPlusText, new Vector3(0, 0, 0), Quaternion.identity);
        plus.transform.SetParent(scoreText.transform.parent, false);
        plus.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        

        if (GameManager.manager.mode == Mode.Endless)
        {
            GameManager.manager.time = GameManager.manager.kEndlessModeTime;
        }
        PowerUpsManager.manager.DeleteEffects(false, true, true);
    }

    public void CreateExplosion() {
        GameObject exp = Instantiate(prefabExplosion, new Vector3(0, 0, 0), Quaternion.identity);
        exp.transform.SetParent(transform.parent);
        exp.GetComponent<RectTransform>().localPosition = new Vector3(x, y, 0);
        exp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);
    }
}

