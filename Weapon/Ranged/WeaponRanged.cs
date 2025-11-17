using UnityEngine;
using System.Collections;

public class WeaponRanged : MonoBehaviour
{
    [Header("Префабы оружия")]
    public Weapon w;
    [SerializeField] private GameObject WeaponObject;
    [SerializeField] private Transform muzzlePoint;

    [Header("Конфигурация")]
    [SerializeField] private ulong PlayerSpeedmultiplier;
    [SerializeField] private ulong Damage;
    [SerializeField] private ulong SplashDamage;
    [SerializeField] private ulong SplashRange;
    [SerializeField] private ushort MaxAmmo; //max ammo
    [SerializeField] private float ReloadTime; //seconds
    [SerializeField] private float BetweenFireTime;
    [SerializeField] private float ManaUse;
    [SerializeField] private float fireRate;

    [Header("Настройка патронов")]
    [SerializeField] private GameObject WeaponAmmoObject; 
    [SerializeField] private string WeaponAmmoName;

    [Header("Состояния")]
    [SerializeField] private int currentAmmo;
    [SerializeField] private bool reloading = false;
    [SerializeField] private float betweenShot = 0f;

    public void privateUpdate()
    {
        if (reloading)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartReload();
            return;
        }

        if (Input.GetMouseButton(0))
        {
            TryShoot();
        }
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
                StartReload();
            }
            return;
        }
        
        Shoot();
        nextShotTime = Time.time + fireRate;
    }

    void Shoot()
    {
        currentAmmo--;

        GameObject bullet = Instantiate(projectilePrefab, muzzlePoint.position, muzzlePoint.rotation);

        Projectile proj = bullet.GetComponent<Projectile>();
        if (proj)
        {
            proj.damage = damage;
            proj.splashDamage = splashDamage;
            proj.splashRange = splashRange;
        }
    }

    void StartReload()
    {
        if (!isReloading)
        {
            StartCoroutine(Reload());
        }
    }
    
    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}