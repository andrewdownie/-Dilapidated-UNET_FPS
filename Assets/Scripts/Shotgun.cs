﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: replace player reference, to indirect references through GunSlot
public class Shotgun : Gun_Base {
    [SerializeField]
    private GunType gunType;

    private Player_Base player;
    private GunSlot_Base gunSlot;

    [Header("Sound Clips")]
    [SerializeField]
    private AudioClip shoot, reload, outOfAmmo;

    
    [Header("Weapon Firing")]
    [SerializeField]
    private int clipSize = 5;
    [SerializeField]
    private int bulletsInClip = 5;


    [SerializeField]
    private bool automatic;

    [SerializeField]
    float timeBetweenShots = 0.3f;
    float timeSinceLastShot = 1f;

	[SerializeField]
	private float timeBetweenReloads;
	private float timeSinceLastReload;

	bool canShoot, canReload, reloading;


    [Header("Other Setup")]
    [SerializeField]
    private Transform bulletSpawnPoint;

    [SerializeField]
    private Transform shellSpawnPoint;
    
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject shellPrefab;

    [SerializeField]
    private ParticleSystem muzzleFlash;

    [SerializeField]
    HitMarkerCallback hitMarkerCallback;
    

    void Start()
    {
        FindGunSlotAndPlayer();
    }

    void FindGunSlotAndPlayer()
    {
        Transform parent = transform.parent;

        if(parent == null)
        {
            enabled = false;
            return;
        }

        GunSlot weaponSlot = parent.GetComponent<GunSlot>();

        if(weaponSlot != null)
        {
            this.gunSlot = weaponSlot;

            Player player = weaponSlot.Player;

            if(player != null)
            {
                this.player = player;
            }
        }
        
    }

    public override int BulletsInClip{
        get{return bulletsInClip;}
    }

    public override int ClipSize{
        get{return clipSize;}
    }
	


	// Update is called once per frame
	void Update () {
        //TODO: record the time since last shot once, and compare the saved value to
        //      the current value
        timeSinceLastShot += Time.deltaTime;
		timeSinceLastReload += Time.deltaTime;

		if(timeSinceLastShot >= timeBetweenShots){
			canShoot = true;
		}

		if(timeSinceLastReload >= timeBetweenReloads){
			canReload = true;
		}

		if(reloading){
			Reload();
		}
	}



	//TODO: instead of adding a rigid body, have a rigid body on by default, and toggle 'isKinematic' (don't actually know if this will do what I want, but it would be much better than what I have if it does)
    public override void Drop()
    {
        enabled = false;

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = true;

        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider c in colliders)
        {
            c.enabled = true;
        }

        StartCoroutine(DropGunTimer());
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player" && player == null)
        {
            Debug.Log("Collided with: " + coll.name);

            Player_Base _player = coll.GetComponent<Player_Base>();
            if (_player != null)
            {
                GunSlot_Base _gunSlot = _player.GunSlot;

                if (_gunSlot != null && _gunSlot.TryPickup(this))
                {
                    player = _player;
                    gunSlot = _gunSlot;
                    gameObject.transform.parent = _gunSlot.transform;

                    Destroy(GetComponent<Rigidbody>());
                    enabled = true;

                    Collider[] colliders = GetComponents<Collider>();
                    foreach (Collider c in colliders)
                    {
                        c.enabled = false;
                    }

                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                    Align();
                }
            }

        }
    }

    IEnumerator DropGunTimer(){
        yield return new WaitForSeconds(1.3f);
        player = null;
        gunSlot = null;
    } 

    public override void Align(){
        if(player != null){
            Transform target = transform.parent.parent;
            Vector3 point = target.position + (target.forward * 10);
            
            transform.LookAt(point);
            transform.Rotate(new Vector3(0, 90, 0));
        }
    }

    public override void Shoot(bool firstDown){
        if(!automatic && !firstDown){
            return;
        }

        if (canShoot)
        {
            timeSinceLastShot = 0;
			canShoot = false;
			canReload = false;
			reloading = false;

            if (bulletsInClip > 0)
            {
            
                ///
                /// Create the bullet
                ///
                player.AudioSource.PlayOneShot(shoot);
                bulletsInClip -= 1;

                Bullet bullet = ((GameObject)Instantiate(bulletPrefab)).GetComponent<Bullet>();
                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.rotation = bulletSpawnPoint.rotation;
                bullet.SetHitMarkerCallBack(hitMarkerCallback);

                Transform t = ((ParticleSystem)Instantiate(muzzleFlash)).GetComponent<Transform>();
                t.position = bulletSpawnPoint.position;
                t.rotation = bulletSpawnPoint.rotation;
                t.parent = bulletSpawnPoint;

                ///
                /// Create the shell
                ///
                Instantiate(shellPrefab, shellSpawnPoint.position, transform.rotation * shellSpawnPoint.localRotation);
           
            
            }
            else
            {
                player.AudioSource.PlayOneShot(outOfAmmo);
            }

        }
    }

    public override void Reload(){
        

        if(bulletsInClip < clipSize)
        {
			if(canShoot && canReload){
				reloading = true;
				int bulletsFromInventory = player.Ammo.Request(1, gunType);

				if(bulletsFromInventory > 0)
				{
					player.AudioSource.PlayOneShot(reload);
					bulletsInClip += bulletsFromInventory;
					timeSinceLastReload = 0;
					canReload = false;
					HUD.SetClipAmmo(bulletsInClip, clipSize);
				}
				else{
					reloading = false;
				}
			}

           
        }
		else{
			reloading = false;
		}
    }

    public override GunType GetGunType(){
        return gunType;
    }

    
}