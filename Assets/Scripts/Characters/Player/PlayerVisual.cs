using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.Rendering;
using Move;

public class PlayerVisual : MonoBehaviour 
{
    public static PlayerVisual Instance { get; private set; }
    public bool isMan = false, haveWeapon = false;
    public Animator animator;
    private MovableSprite movableSprite;

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
}

    private void Update()
    {
        movableSprite.SetDirection(Player.Instance.IsRight, Player.Instance.IsLeft, Player.Instance.IsDown, Player.Instance.IsUp);
        movableSprite.SetAnimator(animator);
    }
}
