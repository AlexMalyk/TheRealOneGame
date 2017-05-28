using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour
{
    
    private IEnumerator Start()
    {
        LocalizationManager.manager.SetLanguage();
        while (!LocalizationManager.manager.GetIsReady())
        {
            yield return null;
        }

        SceneManager.LoadScene("Test");
        
    }

}