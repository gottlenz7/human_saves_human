using UnityEngine;

public class Hearts : MonoBehaviour 
{
    public static Hearts Instance { get; private set; }
    
    private SpriteRenderer heartsSprite;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        heartsSprite = GetComponent<SpriteRenderer>();
    }
}
