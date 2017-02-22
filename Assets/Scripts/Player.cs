using UnityEngine;
using System.Collections;
using System;


/// <summary>
/// The player class has three main responsibilities.
///     1. Act as a centralized organizer of all the components that make up a player (vitals, gunslot, ect.) 
///     2. Act as an interface for picking items up in the world.
///     3. Handle user input (in the update method)
///     TODO: solution for automatic / single shot weapons
/// /// /// </summary>
public class Player : Player_Base {

    [SerializeField]
    private Vitals_Base vitals;

    [SerializeField]
    private GunSlot_Base gunSlot;

    [SerializeField]
    private AmmoInventory ammo;


    public override Vitals_Base Vitals{
        get{return vitals;}
    }

    public override GunSlot_Base GunSlot{
        get{return gunSlot;}
    }

    public override AmmoInventory Ammo{
        get{return ammo;}
    }

    public override void PickupAmmo(int amount, GunType gunType)
    {
        ammo.Add(amount, gunType);
        gunSlot.UpdateAmmoHUD();
    }

    public override bool TryPickupGun(Gun_Base gun){
        return gunSlot.TryPickup(gun);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            vitals.UseHealthpack();
        }

        if(Input.GetKeyDown(KeyCode.E)){
            gunSlot.Drop();
        }


        if(Input.GetKeyDown(KeyCode.R)){
            gunSlot.Reload();
        }

        if(Input.GetKeyDown(KeyCode.Q)){
            gunSlot.NextWeapon();
        }
    }
}
