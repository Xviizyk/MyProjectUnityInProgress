using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [Header("Характеристики врага")]
    public float speed = 3f;
    public float detectionRadius = 5f;

    [Header("Покачивание (вращение)")]
    public float rotateAmplitude = 10f;   // амплитуда вращения
    public float rotateFrequency = 2f;    // частота вращения
    private float _timer = 1f;
    private Rigidbody2D _rb;

    private Transform player;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("MainPlayer");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null) return;
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= detectionRadius)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );
            if (_rb != null){
                _timer += Time.deltaTime * rotateFrequency;
                float angle = Mathf.Sin(_timer * 5f) * rotateAmplitude;
                _rb.rotation = angle;
            }
        }
        if (spriteRenderer != null)
        {
            if (player.position.x > transform.position.x)
                spriteRenderer.flipX = true;
            else
                spriteRenderer.flipX = false;
        }
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
