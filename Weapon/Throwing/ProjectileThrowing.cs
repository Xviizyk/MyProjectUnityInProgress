using UnityEngine;

public class ProjectileThrowing : MonoBehaviour
{
    [HideInInspector] public double damage;
    [HideInInspector] public double splashDamage;
    [HideInInspector] public float splashRange;

    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private LayerMask hitLayers;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (splashRange > 0)
        {
            Explode();
        }
        else
        {
            ApplyDamage(collision.gameObject, damage);
        }

        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, splashRange, hitLayers);
        foreach (var hitCollider in hitColliders)
        {
            ApplyDamage(hitCollider.gameObject, splashDamage);
        }
    }

    private void ApplyDamage(GameObject target, double damageAmount)
    {
        //dopustim...
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, splashRange);
    }
}