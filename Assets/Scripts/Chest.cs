using UnityEngine;

public class Chest : MonoBehaviour 
{
    public Sprite openedChest, closedChest;
    public SpriteRenderer getItem, brush, flute;
    public bool isFlute = false;

    private SpriteRenderer chestSpriteRenderer;
    private bool isOpen = false, isPlayerNear = false;

    private void Start()
    {
        chestSpriteRenderer = GetComponent<SpriteRenderer>();
        getItem.enabled = false;
        brush.enabled = false;
        flute.enabled = false;
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyUp(KeyCode.C))
        {
            if (!isOpen)
                OpenChest();
            else
                CloseChest();
        }
        Debug.Log(PlayerVisual.Instance.isMan);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerT"))
            isPlayerNear = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerT"))
            isPlayerNear = false;
    }

    private void OpenChest()
    {
        chestSpriteRenderer.sprite = openedChest;
        isOpen = true;
        getItem.enabled = true;

        Debug.Log(PlayerVisual.Instance.isMan);

        if (PlayerVisual.Instance.isMan)
        {
            flute.enabled = true;
            isFlute = true;
        }
        else
        {
            brush.enabled = true;
            isFlute = false;
        }

        Item.Instance.ShowItem(isFlute);
    }

    private void CloseChest()
    {
        chestSpriteRenderer.sprite = closedChest;
        isOpen = false;
        getItem.enabled = false;
        brush.enabled = false;
        flute.enabled = false;
    }
}
