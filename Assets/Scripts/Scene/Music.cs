using System;
using UnityEngine;

[Serializable]
public class Music
{
    public AudioClip clip;

    private AudioSource audioSource;
    
    public void NewMusic(GameObject gameObject, float volume, bool loop)
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
    }

    public void Play()
    {
        audioSource.Play();
    }
}
