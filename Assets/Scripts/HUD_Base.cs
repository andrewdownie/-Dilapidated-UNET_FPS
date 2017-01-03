using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public abstract class HUD_Base<T> : MonoBehaviour where T : HUD_Base<T>  {

    public static T singleton;


    /// 
    /// GUI Elements
    /// 
    protected Canvas canvas;

    protected Text clipAmmo;


    protected Text healthText;
    protected Image healthBar;


    /// 
    /// Find GUI Elements
    /// 
    private void FindGUIElements()
    {
        canvas = StaticFunc.FindComponent<Canvas>("Canvas");

        clipAmmo = StaticFunc.FindComponent<Text>("HUD_ClipAmmo");

      
        healthBar = StaticFunc.FindComponent<Image>("HUD_HealthBar");
        healthText = StaticFunc.FindComponent<Text>("HUD_HealthText");
    }


    void Awake()
    {
        if (FindObjectsOfType(typeof(Canvas)).Length > 1)
        {
            Debug.LogWarning("Destroying excess Canvas's...");
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        singleton = (T)this;
        FindGUIElements();
        
    }


    public static void CanvasEnabled(bool enabled)
    {
        singleton.canvas.enabled = enabled;

    }

    void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            CanvasEnabled(false);
        }
        else
        {
            CanvasEnabled(true);
        }
    }
}
