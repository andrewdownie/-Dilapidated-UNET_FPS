using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : HUD_Base<HUD> {

    /////
    ///// GUI Element References
    /////
    [SerializeField]
    protected Image clipAmmoImage;
    [SerializeField]
    protected Text clipAmmoText;


    [SerializeField]
    protected Text healthText;
    [SerializeField]
    protected Image healthImage;


    [SerializeField]
    protected Text healthPackText;
    [SerializeField]
    protected Image healthPackImage;


    [SerializeField]
    protected Text bulletInventoryText;
    [SerializeField]
    protected Image bulletInventoryImage;

    [SerializeField]
    protected Image hitMarker;




    /////
    ///// Public Manipulation Functions
    /////


    public static void SetHealth(float current, float max)
    {
        singleton.healthImage.enabled = true;
        float ratio = current / max;
        singleton.healthImage.fillAmount = ratio;
        singleton.healthText.text = current + "/" + max;
    }



    public static void SetClipAmmo(int current, int max)
    {
        singleton.clipAmmoText.enabled = true;
        singleton.clipAmmoText.text = current + "/" + max;
    }


    public static void SetHealthPackVisible(bool visible)
    {
        singleton.healthPackImage.enabled = visible;
        singleton.healthPackText.enabled = visible;
    }
    
    public static void SetInventoryBullets(int amount)
    {
        singleton.bulletInventoryImage.enabled = true;
        singleton.bulletInventoryText.text = amount.ToString();
    }

    public static void SetHitMarkerVisible(bool visible)
    {
        singleton.hitMarker.enabled = visible;
    }

}
