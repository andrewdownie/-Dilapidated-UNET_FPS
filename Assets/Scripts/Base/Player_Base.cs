﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_Base : MonoBehaviour {
	public abstract void PickupAmmo(int amount, GunType gunType);


	public abstract bool TryPickupGun(Gun_Base gun);


	public abstract Vitals_Base Vitals{get;}
	public abstract GunSlot_Base GunSlot{get;} 

	public abstract AmmoInventory Ammo{get;}

}