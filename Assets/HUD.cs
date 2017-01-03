using UnityEngine;
using System.Collections;

public class HUD : HUD_Base<HUD> {
    

    public static void SetHealth(int current, int max)
    {
        float ratio = current / max;

        singleton.healthBar.fillAmount = ratio;

        singleton.healthText.text = current + "/" + max;
    }



    public static void SetClipAmmo(int current, int max)
    {
        singleton.clipAmmo.text = current + "/" + max;
    }
}
