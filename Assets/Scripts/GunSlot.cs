using UnityEngine;
using System.Collections;

public class GunSlot : GunSlot_Base {
    [SerializeField]
    private Player player;

    [SerializeField]
    private KeyCode dropWeaponKey = KeyCode.E;

    [SerializeField]
    private Gun_Base primaryWeapon;
    
    [SerializeField]
    private Gun_Base secondaryWeapon;

	// Use this for initialization
	void Start () {
        primaryWeapon = GetComponentInChildren<Gun_Base>();

        HUD.SetInventoryBullets(player.GetAmmoCount(secondaryWeapon.GetGunType()));
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(dropWeaponKey))
        {
            if(primaryWeapon != null)
            {
                primaryWeapon.transform.parent = null;
                //TODO: make the slot drop the gun
                //currentlyEquippedGun.Drop();
            }
        }
	}

    public override void Drop(){

    }

    public override bool TryPickup(Gun_Base gun){
        return false;
    }

    public override void PreviousWeapon(){

    }

    public override void NextWeapon(){

    }


    public Player Player
    {
        get { return player; }
    }
}
