using UnityEngine;

public class GunChoosing : MonoBehaviour
{
    public GameObject object0;
    public GameObject object1;
    public GameObject object2;
    public int mode = 0;

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
    }
}
