using UnityEngine;

public class Hearts : MonoBehaviour
{
    public static Hearts Instance { get; private set; }

    [SerializeField] private Sprite[] heartSprites; 

    private SpriteRenderer heartsSprite;
    private float hearts;

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

    private void Update()
    {
        UpdateHeartsSprite();
    }

    private void UpdateHeartsSprite()
    {
        hearts = PlayerVisual.Instance.hearts;

        int spriteIndex = Mathf.Clamp(Mathf.RoundToInt(hearts * 2), 0, heartSprites.Length - 1);

        heartsSprite.sprite = heartSprites[spriteIndex];
    }
}
