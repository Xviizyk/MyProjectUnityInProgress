using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class WeaponThrowing : MonoBehaviour
{
    [Header("Префабы оружия")]
    [SerializeField] private GameObject WeaponObject;
    [SerializeField] private Transform throwPoint;

    [Header("Конфигурация")]
    [Range(0, 2)]
    [SerializeField] private float PlayerSpeedmultiplier;
    [Range(0, 100)]
    [SerializeField] private double Damage;
    [Range(0, 100)]
    [SerializeField] private double SplashDamage;
    [Range(0, 10)]
    [SerializeField] private float SplashRange;
    [Range(0, 250)]
    [SerializeField] private byte MaxAmmo;
    [Range(0, 10)]
    [SerializeField] private float ReloadTime;
    [Range(0, 100)]
    [SerializeField] private float ManaUse;
    [Range(0, 10)]
    [SerializeField] private float throwRate;
    [Range(0, 50)]
    [SerializeField] private float throwForce;

    [Header("Настройка снаряда")]
    [SerializeField] private GameObject WeaponAmmoObject; 
    [SerializeField] private string WeaponAmmoName;

    [Header("Состояния")]
    [SerializeField] private int currentAmmo;
    [SerializeField] private bool reloading;
    [SerializeField] private float nextThrowTime;
    // [SerializeField] private string 

    private bool isReloading;
    
    [Header("Инпут система")]
    [SerializeField] InputActionReference throwInput;
    [SerializeField] InputActionReference reloadInput;

    private void OnEnable()
    {
        reloadInput.action.performed += OnReloadPerformed;
        reloadInput.action.Enable();
        throwInput.action.Enable();
    }

    private void OnDisable()
    {
        reloadInput.action.performed -= OnReloadPerformed;
        reloadInput.action.Disable();

        throwInput.action.Disable();
    }
    
    public void privateUpdate(string currentEffect)
    {
        if (isReloading)
        {
            return;
        }

        if (throwInput.action.IsPressed())
        {
            TryThrow();
        }
    }
    
    private void OnReloadPerformed(InputAction.CallbackContext context)
    {
        StartCoroutine(Reload());
    }

    void TryThrow()
    {
        if (Time.time < nextThrowTime)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            if (!isReloading){
                StartCoroutine(Reload());
            }
            return;
        }
        
        Throw();
        nextThrowTime = Time.time + throwRate;
    }

    void Throw()
    {
        currentAmmo--;
        
        GameObject projectile = Instantiate(WeaponAmmoObject, throwPoint.position, throwPoint.rotation);
        
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);
        }

        ProjectileThrowing proj = projectile.GetComponent<ProjectileThrowing>();
        if (proj)
        {
            proj.damage = Damage;
            proj.splashDamage = SplashDamage;
            proj.splashRange = SplashRange;
        }
    }
    
    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(ReloadTime);
        currentAmmo = MaxAmmo;
        isReloading = false;
    }
}