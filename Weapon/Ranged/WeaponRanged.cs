using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class WeaponRanged : MonoBehaviour
{
    [Header("Префабы оружия")]
    [SerializeField] private GameObject WeaponObject;
    [SerializeField] private Transform muzzlePoint;

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
    [SerializeField] private float fireRate;
    [Range(0, 25)]
    [SerializeField] private byte spread;

    [Header("Настройка патронов")]
    [SerializeField] private GameObject WeaponAmmoObject; 
    [SerializeField] private string WeaponAmmoName;
    [SerializeField] private float bulletLife;
    [SerializeField] private float bulletSpeed;

    [Header("Состояния")]
    [SerializeField] private int currentAmmo;
    [SerializeField] private bool reloading;
    [SerializeField] private float nextShotTime;

    private bool isReloading;
    
    [Header("Инпут система")]
    [SerializeField] InputActionReference shootInput;
    [SerializeField] InputActionReference reloadInput;

    private void OnEnable()
    {
        reloadInput.action.performed += OnReloadPerformed;
        reloadInput.action.Enable();
        shootInput.action.Enable();
    }

    private void OnDisable()
    {
        reloadInput.action.performed -= OnReloadPerformed;
        reloadInput.action.Disable();

        shootInput.action.Disable();
    }
    
    public void privateUpdate(string currentEffect)
    {
        if (isReloading)
        {
            return;
        }

        if (shootInput.action.IsPressed())
        {
            TryShoot();
        }
    }
    
    private void OnReloadPerformed(InputAction.CallbackContext context)
    {
        StartCoroutine(Reload());
    }

    void TryShoot()
    {
        if (Time.time < nextShotTime)
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
        
        Shoot();
        nextShotTime = Time.time + fireRate;
    }

    void Shoot()
    {
        currentAmmo--;
        GameObject projectilePrefab = WeaponAmmoObject;
        double damage = Damage;
        double splashDamage = SplashDamage;
        float splashRange = SplashRange;
        GameObject bullet = Instantiate(projectilePrefab, muzzlePoint.position, muzzlePoint.rotation);
        ProjectileRanged proj = bullet.GetComponent<ProjectileRanged>();
        if (proj)
        {
            proj.lifetime = bulletLife;
            proj.speed = bulletSpeed;
            proj.damage = damage;
            proj.splashDamage = splashDamage;
            proj.splashRange = splashRange;
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