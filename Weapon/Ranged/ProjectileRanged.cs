using UnityEngine;
using System.Collections;

public class ProjectileRanged : MonoBehaviour
{
    [Header("Параметры урона")]
    public double damage;
    public double splashDamage;
    public float splashRange;

    [Header("Конфигурация снаряда")]
    public float speed;
    public float lifetime;
    [SerializeField] private LayerMask targets;

    private void Start()
    {
        StartCoroutine(MoveForward());
        Destroy(gameObject, lifetime);
    }

    IEnumerator MoveForward()
    {
        while (true)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ApplyDamage(other.gameObject, damage);
        if (splashDamage > 0 && splashRange > 0)
        {
            splashDamageReceive();
        }
        Destroy(gameObject);
    }

    private void ApplyDamage(GameObject target, double amount)
    {
        //Nu tut karoche mne len pisat hot chto libo
    }

    private void splashDamageReceive()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, splashRange, targets);
        foreach (var hit in hitColliders)
        {
            ApplyDamage(hit.gameObject, splashDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (splashRange > 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, splashRange);
        }
    }
}