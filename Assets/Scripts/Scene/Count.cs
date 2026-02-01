using UnityEngine;

public class Count : MonoBehaviour
{
    public static Count Instance { get; private set; }

    public int enemiesCount;
    public AudioClip clip;

    private Music music;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        music = new Music(gameObject, clip, 0.5f, true);
    }

    private void Start()
    {
        music.Play();
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
