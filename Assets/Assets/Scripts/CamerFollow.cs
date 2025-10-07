using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target;        // объект, за которым следим
    public float followSpeed = 5f;  // скорость сглаживания
    public float fixedZ = 5f;       // фиксированная координата Z

    void Update()
    {
        if (target != null)
        {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, fixedZ);

            transform.position = Vector3.Lerp(
                transform.position,
                targetPos,
                followSpeed * Time.deltaTime
            );
        }
    }
}
