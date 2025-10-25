using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] public float ammo1;
    [SerializeField] public float ammo2;
    [SerializeField] public float hp;
    [SerializeField] private Config config;
    void Start(){
        ammo1 = config.AK47_MaxAmmo;
        ammo2 = config.Shotgun_MaxAmmo;
        hp = config.mainCharacterHealth;
    }
}