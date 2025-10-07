using UnityEngine;

public class Stats : MonoBehaviour
{
    public float ammo1;
    public float ammo2;
    public EnemyHealth EnemyHealth;
    public float hp;

    void Start(){
        ammo1 = 30f;
        ammo2 = 6f;
        hp = 100f;
    }
}
