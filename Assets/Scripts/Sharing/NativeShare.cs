using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;

/*
 * https://github.com/ChrisMaire/unity-native-sharing
 */

public class NativeShare : MonoBehaviour
{
    string ScreenshotName = "screenshot.png";
    string kMessage = "Check out my The Real One score! https://play.google.com/store/apps/details?id=com.ogs.therealone";


    public GameObject HiddenGameObject;
    public GameObject ShowedGameObject;
    public GameObject SharingCanvas;

    public void ShareScreenshotWithText()
    {
        string screenShotPath = Application.persistentDataPath + "/" + ScreenshotName;
        if (File.Exists(screenShotPath)) File.Delete(screenShotPath);

        StartCoroutine(screenshotCaptureCoroutine(SharingCanvas.GetComponent<Animator>(), screenShotPath, kMessage));
    }

    IEnumerator screenshotCaptureCoroutine(Animator anim, string screenShotPath, string text) {
        HiddenGameObject.SetActive(false);
        ShowedGameObject.SetActive(true);

        Application.CaptureScreenshot(ScreenshotName);
        yield return CoroutineUtil.WaitForRealSeconds(.01f);

        ShowedGameObject.SetActive(false);
        HiddenGameObject.SetActive(true);

        while (!File.Exists(screenShotPath))
        {
            yield return new WaitForSeconds(.05f);
        }

        Share(text, screenShotPath, "");
    }


    //CaptureScreenshot runs asynchronously, so you'll need to either capture the screenshot early and wait a fixed time
    //for it to save, or set a unique image name and check if the file has been created yet before sharing.
    IEnumerator delayedShare(string screenShotPath, string text)
    {
        while (!File.Exists(screenShotPath))
        {
            yield return new WaitForSeconds(.05f);
        }

        Share(text, screenShotPath, "");
    }

    public void Share(string shareText, string imagePath, string url, string subject = "")
    {
#if UNITY_ANDROID
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + imagePath);
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
        intentObject.Call<AndroidJavaObject>("setType", "image/png");

        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareText);

        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, subject);
        currentActivity.Call("startActivity", jChooser);
#endif
    }
}
