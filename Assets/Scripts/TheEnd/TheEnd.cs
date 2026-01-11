using UnityEngine;

public class TheEnd : MonoBehaviour
{
    public Transform star1, star2, heart;

    private float rotationStar = 20f;
    private float swayAmount = 8f, swaySpeed = 1.8f;
    private float swayTimer = 0f, swayAngle = 0f;
    private Vector3 heartStartRotation;

    private void Start()
    {
        heartStartRotation = heart.eulerAngles;
    }

    private void Update()
    {
        star1.Rotate(0f, 0f, rotationStar * Time.deltaTime);
        star2.Rotate(0f, 0f, -rotationStar * Time.deltaTime);

        swayTimer += Time.deltaTime * swaySpeed;
        swayAngle = Mathf.Sin(swayTimer) * swayAmount;
        heart.rotation = Quaternion.Euler(0f, 0f, heartStartRotation.z + swayAngle);
    }
}
