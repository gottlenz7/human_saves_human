using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    public GameObject outline1, outline2;

    private SpriteRenderer spriteOutline1, spriteOutline2;

    private void Start()
    {
        spriteOutline1 = outline1.GetComponent<SpriteRenderer>();
        spriteOutline2 = outline2.GetComponent<SpriteRenderer>();

        SetAlpha(0, spriteOutline1);
        SetAlpha(0, spriteOutline2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SetAlpha(1, spriteOutline1);
            SetAlpha(0, spriteOutline2);

            PlayerVisual.Instance.isMan = false;
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            SetAlpha(0, spriteOutline1);
            SetAlpha(1, spriteOutline2);

            PlayerVisual.Instance.isMan = true;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerVisual.Instance.animator.SetBool("isMan", PlayerVisual.Instance.isMan);
            Debug.Log(PlayerVisual.Instance.isMan);
        }
    }

    private void SetAlpha(float alpha, SpriteRenderer sprite)
    {
        if (sprite != null)
        {
            Color color = sprite.color;
            color.a = alpha;
            sprite.color = color;
        }
    }
}
