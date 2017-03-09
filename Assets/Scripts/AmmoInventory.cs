using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class AmmoInventory : AmmoInventory_Base {

    private Action CB_AmmoChanged;
    

    [SerializeField][SyncVar]
    int sniper; 

    [SerializeField][SyncVar]
    int pistol;

    [SerializeField][SyncVar]
    int shotgun; 

    [SerializeField][SyncVar]
    int assualtRifle; 

    [SerializeField][SyncVar]
    int smg;



    public override int Count(GunType gunType){
        switch(gunType){
            case GunType.assaultRifle:  return assualtRifle;
            case GunType.pistol:        return pistol;
            case GunType.shotgun:       return shotgun;
            case GunType.smg:           return smg;
            case GunType.sniper:        return sniper;
        }

        return 0;
    }
    
    private void ChangeAmmo(GunType gunType, int ammoAmount){
        switch(gunType){
            case GunType.assaultRifle:  assualtRifle += ammoAmount; break;
            case GunType.pistol:        pistol += ammoAmount; break;
            case GunType.shotgun:       shotgun += ammoAmount; break;
            case GunType.smg:           smg += ammoAmount; break;
            case GunType.sniper:        sniper += ammoAmount; break;
        }
    }


    public override void Add(GunType gunType, int ammoAmount){
        if(ammoAmount > 0){
            ChangeAmmo(gunType, ammoAmount);
            if(CB_AmmoChanged != null){
                CB_AmmoChanged();
            }

        }
    }


    public override int Request(GunType gunType, int amountRequested){
        int returnAmount = 0;

        if(Count(gunType) >= amountRequested){
            ChangeAmmo(gunType, -amountRequested);
            returnAmount = amountRequested;
        }
        else{
            returnAmount = Count(gunType);
            ChangeAmmo(gunType, -returnAmount);
        }

        if(CB_AmmoChanged != null){
            CB_AmmoChanged();
        }

        return returnAmount; 
    }
    
    public override void SetCB_AmmoChanged(Action action){
        CB_AmmoChanged = action;
    }

}
