using UnityEngine;

public class WeaponMagic : MonoBehaviour
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
    [SerializeField] private ushort MaxAmmo;
    [SerializeField] private float ReloadTime;
    [SerializeField] private float BetweenFireTime;
    [SerializeField] private float ManaUse;

    [Header("Настройка патронов")]
    [SerializeField] private GameObject WeaponAmmoObject; 
    [SerializeField] private string WeaponAmmoName;

    public void privateUpdate(string currentEffect){
        
    }
}