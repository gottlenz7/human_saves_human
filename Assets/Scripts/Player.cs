using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private Rigidbody2D rd;
    private float speed = 2f;

    private float minSpeed = 0.1f;

    private Vector2 lastDirection = Vector2.down;
    public bool isDown => lastDirection.y < 0 && Input.GetKey(KeyCode.DownArrow);
    public bool isUp => lastDirection.y > 0 && Input.GetKey(KeyCode.UpArrow);
    public bool isLeft => lastDirection.x < 0 && Input.GetKey(KeyCode.LeftArrow);
    public bool isRight => lastDirection.x > 0 && Input.GetKey(KeyCode.RightArrow);

    private void Awake()
    {
        Instance = this;
        rd = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMoment();
    }

    private void HandleMoment()
    {
        Vector2 inputVector = GameInput.Instance.GetMomentVector();
        inputVector = inputVector.normalized;
        rd.MovePosition(rd.position + inputVector * (Time.fixedDeltaTime * speed));

        if (inputVector.magnitude > minSpeed) lastDirection = inputVector;
    }

    public bool IsDown()
    {
        return isDown;
    }

    public bool IsUp()
    {
        return isUp;
    }

    public bool IsLeft()
    {
        return isLeft;
    }

    public bool IsRight()
    {
        return isRight;
    }
}
