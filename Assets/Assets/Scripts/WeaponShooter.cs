using UnityEngine;
using System.Collections;

public class WeaponShooter : MonoBehaviour
{
    [Header("References")]
    public WeaponSystem weaponSwitcher;
    public GameObject bulletPrefab;
    public Transform firePoint1;
    public Transform firePoint2;
    public Stats stats;
    public GunChoosing GunChoosing;

    private Rigidbody2D _rb;
    public Movement _movement;

    [Header("Shooting Settings")]
    public float bulletSpeed = 10f;
    private bool _shooting = false;
    private bool _isShootingCoroutineRunning = false;

    [Header("Reload Settings")]
    public float reloadTime1 = 1.5f;
    public float reloadTime2 = 3f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _movement = GetComponent<Movement>();
    }

    void Update()
    {
        if (GunChoosing.mode == 0) return;

        if (Input.GetMouseButtonDown(0))
        {
            _shooting = true;
            if (!_isShootingCoroutineRunning)
            {
                StartCoroutine(ShootingLoop());
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _shooting = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator ShootingLoop()
    {
        _isShootingCoroutineRunning = true;

        while (_shooting)
        {
            if (GunChoosing.mode == 1)
            {
                if (stats.ammo1 > 0)
                {
                    GameObject bullet = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
                    Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();

                    // базовый разброс ±3°, удваивается в прыжке
                    float baseSpread = 7f;
                    float spread = baseSpread;

                    if (!_movement.isGrounded)
                        spread *= 3f;

                    Vector2 dir = Quaternion.Euler(0, 0, Random.Range(-spread, spread)) * firePoint1.right;
                    rbBullet.linearVelocity = dir * bulletSpeed;

                    stats.ammo1--;
                    Destroy(bullet, 5f);

                    yield return new WaitForSeconds(0.1f);
                }
                else
                {
                    yield return StartCoroutine(Reload());
                }
            }
            else if (GunChoosing.mode == 2)
            {
                if (stats.ammo2 > 0)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        GameObject bullet = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
                        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();

                        float spread = Random.Range(-15f, 15f);
                        Vector2 dir = Quaternion.Euler(0, 0, spread) * firePoint2.right;
                        rbBullet.linearVelocity = dir * bulletSpeed;

                        Destroy(bullet, 5f);
                    }

                    stats.ammo2--;
                    yield return new WaitForSeconds(0.75f);
                }
                else
                {
                    yield return StartCoroutine(Reload());
                }
            }
        }

        _isShootingCoroutineRunning = false;
    }

    IEnumerator Reload()
    {
        if (GunChoosing.mode == 1)
        {
            stats.ammo1 = 0;
            yield return new WaitForSeconds(reloadTime1);
            stats.ammo1 = 30;
        }
        else if (GunChoosing.mode == 2)
        {
            stats.ammo2 = 0;
            yield return new WaitForSeconds(reloadTime2);
            stats.ammo2 = 6;
        }
    }
}
