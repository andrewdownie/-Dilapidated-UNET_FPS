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

        if(primaryWeapon != null){
            Debug.Log("Primary weapon is null");
            equippedWeapon = primaryWeapon;
            secondaryWeapon.gameObject.SetActive(false);
        }
        else{
            equippedWeapon = secondaryWeapon;
        }
        equippedWeapon.gameObject.SetActive(true);        
        equippedWeapon.Align();

        UpdateAmmoHUD();
    }
	
    public override void Drop(){
        if(primaryWeapon != null && equippedWeapon != secondaryWeapon)
        {
            equippedWeapon = secondaryWeapon;
            secondaryWeapon.gameObject.SetActive(true);
            UpdateAmmoHUD();

            primaryWeapon.transform.parent = null;
            primaryWeapon.Drop();
            primaryWeapon = null;
        }

        UpdateAmmoHUD();
    }

    public override bool TryPickup(Gun_Base gun){
        if(primaryWeapon == null){
            primaryWeapon = gun;
            equippedWeapon = primaryWeapon;
            secondaryWeapon.gameObject.SetActive(false);
            UpdateAmmoHUD();
            return true;
        }
        return false;
    }

    public override void PreviousWeapon(){
        ToggleEquip();
    }

    public override void NextWeapon(){
        ToggleEquip();
    }

    private void ToggleEquip(){
        if(primaryWeapon == null){
            return;
        }

        equippedWeapon.gameObject.SetActive(false);
        if(equippedWeapon == primaryWeapon){
            equippedWeapon = secondaryWeapon;
        }
        else{
            equippedWeapon = primaryWeapon;
        }
        equippedWeapon.gameObject.SetActive(true);
        UpdateAmmoHUD();
        equippedWeapon.Align();
    }


    public override void UpdateAmmoHUD(){
        HUD.SetInventoryAmmo(player.Ammo.Count(equippedWeapon.GetGunType()));
        HUD.SetClipAmmo(equippedWeapon.BulletsInClip, equippedWeapon.ClipSize);
    }


    public Player Player
    {
        get { return player; }
    }

    public override void Reload(){
        equippedWeapon.Reload();
        UpdateAmmoHUD();
    }


    public override void Shoot(bool firstDown){
        equippedWeapon.Shoot(firstDown);
        UpdateAmmoHUD();
    }
}
