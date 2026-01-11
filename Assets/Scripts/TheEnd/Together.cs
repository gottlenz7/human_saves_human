using UnityEngine;

public class Together : MonoBehaviour
{
    public Sprite sprite1, sprite2;

    private SpriteRenderer together;

    private void Start()
    {
        together = GetComponent<SpriteRenderer>();

        if (PlayerVisual.Instance.isMan)
            together.sprite = sprite1;
        else
            together.sprite = sprite2;
    }
}
