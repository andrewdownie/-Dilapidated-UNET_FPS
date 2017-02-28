using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun_Base : MonoBehaviour {

	public abstract void Reload();

	public abstract void Shoot(bool firstDown);	

	public abstract void Drop();

	/// <summary>
	/// Aligns the gun to point at the center of the players screen.
	/// Requires that the gun be equipped.
	/// </summary>
	public abstract void Align();


	public abstract GunType GunType{get;}

	public abstract int ClipSize{get;}
	public abstract int BulletsInClip{get;}

}
