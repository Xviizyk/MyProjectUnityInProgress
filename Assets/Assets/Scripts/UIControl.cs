using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI hpText;
    public Stats stats;
    public GunChoosing GunChoosing;
    void Update()
    {
        if(GunChoosing.mode==1){
            ammoText.text = stats.ammo1.ToString();
        } else if(GunChoosing.mode==2){
            ammoText.text = stats.ammo2.ToString();
        }
        hpText.text = stats.hp.ToString();
        if(stats.ammo1==0){
            ammoText.text = "reloading";
        } else if (stats.ammo2 == 0){
            ammoText.text = "reloading";
        }
    }
}
