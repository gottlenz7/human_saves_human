using System;
using System.Collections;
using Move;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float hearts, damage;
    public bool isHitting;

    public SpriteRenderer spriteRenderer;
    public Color originalColor;

    public bool hasDamage;
  

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        if (isHitting) StartCoroutine(ChangeColor());
        if (hearts <= 0f) Destroy(gameObject);
        StartCoroutine(Hit());
    }

    public IEnumerator Hit()
    {
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), 
            new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y));

        if (distance < 1f && !hasDamage)
        {
            PlayerVisual.Instance.isHitting = true;
            PlayerVisual.Instance.hearts -= damage;
            hasDamage = true;

            yield return new WaitForSeconds(5f);

            hasDamage = false;
        }
    }

    public IEnumerator ChangeColor()
    {
        spriteRenderer.color = new Color(1f, 0f, 0f, 0.5f);

        yield return new WaitForSeconds(0.5f);

        spriteRenderer.color = originalColor;
        isHitting = false;
    }
}
