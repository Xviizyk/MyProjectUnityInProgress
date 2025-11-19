using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponRanged _weaponRanged;
    [SerializeField] private WeaponMelee _weaponMelee;
    [SerializeField] private WeaponThrowing _weaponThrowing;
    [SerializeField] private WeaponMagic _weaponMagic;
    [SerializeField] private WeaponPotion _weaponPotion;
    [SerializeField] private WeaponRotate _weaponRotate;

    [SerializeField] private WeaponType currentWeaponType = WeaponType.Melee;
    [SerializeField] private Effect currentEffect = Effect.None;

    public GameObject currentWeapon;
    

    public enum WeaponType
    {
        Ranged,
        Melee,
        Throwing,
        Magic,
        Potion
    }

    public enum Effect
    {
        None,
        FireDamage,
        Slowness,
        Poison,
        Stun,
        Vampirism,
        Blindness
    }
    
    private void Update()
    {
        switch(currentWeaponType)
        {
            case WeaponType.Ranged:
                _weaponRanged?.privateUpdate(currentEffect.ToString());
                break;
            case WeaponType.Melee:
                _weaponMelee?.privateUpdate(currentEffect.ToString());
                break;
            case WeaponType.Throwing:
                _weaponThrowing?.privateUpdate(currentEffect.ToString());
                break;
            case WeaponType.Magic:
                _weaponMagic?.privateUpdate(currentEffect.ToString());
                break;
            case WeaponType.Potion:
                _weaponPotion?.privateUpdate(currentEffect.ToString());
                break;
        }

        _weaponRotate.currentWeapon = currentWeapon;
    }
}