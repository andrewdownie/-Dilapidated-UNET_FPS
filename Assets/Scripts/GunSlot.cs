using UnityEngine;
using System.Collections;

public class GunSlot : GunSlot_Base {
    [SerializeField]
    private Player player;

    [SerializeField]
    private Gun_Base primaryWeapon;
    
    [SerializeField]
    private Gun_Base secondaryWeapon;


    private Gun_Base equippedWeapon;

	// Use this for initialization
	void Start () {
        //primaryWeapon = GetComponentInChildren<Gun_Base>();
        equippedWeapon = secondaryWeapon;


        HUD.SetInventoryAmmo(player.Ammo.Count(secondaryWeapon.GetGunType()));
    }
	
    public override void Drop(){
        Debug.Log("Drop gun pls");
        if(primaryWeapon != null && equippedWeapon != secondaryWeapon)
        {
            primaryWeapon.transform.parent = null;
            //TODO: make the slot drop the gun
            //currentlyEquippedGun.Drop();
        }
    }

    public override bool TryPickup(Gun_Base gun){
        return false;
    }

    public override void PreviousWeapon(){
        Debug.Log("Previous weapon");
        //Since there are only two weapons in this setup, next / prev weapon is just a toggle
        if(equippedWeapon == primaryWeapon){
            equippedWeapon = secondaryWeapon;
        }
        else{
            equippedWeapon = primaryWeapon;
        }
    }

    public override void NextWeapon(){
        Debug.Log("Next weapon");
        //Since there are only two weapons in this setup, next / prev weapon is just a toggle
        if(equippedWeapon == primaryWeapon){
            equippedWeapon = secondaryWeapon;
        }
        else{
            equippedWeapon = primaryWeapon;
        }
    }

    public override void UpdateAmmoHUD(){
        HUD.SetInventoryAmmo(player.Ammo.Count(equippedWeapon.GetGunType()));
    }


    public Player Player
    {
        get { return player; }
    }

    public override void Reload(){
        equippedWeapon.Reload();
        HUD.SetInventoryAmmo(player.Ammo.Count(equippedWeapon.GetGunType()));
    }

    public override void Shoot(){
        equippedWeapon.Shoot();
        Debug.Log("Shoot pls");
    }
}
