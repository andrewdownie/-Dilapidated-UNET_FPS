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


    void Start()
    {
        HUD.SetClipAmmo(bulletsInClip, clipSize);
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

                if(bulletsInClip == 0)
                {
                    Reload();
                }
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
                bulletsInClip = clipSize;
                timeSinceLastShot = -(reload.length - timeBetweenShoots);
                HUD.SetClipAmmo(bulletsInClip, clipSize);
            }

           
        }
        
    }
}
