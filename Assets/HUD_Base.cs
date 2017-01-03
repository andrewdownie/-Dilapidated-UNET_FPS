using UnityEngine;
using UnityEngine.UI;

public abstract class HUD_Base : MonoBehaviour {

    public static HUD singleton;
    private static int instancesCount = 0;
    

    /// 
    /// GUI Elements
    /// 
    protected Text clipAmmo;


    protected Text healthText;
    protected Image healthBar;


    /// 
    /// Find GUI Elements
    /// 
    private void FindGUIElements()
    {
        clipAmmo = StaticFunc.FindComponent<Text>("HUD_ClipAmmo");

      
        healthBar = StaticFunc.FindComponent<Image>("HUD_HealthBar");
        healthText = StaticFunc.FindComponent<Text>("HUD_HealthText");
    }





	void Awake () {
	    if(instancesCount != 0)
        {
            Debug.LogError("HUD_Interface[Awake]: there is more than one HUD_Interface in the scene. There should only ever be one. Breaking things on purpose because of this, sorry...");
            singleton = null;
            return;
        }
        
        
        singleton = (HUD)this;
        instancesCount++;
        
        
    }





    void Start()
    {
        if(instancesCount != 1)
        {
            Debug.LogWarning("HUD_Interface[Start]: there is more than one HUD_Interface in the scene. HUD_Interface will not continue with setup.");
            return;
        }

        FindGUIElements();
    }
	
}
