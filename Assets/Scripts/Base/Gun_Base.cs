using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun_Base : MonoBehaviour {

	public abstract void Reload();

	public abstract void Shoot();	

	public abstract GunType GetGunType();
}
