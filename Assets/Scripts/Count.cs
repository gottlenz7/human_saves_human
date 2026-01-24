using UnityEngine;

public class Count : MonoBehaviour
{
    public static Count Instance { get; private set; }

    public int enemiesCount;
    public AudioClip music;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.volume = 0.5f;

        audioSource.Play();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
