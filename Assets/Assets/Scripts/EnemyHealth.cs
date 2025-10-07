using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour{
    [SerializeField]
    public Stats stats;
    public float Health = 3f;
    public float hp;
    public AssetBundle myLoadedAssetBundle;
    public string[] scenePaths;

    void Start(){
        stats = GameObject.FindWithTag("MainPlayer").GetComponent<Stats>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("MainPlayer")){
            stats.hp -= 25f;
            Destroy(gameObject);
            if (stats.hp <= 0){
                SceneManager.LoadScene("Gameover", LoadSceneMode.Single);
            }
        }
        else if (other.CompareTag("Bullet")){
            Health -= 1;
            Destroy(other.gameObject);
            if (Health <= 0){
                Destroy(gameObject);
            }
        }
    }
}
