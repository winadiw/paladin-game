using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;
    public AudioSource musicSource;

    private float soundVolume;
    private float musicVolume;

    public static SoundManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
	
    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void setSoundVolume(float value)
    {
        soundVolume = value;
    }
    public void setMusicVolume(float value)
    {
        musicVolume = value;
    }

    public void destroyMusic()
    {
        Destroy(this.gameObject);
    }
}
