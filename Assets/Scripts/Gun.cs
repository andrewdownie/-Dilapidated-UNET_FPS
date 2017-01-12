using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gun : MonoBehaviour {//TODO: need to check who actually owns the gun? transfer ownership on pickup?
    private Player player;
    private WeaponSlot weaponSlot;

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
    private GameObject bulletPrefab;

    [SerializeField]
    private ParticleSystem muzzleFlash;

    HitMarkerCallback hitMarkerCallback;

    bool canBePickedUp = false;

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

        WeaponSlot weaponSlot = parent.GetComponent<WeaponSlot>();

        if(weaponSlot != null)
        {
            this.weaponSlot = weaponSlot;

            Player player = weaponSlot.Player;

            if(player != null)
            {
                this.player = player;
            }
        }
        
    }
	


	// Update is called once per frame
	void Update () {
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


    void Shoot()
    {
        if (timeSinceLastShot >= timeBetweenShoots)
        {
            timeSinceLastShot = 0;

            if (bulletsInClip > 0)
            {
            
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
           
            
            }
            else
            {
                audioSource.PlayOneShot(outOfAmmo);
            }

        }
    }

    void Reload()
    {
        if(bulletsInClip < clipSize && timeSinceLastShot >= timeBetweenShoots)
        {
            int bulletsFromInventory = player.RemoveBullets(clipSize - bulletsInClip);

            if(bulletsFromInventory > 0)
            {
                audioSource.PlayOneShot(reload);
                bulletsInClip = bulletsFromInventory + bulletsInClip;
                timeSinceLastShot = -(reload.length - timeBetweenShoots);
                HUD.SetClipAmmo(bulletsInClip, clipSize);
            }

           
        }
        
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
                WeaponSlot _weaponSlot = _player.GetComponentInChildren<WeaponSlot>();

                if (_weaponSlot != null)
                {
                    player = _player;
                    weaponSlot = _weaponSlot;
                    gameObject.transform.parent = _weaponSlot.transform;

                    Destroy(GetComponent<Rigidbody>());
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
        weaponSlot = null;
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