using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;



public class Net_Manager : NetworkManager{
    List<Player_Base> playerList;

    void Start(){
        playerList = new List<Player_Base>();
    }


    [SerializeField]
    private Gun_Base startingGun;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId){
        ///
        /// Go through each existing player, and set gun transform parent to their gun slot
        ///
        foreach(Player_Base player in playerList){
            GunSlot_Base gunSlot = player.GunSlot;
        }

        ///
        /// Create and setup the player
        ///
        GameObject newPlayer = Instantiate(playerPrefab);
        NetworkServer.Spawn(newPlayer);

        NetworkIdentity newId = newPlayer.GetComponent<NetworkIdentity>();
        newId.AssignClientAuthority(conn);

        SetupLocalPlayer slp = newPlayer.GetComponent<SetupLocalPlayer>();

        slp.TargetSetup(conn);


        ///
        /// Create the starting weapon 
        ///
        Gun_Base gun = Instantiate(startingGun.gameObject).GetComponent<Gun_Base>();
        NetworkServer.Spawn(gun.gameObject);

        NetworkIdentity gunId = gun.GetComponent<NetworkIdentity>();
        gunId.AssignClientAuthority(conn);
        


        Player_Base pb = newPlayer.GetComponent<Player_Base>();
        NetworkInstanceId gunInstanceId = gun.GetComponent<NetworkIdentity>().netId;
        pb.RpcAddStartingGun(gunInstanceId);

        playerList.Add(pb);

    }
}
