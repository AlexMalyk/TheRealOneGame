using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    public static AudioManager manager;

    public AudioClip clickAudio;
    public AudioClip positiveAudio;
    public AudioClip negativeAudio;
    public AudioClip lastSecondsAudio;

    AudioSource source;

    void Awake()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if (manager != null)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        source = gameObject.GetComponent<AudioSource>();
        source.playOnAwake = false;
	}

    public void PlayClickSound()
    {
        if (SettingsManager.manager.isAudioOn)
        {
            source.PlayOneShot(clickAudio);
        }
    }
    public void PlayPositiveSound()
    {
        if (SettingsManager.manager.isAudioOn)
        {
            source.PlayOneShot(positiveAudio);
        }
    }
    public void PlayNegativeSound()
    {
        if (SettingsManager.manager.isAudioOn)
        {
            source.PlayOneShot(negativeAudio);
        }
    }
}
