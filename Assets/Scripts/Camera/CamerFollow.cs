using UnityEngine;

public class CamerFollow : MonoBehaviour
{
    [SerializeField] private Transform _object1;
    public float followSpeed = 5f;
    void Update(){
        Vector3 targetPos = new Vector3(_object1.position.x, _object1.position.y, -2f);
        transform.position = Vector3.Lerp(transform.position,targetPos,followSpeed * Time.deltaTime);
    }
}
