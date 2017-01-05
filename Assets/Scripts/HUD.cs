using UnityEngine;
using System.Collections;

public class HUD : HUD_Base<HUD> {
    

    public static void SetHealth(float current, float max)
    {
        singleton.healthBar.enabled = true;

        float ratio = current / max;

        singleton.healthBar.fillAmount = ratio;

        singleton.healthText.text = current + "/" + max;
    }



    public static void SetClipAmmo(int current, int max)
    {
        singleton.clipAmmo.enabled = true;

        singleton.clipAmmo.text = current + "/" + max;
    }


    public static void HealthPackVisible(bool visible)
    {
        singleton.healthPackImage.enabled = visible;
        singleton.healthPackText.enabled = visible;
    }
    
    
}
