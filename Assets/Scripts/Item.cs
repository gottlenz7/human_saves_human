using UnityEngine;

public class Item : MonoBehaviour
{
    public static Item Instance {  get; private set; }
    public Sprite brush, flute;

    private SpriteRenderer item;

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
        item = GetComponent<SpriteRenderer>();
    }

    public void ShowItem(bool isFlute)
    {
        if (isFlute)
            item.sprite = flute;
        else
            item.sprite = brush;
    }
}
