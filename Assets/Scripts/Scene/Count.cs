using UnityEngine;

public class Count : MonoBehaviour
{
    public static Count Instance { get; private set; }

    public int enemiesCount;

    [SerializeField] private Music music;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        music.NewMusic(gameObject, 0.5f, true);
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
