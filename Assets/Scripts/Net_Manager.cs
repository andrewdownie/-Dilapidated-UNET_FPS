using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class Net_Manager : NetworkManager{
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId){
        Debug.Log("Add player to game here | Controller ID: " + playerControllerId);

        GameObject newPlayer = Instantiate(playerPrefab);
        NetworkServer.Spawn(newPlayer);

        NetworkIdentity newId = newPlayer.GetComponent<NetworkIdentity>();
        newId.AssignClientAuthority(conn);

        SetupLocalPlayer slp = newPlayer.GetComponent<SetupLocalPlayer>();

        slp.RpcSetup();

    }
}
