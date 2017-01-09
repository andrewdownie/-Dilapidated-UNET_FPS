using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    [SerializeField]
    private float curHealth = 100;
    [SerializeField]
    private float maxHealth = 200;
    [SerializeField]
    private bool hasHealthPack;


    [SerializeField]
    private int inventoryBullets = 100;


    [SerializeField]
    private AudioClip healSound;


    private AudioSource audioSource;

    void Start () {
        HUD.SetHealth(curHealth, maxHealth);
        HUD.SetHealthPackVisible(hasHealthPack);
        HUD.SetInventoryBullets(inventoryBullets);

        if(curHealth == 0)
        {
            HUD.SetRespawnButtonVisible(true);
        }
        else
        {
            HUD.SetRespawnButtonVisible(false);
        }


        audioSource = GetComponent<AudioSource>();
	}


    public void AddBullets(int bullets)
    {
        if(bullets > 0)
        {
            inventoryBullets += bullets;
            HUD.SetInventoryBullets(inventoryBullets);
        }
    }

    public int RemoveBullets(int amountRequested)
    {
        if(amountRequested <= inventoryBullets)
        {
            inventoryBullets -= amountRequested;
            HUD.SetInventoryBullets(inventoryBullets);
            return amountRequested;
        }

        int amountAvailable = inventoryBullets;
        inventoryBullets = 0;
        HUD.SetInventoryBullets(0);
        return amountAvailable;
    }
	
    public void ChangeHealth(float amount)
    {
        curHealth = Mathf.Clamp(curHealth + amount, 0, maxHealth);
        HUD.SetHealth(curHealth, maxHealth);

        if(curHealth == 0)
        {
            HUD.SetRespawnButtonVisible(true);
        }
    }

    /// <summary>
    /// Adds a health pack for the player to use.
    /// </summary>
    /// <returns>True if the health pack was taken.</returns>
    public bool AddHealthPack()
    {
        if (hasHealthPack)
        {
            return false;
        }


        HUD.SetHealthPackVisible(true);
        hasHealthPack = true;
        return true;
    }


    public void AddAmmoPack()
    {
        inventoryBullets += 8;
        HUD.SetInventoryBullets(inventoryBullets);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (hasHealthPack && curHealth < maxHealth)
            {
                hasHealthPack = false;
                ChangeHealth(80);
                audioSource.PlayOneShot(healSound);
                HUD.SetHealthPackVisible(false);
            }
        }
    }
}
