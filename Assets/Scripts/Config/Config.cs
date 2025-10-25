using UnityEngine;

public class Config : MonoBehaviour
{
    public float mainCharacterHealth = 100f;
    public float GlobalBulletSpeed = 30f;
    public float AK47_FireRate = 0.1f;
    public float AK47_ReloadTime = 1.5f;
    public int AK47_MaxAmmo = 30;
    public float AK47_BaseSpread = 7f;
    public GameObject AK47_BulletPrefab;
    public float Shotgun_FireRate = 0.75f;
    public float Shotgun_ReloadTime = 3f;
    public int Shotgun_MaxAmmo = 6;
    public int Shotgun_PelletCount = 6;
    public float Shotgun_MaxSpread = 15f;
    public float Shotgun_XOffset = -0.1f;
    public GameObject Shotgun_BulletPrefab;
}