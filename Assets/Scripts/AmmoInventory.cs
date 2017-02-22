using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AmmoInventory : MonoBehaviour {


    
    private Dictionary<GunType, int> bullets = new Dictionary<GunType, int>();

    [SerializeField]
    int sniper, pistol, shotgun, assualtRifle, smg;




    void Start(){
        bullets = new Dictionary<GunType, int>();

        //TODO: custom unity inspector gui applied to a dictionary (unsupported by unity), 
        //      does not remember the values entered (values get forgotten at runtime)
        //TODO: this way is actually not bad, requiring only three lines of code to be added for a new
        //      weapon / ammo type to be setup: the Weapon type enum, the above serializedfield and
        //      the below add to dictionary.
        bullets.Add(GunType.sniper, sniper);
        bullets.Add(GunType.pistol, pistol);
        bullets.Add(GunType.assaultRifle, assualtRifle);
        bullets.Add(GunType.smg, smg);
        bullets.Add(GunType.shotgun, shotgun);
    }

    public int Count(GunType type){
        return bullets[type];
    }


    public void Add(int amount, GunType type){
        if(amount > 0){
            bullets[type] += amount;
        }
    }


    public int Request(int amountRequested, GunType type){
        if(bullets[type] >= amountRequested){
            bullets[type] -= amountRequested;
            return amountRequested;
        }

        int amountAvailable = bullets[type];
        bullets[type] = 0;
        return amountAvailable; 
    }

}
