using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour {

    public GameObject nextScreen;

    public Button sphere;
    public GameObject prefabExplosion;

    public bool isAnimationEnd;
    public bool isFinish;
    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();

        isFinish = false;

        sphere.onClick.AddListener(()=>PlaySecond());

		SettingsManager.manager.isAudioOn = true;
		SettingsManager.manager.isVibrationOn = true;
    }

    public void PlaySecond() {
        Debug.Log("play second");
        if (isAnimationEnd)
        {
            CreateExplosion();

            anim.SetTrigger("PlaySecond");
            sphere.onClick.RemoveAllListeners();
            sphere.onClick.AddListener(() => FirstMove());
        }
    }

    public void FirstMove()
    {
        if (isAnimationEnd)
        {
            CreateExplosion();

            anim.SetTrigger("MoveFirst");
            sphere.onClick.RemoveAllListeners();
            sphere.onClick.AddListener(() => SecondMove());
        }
    }

    public void SecondMove()
    {
        if (isAnimationEnd)
        {
            CreateExplosion();

            anim.SetTrigger("MoveSecond");
            sphere.onClick.RemoveAllListeners();
            sphere.onClick.AddListener(() => PlayThird());
        }
    }

    public void PlayThird()
    {
        if (isAnimationEnd)
        {
            CreateExplosion();

            anim.SetTrigger("PlayThird");
            sphere.onClick.RemoveAllListeners();
            sphere.onClick.AddListener(() => PlayFinal());
        }

    }

    public void PlayFinal()
    {
        if (isAnimationEnd)
        {
            CreateExplosion();

            anim.SetTrigger("PlayFinal");
            sphere.onClick.RemoveAllListeners();
        }
    }

    void Update() {
        if (isFinish) {
            if (DataControl.control.isTutorialFinished == false)
            {
                DataControl.control.isTutorialFinished = true;
                HintManager.manager.amountFlankers = 5;
                HintManager.manager.amountFlashes = 5;
                HintManager.manager.amountTimeStops = 5;
                BankManager.bank = 500;

                DataControl.control.SaveAll();
            }

            nextScreen.SetActive(true);
            this.gameObject.SetActive(false);
        }

    }

    public void CreateExplosion()
    {
        AudioManager.manager.PlayFoundSound();

        GameObject exp = Instantiate(prefabExplosion, new Vector3(0, 0, 0), Quaternion.identity);
        exp.transform.SetParent(sphere.transform.parent);
        exp.GetComponent<RectTransform>().localPosition = new Vector3(sphere.transform.localPosition.x, sphere.transform.localPosition.y, 0);
        exp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);
    }

    public void Miss() {
        Debug.Log("miss");
        anim.SetTrigger("Miss");

        AudioManager.manager.PlayMissedSound();
    }

}
