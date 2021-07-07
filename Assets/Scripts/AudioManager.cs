using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    void Awake()
    {
        instance = this;
    }

    public AudioClip bugSquashSound;

    private AudioSource[] audioSources; 


    // Start is called before the first frame update
    void Start()
    {
        audioSources = gameObject.GetComponents<AudioSource>();
        audioSources[0].volume = GameData.sound;
        audioSources[1].volume = GameData.music;
    }

    public void PlaySquashSound()
    {
        if (audioSources == null)
        {
            audioSources = GetComponents<AudioSource>();
            audioSources[0].volume = GameData.sound;
            audioSources[0].PlayOneShot(bugSquashSound);
        }
        else
        {
            audioSources[0].volume = GameData.sound;
            audioSources[0].PlayOneShot(bugSquashSound);
        }
    }

    public void PlayClassicBGM()
    {
        if (audioSources == null)
        {
            audioSources = GetComponents<AudioSource>();
            audioSources[1].volume = GameData.music;
            audioSources[1].Play();
        }
        else
        {
            audioSources[1].volume = GameData.music;
            audioSources[1].Play();
        }
    }

    public void PlayLimitedBGM()
    {
        if (audioSources == null)
        {
            audioSources = GetComponents<AudioSource>();
            audioSources[1].volume = GameData.music;
            audioSources[1].Play();
        }
        else
        {
            audioSources[1].volume = GameData.music;
            audioSources[1].Play();
        }
    }

    public void PlayMasteryBGM()
    {
        if (audioSources == null)
        {
            audioSources = GetComponents<AudioSource>();
            audioSources[1].volume = GameData.music;
            audioSources[1].Play();
        }
        else
        {
            audioSources[1].volume = GameData.music;
            audioSources[1].Play();
        }
    }

    public void PauseBGM()
    {
        audioSources[1].Pause();
    }

    public void PlayBGM()
    {
        audioSources[1].Play();
    }

    public void StopBGM()
    {
        audioSources[1].Stop();
    }
}
