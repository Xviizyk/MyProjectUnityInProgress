using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI ammoText;
    [SerializeField] public TextMeshProUGUI hpText;
    [SerializeField] public Stats stats;
    [SerializeField] public WeaponShooter GunChoosing;
    [SerializeField] public GameObject AmmoAndHp;

    void Update()
    {
        WeaponShooter.WeaponMode currentMode = GunChoosing._currentMode;
        float currentAmmo = 0f;
        float maxAmmo = 1f;
        if(currentMode == WeaponShooter.WeaponMode.AK47){
            AmmoAndHp.SetActive(true);
            currentAmmo = stats.ammo1;
            maxAmmo = GunChoosing.config.AK47_MaxAmmo;
        } else if(currentMode == WeaponShooter.WeaponMode.Shotgun){
            AmmoAndHp.SetActive(true);
            currentAmmo = stats.ammo2;
            maxAmmo = GunChoosing.config.Shotgun_MaxAmmo;
        } else{
            AmmoAndHp.SetActive(false);
            currentAmmo = 0f;
            maxAmmo = 0f;
        }
        if(currentMode == WeaponShooter.WeaponMode.Melee){
            ammoText.text = "";
        } else if(currentAmmo == 0 && maxAmmo > 0){
            ammoText.text = "ПЕРЕЗАРЯДКА...";
        }
        else{
            ammoText.text = currentAmmo.ToString();
        }
        hpText.text = stats.hp.ToString();
    }
}