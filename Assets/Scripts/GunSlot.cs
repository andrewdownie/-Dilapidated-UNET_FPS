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
        primaryWeapon.gameObject.SetActive(false);
        secondaryWeapon.gameObject.SetActive(false);

        if(primaryWeapon != null){
            equippedWeapon = primaryWeapon;
        }
        else{
            equippedWeapon = secondaryWeapon;
        }
        equippedWeapon.gameObject.SetActive(true);        

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
        return false;
    }

    public override void PreviousWeapon(){
        Debug.Log("Previous weapon");
        ToggleEquip();
    }

    public override void NextWeapon(){
        Debug.Log("Next weapon");
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


    public override void Shoot(){
        equippedWeapon.Shoot();
        UpdateAmmoHUD();
    }
}
