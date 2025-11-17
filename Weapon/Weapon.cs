using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponRanged _weaponRanged;
    public WeaponMelee _weaponMelee;
    public WeaponThrowing _weaponThrowing;
    public WeaponMagic _weaponMagic;
    public WeaponPotion _weaponPotion;
    [SerializeField] private WeaponType currentWeapon = WeaponType.melee;

    public enum WeaponType
    {
        ranged,
        melee,
        throwing,
        magic,
        potion
    }

    public enum Effect
    {
        none,
        fireDamage,
        slowness,
        poison,
        stun,
        vampirism,
        blindness
    }
    
    private void Update()
    {
        switch(currentWeapon)
        {
            case WeaponType.ranged:
                _weaponRanged?.privateUpdate();
                break;
            case WeaponType.melee:
                _weaponMelee?.privateUpdate();
                break;
            case WeaponType.throwing:
                _weaponThrowing?.privateUpdate();
                break;
            case WeaponType.magic:
                _weaponMagic?.privateUpdate();
                break;
            case WeaponType.potion:
                _weaponPotion?.privateUpdate();
                break;
        }
    }
}