using UnityEngine;

public class Music
{
    private AudioSource audioSource;

    public Music(GameObject gameObject, AudioClip clip, float volume, bool loop)
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
