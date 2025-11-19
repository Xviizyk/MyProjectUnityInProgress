using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class WeaponMelee : MonoBehaviour
{
    [Header("Визуал")]
    [SerializeField] private GameObject WeaponObject;
    [SerializeField] private Transform attackPoint;

    [Header("Конфигурация")]
    [Range(0, 2)]
    [SerializeField] private float PlayerSpeedmultiplier;
    
    [Range(0, 100)]
    [SerializeField] private double Damage;
    
    [Range(0, 5)]
    [SerializeField] private float attackRange;
    
    [Range(0, 10)]
    [SerializeField] private float attackRate;
    
    [Range(0, 100)]
    [SerializeField] private float ManaUse;

    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float nextAttackTime;
    [SerializeField] InputActionReference attackInput;

    private void OnEnable()
    {
        attackInput.action.Enable();
    }

    private void OnDisable()
    {
        attackInput.action.Disable();
    }
    
    public void privateUpdate(string currentEffect)
    {
        if (attackInput.action.IsPressed())
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        if (Time.time < nextAttackTime)
        {
            return;
        }
        Attack();
        nextAttackTime = Time.time + attackRate;
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider enemy in hitEnemies)
        {
            //uzhe tretiy script i bez etovo koda...
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}