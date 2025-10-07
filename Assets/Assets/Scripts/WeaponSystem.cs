using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public GameObject object0;
    public GameObject object1;
    public GameObject object2;
    public int mode = 0;
    public float rotationSpeed = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            object0.SetActive(true);
            object1.SetActive(false);
            object2.SetActive(false);
            mode = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)){
            object0.SetActive(false);
            object1.SetActive(true);
            object2.SetActive(false);
            mode = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)){
            object0.SetActive(false);
            object1.SetActive(false);
            object2.SetActive(true);
            mode = 2;
        }

        if (mode == 1){
            RotateWeapon(object1);
        } else if (mode == 2){
            RotateWeapon(object2);
        }
    }

    void RotateWeapon(GameObject weapon)
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 weaponPos = weapon.transform.position;
        Vector2 direction = mouseWorldPos - weaponPos;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Vector3 localScale = weapon.transform.localScale;
        if (angle > 90f || angle < -90f){
            localScale.y = -Mathf.Abs(localScale.y);
            localScale.x = -Mathf.Abs(localScale.x);
        } else {
            localScale.y = Mathf.Abs(localScale.y);
            localScale.x = -Mathf.Abs(localScale.x);
        }
        weapon.transform.localScale = localScale;
    }
}