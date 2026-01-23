using UnityEngine;
using Move;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerVisual : MonoBehaviour 
{
    public static PlayerVisual Instance { get; private set; }

    public bool isMan = false, haveWeapon = false, isAttacking = false, isHitting = false;
    public float hearts = 3f;
    public Animator animator;

    private MovableSprite movableSprite;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool hasDamage = false;

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

        animator = GetComponent<Animator>();
        movableSprite = new MovableSprite();

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        movableSprite.SetDirection(Player.Instance.IsRight, Player.Instance.IsLeft, Player.Instance.IsDown, Player.Instance.IsUp);
        movableSprite.SetAnimator(animator);

        AttackAnimation();
        if (isHitting) StartCoroutine(ChangeColor());
        if (Input.GetKeyDown(KeyCode.C)) StartCoroutine(Eat());

        if (hearts <= 0f)
        {
            Player.Instance.OnDestroy();
            EnemyManager.Instance.OnDestroy();
            Item.Instance.OnDestroy();

            SceneManager.LoadScene(12);
        }
    }

    private void AttackAnimation()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(AttackRoutine());
        }

        IEnumerator AttackRoutine()
        {
            isAttacking = true;
            animator.SetBool("Attack", true);
            StartCoroutine(Hit());

            yield return null;

            AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(state.length);

            animator.SetBool("Attack", false);

            isAttacking = false;
        }
    }

    IEnumerator Eat()
    {
        if (EnemyManager.Instance.applesCount > 0 && hearts < 3)
        {
            hearts += 1f;
            hearts = Mathf.Min(hearts, 3);

            EnemyManager.Instance.LoseApples();

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Hit()
    {
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in allEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < 1.5f && isAttacking && !hasDamage)  
            {
                enemy.isHitting = true;

                yield return new WaitForSeconds(0.2f);

                enemy.hearts -= 0.5f;
                hasDamage = true;

                yield return new WaitForSeconds(0.5f);

                hasDamage = false;
            }
        }
    }

    IEnumerator ChangeColor()
    {
        spriteRenderer.color = new Color(1f, 0f, 0f, 0.5f);
        
        yield return new WaitForSeconds(0.5f);

        spriteRenderer.color = originalColor;
        isHitting = false;
    }
}
