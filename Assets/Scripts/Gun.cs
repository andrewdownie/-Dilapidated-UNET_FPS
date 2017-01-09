using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gun : MonoBehaviour {

    [Header("Auto filled")]
    [SerializeField]
    private Player player;

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

        
        //TODO: this does not work
        player = transform.parent.parent.parent.gameObject.GetComponent<Player>();
        Debug.Log(transform.parent.parent.parent.gameObject.name);
        if (player == null)
        {
            Debug.Log(player.name);
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
        if(bulletsInClip < clipSize)
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
}


public enum GunType
{
    sniper,
    shotgun,
    pistol,
    smg,
    assaultRifle
}