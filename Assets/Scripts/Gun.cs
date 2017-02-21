using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gun : Gun_Base {//TODO: need to check who actually owns the gun? transfer ownership on pickup?
    [SerializeField]
    private GunType gunType;

    private Player player;
    private GunSlot gunSlot;

    [Header("Sound")]
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip shoot, reload, outOfAmmo;

    
    [Header("Weapon Firing")]
    [SerializeField]
    private int clipSize = 5;
    [SerializeField]
    private int bulletsInClip = 5;


    [SerializeField]
    private bool automatic;
    private System.Func<KeyCode, bool> mouseAction;

    [SerializeField]
    float timeBetweenShoots = 0.3f;
    float timeSinceLastShot = 1f;


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

    HitMarkerCallback hitMarkerCallback;
    

    void Start()
    {
        HUD.SetClipAmmo(bulletsInClip, clipSize);
        hitMarkerCallback = GetComponent<HitMarkerCallback>();
        
        if (automatic)
        {
            mouseAction = Input.GetKey;
        }
        else
        {
            mouseAction = Input.GetKeyDown;
        }
        
        FindWeaponSlotAndPlayer();


    }

    void FindWeaponSlotAndPlayer()
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
	


	// Update is called once per frame
	void Update () {
        AlignGun();

        if (mouseAction(KeyCode.Mouse0))
        {
            Shoot();  
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        timeSinceLastShot += Time.deltaTime;
	}


    public void DropGun()
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

            Player _player = coll.GetComponent<Player>();
            if (_player != null)
            {
                GunSlot _weaponSlot = _player.GetComponentInChildren<GunSlot>();

                if (_weaponSlot != null)
                {
                    player = _player;
                    gunSlot = _weaponSlot;
                    gameObject.transform.parent = _weaponSlot.transform;

                    Destroy(GetComponent<Rigidbody>());//Why do I destroy rigidbody and disable colliders?
                    enabled = true;

                    Collider[] colliders = GetComponents<Collider>();
                    foreach (Collider c in colliders)
                    {
                        c.enabled = false;
                    }

                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.Euler(0, 180, 0);

                }
            }

        }
    }

    IEnumerator DropGunTimer(){
        yield return new WaitForSeconds(1.3f);
        player = null;
        gunSlot = null;
    } 

    void AlignGun(){
        if(player != null){
            Transform target = transform.parent.parent;
            Vector3 point = target.position + (target.forward * 10);
            
            transform.LookAt(point);
            transform.Rotate(new Vector3(0, 90, 0));
        }
    }

    public override void Shoot(){

        if (timeSinceLastShot >= timeBetweenShoots)
        {
            timeSinceLastShot = 0;

            if (bulletsInClip > 0)
            {
            
                ///
                /// Create the bullet
                ///
                audioSource.PlayOneShot(shoot);
                bulletsInClip -= 1;
                HUD.SetClipAmmo(bulletsInClip, clipSize);

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
                Instantiate(shellPrefab, shellSpawnPoint.position, transform.rotation);
           
            
            }
            else
            {
                audioSource.PlayOneShot(outOfAmmo);
            }

        }
    }

    public override void Reload(){

        if(bulletsInClip < clipSize && timeSinceLastShot >= timeBetweenShoots)
        {
            int bulletsFromInventory = player.RequestAmmo(clipSize - bulletsInClip, gunType);

            if(bulletsFromInventory > 0)
            {
                audioSource.PlayOneShot(reload);
                bulletsInClip = bulletsFromInventory + bulletsInClip;
                timeSinceLastShot = -(reload.length - timeBetweenShoots);
                HUD.SetClipAmmo(bulletsInClip, clipSize);
            }

           
        }
    }

    public override GunType GetGunType(){
        return gunType;
    }

    
}


public enum GunType
{
    sniper,
    shotgun,
    pistol,
    smg,
    assaultRifle
}