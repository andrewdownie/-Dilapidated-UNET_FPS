using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun_Base : MonoBehaviour {

	public abstract void Reload();

	public abstract void Shoot();	

	public abstract void Drop();

	public abstract GunType GetGunType();

	public abstract int ClipSize{get;}
	public abstract int BulletsInClip{get;}

}
