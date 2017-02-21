using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AmmoInventory : MonoBehaviour {


    public Dictionary<GunType, int> bullets = new Dictionary<GunType, int>();

    public int Count(GunType type){
        return bullets[type];
    }


    void Start(){
        Array types = Enum.GetValues(typeof(GunType));
        
        if(types.Length != bullets.Count){
            foreach(GunType wt in types){
                if (bullets.ContainsKey(wt) == false)
                {
                    bullets.Add(wt, 0);
                }
            }
        }
        
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
