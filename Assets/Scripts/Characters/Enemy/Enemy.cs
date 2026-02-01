using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hearts, damage;
    public bool isHitting;

    [SerializeField] private Music music;

    public SpriteRenderer spriteRenderer;
    public Color originalColor;

    public bool hasDamage;


    protected virtual void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        music.SetMusic(gameObject, 0.7f, false);
    }

    protected virtual void Update()
    {
        if (isHitting) StartCoroutine(ChangeColor());

        if (hearts <= 0f)
        {
            EnemyManager.Instance.LoseEnemy();
            Destroy(gameObject);
        }

        StartCoroutine(Hit());
    }

    public IEnumerator Hit()
    {
        float distance = Vector3.Distance(transform.position, Player.Instance.transform.position);

        if (distance < 1f && !hasDamage)
        {   
            music.Play();

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
