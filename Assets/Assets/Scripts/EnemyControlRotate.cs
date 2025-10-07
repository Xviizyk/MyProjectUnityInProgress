using UnityEngine;

public class EnemyControlRotate : MonoBehaviour
{
    public float rotateAmplitude = 10f;
    public float rotateFrequency = 2f;
    public GameObject _object;
    private float _timer=1f;
    public Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        _timer += Time.deltaTime * rotateFrequency;
        float angle = Mathf.Sin(_timer*5) * rotateAmplitude;
        _rb.rotation = angle;
    }
}
