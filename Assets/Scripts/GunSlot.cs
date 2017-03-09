using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;

public class GunSlot : GunSlot_Base {

    [SerializeField]
    Player player;

    private Action CB_AmmoChanged;

    [SerializeField]
    private Gun_Base primaryGun;
    
    [SerializeField]
    private Gun_Base secondaryGun;


    [SerializeField]
    private Gun_Base equippedGun;

	// Use this for initialization
	void Start () {
        return;/////////////////////////////////////
        if(primaryGun != null){
            Debug.Log("Primary weapon is null");
            equippedGun = primaryGun;
            secondaryGun.gameObject.SetActive(false);
        }
        else{
            equippedGun = secondaryGun;
        }
        equippedGun.gameObject.SetActive(true);        
        equippedGun.AlignGun();

       // CB_AmmoChanged();/////////////////////////TODO: NULL ATM due to networking
    }
	
    public override void Drop(){
        if(primaryGun != null && equippedGun != secondaryGun)
        {
            equippedGun = secondaryGun;
            secondaryGun.gameObject.SetActive(true);
//            UpdateAmmoHUD();

            primaryGun.transform.parent = null;
            primaryGun.Drop();
            primaryGun = null;
        }

        CB_AmmoChanged();
    }

    public override bool TryPickup(Gun_Base gun){
        if(primaryGun == null){
            primaryGun = gun;
            equippedGun = primaryGun;
            secondaryGun.gameObject.SetActive(false);
            CB_AmmoChanged();
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
        if(primaryGun == null){
            return;
        }

        equippedGun.gameObject.SetActive(false);
        if(equippedGun == primaryGun){
            equippedGun = secondaryGun;
        }
        else{
            equippedGun = primaryGun;
        }
        equippedGun.gameObject.SetActive(true);
        CB_AmmoChanged();
        equippedGun.AlignGun();
    }


    public override void Reload(){
        equippedGun.Reload();
        CB_AmmoChanged();
    }


    public override void Shoot(bool firstDown){
        if(equippedGun == null){
            Debug.Log("Equipped gun is null");
        }
        equippedGun.CmdShoot(firstDown);
        CB_AmmoChanged();
    }

    public override int BulletsInClip{
        get{ return equippedGun.BulletsInClip;}
    }

    public override int ClipSize{
        get{return equippedGun.ClipSize;}
    }


    public override void SetCB_AmmoChanged(Action action){
        CB_AmmoChanged = action;
    }


    public override Gun_Base EquippedGun{
        get{
            return equippedGun;
        }
    }

    public override Player Player{
        get{return player;}
    }

    
    public override void AddStartingGun(NetworkInstanceId gunId){
        Gun_Base startingGun = ClientScene.FindLocalObject(gunId).GetComponent<Gun_Base>();

        secondaryGun = startingGun;
        equippedGun = secondaryGun;
        startingGun.transform.parent = player.GunSlot.transform;
        startingGun.transform.localPosition = Vector3.zero;
        startingGun.transform.localRotation = Quaternion.Euler(0, 90, 0);
        equippedGun.AlignGun();
    }


}
