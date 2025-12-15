using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerVisual : MonoBehaviour 
{
    public static PlayerVisual Instance { get; private set; }
    public bool isMan = false;
    public Animator animator;

    private const string IS_DOWN = "IsDown";
    private const string IS_UP = "IsUp";
    private const string IS_LEFT = "IsLeft";
    private const string IS_RIGHT = "IsRight";

    private void Awake()
    {
        Instance = this;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_DOWN, Player.Instance.IsDown);
        animator.SetBool(IS_UP, Player.Instance.IsUp);
        animator.SetBool(IS_LEFT, Player.Instance.IsLeft);
        animator.SetBool(IS_RIGHT, Player.Instance.IsRight);
    }
}
