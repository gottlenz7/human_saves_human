using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public Rigidbody2D rb;
    private float speed = 2f;

    private float minSpeed = 0.1f;

    private Vector2 lastDirection = Vector2.down;
    public bool IsDown => lastDirection.y < 0 && Input.GetKey(KeyCode.DownArrow);
    public bool IsUp => lastDirection.y > 0 && Input.GetKey(KeyCode.UpArrow);
    public bool IsLeft => lastDirection.x < 0 && Input.GetKey(KeyCode.LeftArrow);
    public bool IsRight => lastDirection.x > 0 && Input.GetKey(KeyCode.RightArrow);

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

        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMoment();
    }

    private void HandleMoment()
    {
        Vector2 inputVector = GameInput.Instance.GetMomentVector();
        inputVector = inputVector.normalized;
        rb.MovePosition(rb.position + inputVector * (Time.fixedDeltaTime * speed));

        if (inputVector.magnitude > minSpeed) lastDirection = inputVector;
    }
}
