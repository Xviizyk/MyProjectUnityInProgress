using UnityEngine;

public class EyesFollow : MonoBehaviour
{
    [Header("Глаза")]
    public SpriteRenderer leftEye;
    public SpriteRenderer rightEye;

    [Header("Смещение глаз по X")]
    public Vector2 eyesPosRight = new Vector2(-0.25f, -0.65f);
    public Vector2 eyesPosLeft  = new Vector2(-0.85f, -0.5f);

    [Header("Смещение глаз по Y")]
    public float minY = 0.1f;
    public float maxY = 0.3f;

    [Header("Ротация")]
    public float maxRotation = 20f;
    public float rotationSmooth = 10f;

    [Header("Плавность движения глаз")]
    public float moveSmooth = 10f;

    private Transform leftEyeTransform;
    private Transform rightEyeTransform;

    void Start()
    {
        leftEyeTransform = leftEye.transform;
        rightEyeTransform = rightEye.transform;
    }

    void Update()
    {
        if (leftEye == null || rightEye == null) return;

        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 objPos = transform.position;
        bool lookLeft = mouseWorldPos.x < objPos.x;

        leftEye.flipX = lookLeft;
        rightEye.flipX = lookLeft;

        float leftX = lookLeft ? eyesPosLeft.x : eyesPosRight.x;
        float rightX = lookLeft ? eyesPosLeft.y : eyesPosRight.y;

        float screenHeight = Screen.height;
        float mouseY01 = Mathf.Clamp01(Input.mousePosition.y / screenHeight);
        float eyeY = Mathf.Lerp(minY, maxY, mouseY01);
        leftEyeTransform.localPosition = Vector3.Lerp(leftEyeTransform.localPosition,
            new Vector3(leftX, eyeY, leftEyeTransform.localPosition.z),
            moveSmooth * Time.deltaTime);
        rightEyeTransform.localPosition = Vector3.Lerp(rightEyeTransform.localPosition,
            new Vector3(rightX, eyeY, rightEyeTransform.localPosition.z),
            moveSmooth * Time.deltaTime);

        Vector2 dir = (mouseWorldPos - objPos).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        angle = Mathf.Clamp(angle, -maxRotation, maxRotation);

        float finalAngle = lookLeft ? -angle : angle;

        leftEyeTransform.localRotation = Quaternion.Lerp(leftEyeTransform.localRotation,
            Quaternion.Euler(0, 0, finalAngle), rotationSmooth * Time.deltaTime);
        rightEyeTransform.localRotation = Quaternion.Lerp(rightEyeTransform.localRotation,
            Quaternion.Euler(0, 0, finalAngle), rotationSmooth * Time.deltaTime);
    }
}
