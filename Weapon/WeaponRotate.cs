using UnityEngine;

public class WeaponRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Camera mainCamera;
    public GameObject currentWeapon;

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - currentWeapon.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        currentWeapon.transform.rotation = Quaternion.Lerp(currentWeapon.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        Vector3 _localScale = currentWeapon.transform.localScale;

        if (angle > 90 || angle < -90)
        {
            _localScale.y = -1f;
        }
        else
        {
            _localScale.y = 1f;
        }

        currentWeapon.transform.localScale = _localScale;
    }
}