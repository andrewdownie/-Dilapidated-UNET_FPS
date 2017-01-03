using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A list of constants for use by other gameObjects
/// </summary>
public static partial class GlobalConstants { 
    
    public static Text HUD_ClipAmmo()
    {
        return FindComponent<Text>("HUD_ClipAmmo");
    }

    public static Text HUD_HealthText()
    {
        return FindComponent<Text>("HUD_HealthText");
    }

    public static Image HUD_HealthBarBackground()
    {
        return FindComponent<Image>("HUD_HealthBarBackground");
    }

    public static Image HUD_HealthBar()
    {
        return FindComponent<Image>("HUD_HealthBar");
    }






   
	
}


