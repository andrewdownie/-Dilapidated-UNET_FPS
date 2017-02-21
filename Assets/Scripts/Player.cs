using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    [SerializeField]
    private Vitals_Base vitals;

    [SerializeField]
    private float maxHealth = 200;
    [SerializeField]
    private bool hasHealthPack;




    [SerializeField]
    private AmmoInventory ammo;


    [SerializeField]
    private AudioClip healSound;


    private AudioSource audioSource;


    void Start () {
        
	}


    public void AddAmmo(int amount, GunType gunType)
    {
        ammo.AddAmmo(amount, gunType);
        HUD.SetInventoryBullets(ammo.GetAmmoCount(gunType));
    }

    public int RequestAmmo(int amountRequested, GunType gunType)
    {
        int amountReturned = ammo.RequestAmmo(amountRequested, gunType);
        HUD.SetInventoryBullets(ammo.GetAmmoCount(gunType));
        return amountReturned;
    }

    public int GetAmmoCount(GunType gunType){
        return ammo.GetAmmoCount(gunType);
    }
	
    public void ChangeHealth(float amount)
    {
        vitals.ChangeHealth(amount);
    }

    /// <summary>
    /// Adds a health pack for the player to use.
    /// </summary>
    /// <returns>True if the health pack was taken.</returns>
    public bool AddHealthPack()
    {
        if (hasHealthPack)
        {
            return false;
        }


        HUD.SetHealthPackVisible(true);
        hasHealthPack = true;
        return true;
    }


    public void AddAmmoPack()
    {
        ammo.AddAmmo(8, GunType.pistol);
        //TODO: need to update HUD here, but need to know what gun is equiped
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            vitals.UseHealthpack();
        }
    }
}
