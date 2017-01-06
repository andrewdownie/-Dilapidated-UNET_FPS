using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gun : MonoBehaviour {

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip shoot, reload, outOfAmmo;

    [SerializeField]
    private Combat combat;


    [SerializeField]
    private int clipSize = 5;

    private int bulletsInClip = 5;

    [SerializeField]
    float timeBetweenShoots = 0.3f;
    float timeSinceLastShot = 1f;


    [SerializeField]
    private Transform bulletSpawnPoint;


    [SerializeField]
    private GameObject bulletPrefab;

    HitMarkerCallback hitMarkerCallback;

    void Start()
    {
        HUD.SetClipAmmo(bulletsInClip, clipSize);
        hitMarkerCallback = GetComponent<HitMarkerCallback>();
    }
	


	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
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
        if (bulletsInClip > 0)
        {
            if(timeSinceLastShot >= timeBetweenShoots)
            {
                audioSource.PlayOneShot(shoot);
                bulletsInClip -= 1;
                timeSinceLastShot = 0;
                HUD.SetClipAmmo(bulletsInClip, clipSize);

                Bullet bullet = ((GameObject)Instantiate(bulletPrefab)).GetComponent<Bullet>();
                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.rotation = bulletSpawnPoint.rotation;
                bullet.SetHitMarkerCallBack(hitMarkerCallback);
            }
            
        }
        else
        {
            audioSource.PlayOneShot(outOfAmmo);
        }
    }

    void Reload()
    {
        if(bulletsInClip < clipSize)
        {
            int bulletsFromInventory = combat.RemoveBullets(clipSize - bulletsInClip);

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
