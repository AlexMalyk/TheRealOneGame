using UnityEngine;
using System.Collections;

public class ScanQuarterHint : MonoBehaviour {
    public GameObject Sphere;
    public GameObject Plane;

    MeshRenderer meshRen;

    void Start()
    {
        meshRen = Plane.GetComponent<MeshRenderer>();
        Debug.Log(Sphere.transform.position);
    }

    public void QuarterScan() {
        Debug.Log(Sphere.transform.position);
        if (Sphere.transform.position.z > 0)
        {
            if (Sphere.transform.position.x > 0)
            {
                Debug.Log("ifif");
                Plane.transform.position = new Vector3(1.5f, 0.01f, 2.5f);
            }
            else {
                Debug.Log("ifelse");
                Plane.transform.position = new Vector3(-1.5f, 0.01f, 2.5f);
            }
        }
        else {
            if (Sphere.transform.position.x > 0)
            {
                Debug.Log("elseif");
                Plane.transform.position = new Vector3(1.5f, 0.01f, -2.5f);
            }
            else
            {
                Debug.Log("elseelse");
                Plane.transform.position = new Vector3(-1.5f, 0.01f, -2.5f);
            }
        }
        StartCoroutine(MyCoroutine());
    }

    private IEnumerator MyCoroutine() {
        meshRen.enabled = true;
        yield return CoroutineUtil.WaitForRealSeconds(0.5f);

        meshRen.enabled = false;
    }
}
