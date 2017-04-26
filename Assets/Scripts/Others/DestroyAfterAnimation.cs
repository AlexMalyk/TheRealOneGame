using UnityEngine;
using System.Collections;

public class DestroyAfterAnimation : MonoBehaviour {

    public bool isAnimationEnded = false;
	
	// Update is called once per frame
	void Update () {
        if (isAnimationEnded) {
            Destroy(gameObject);
        }
	}
}
