using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AmmoInventory : MonoBehaviour {


    public Dictionary<GunType, int> bullets = new Dictionary<GunType, int>();

    public int GetAmmo(GunType type){
        return bullets[type];
    }

    public void AddAmmo(int amount, GunType type){
        if(amount > 0){
            bullets[type] += amount;
        }
    }

    public int RemoveAmmo(int amount, GunType type){
        if(bullets[type] >= amount){
            bullets[type] -= amount;
            return amount;
        }

        int amountAvailable = bullets[type];
        bullets[type] = 0;
        return amountAvailable; 
    }

}
