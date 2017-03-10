using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class Net_Manager : NetworkManager{
    Dictionary<NetworkConnection, string> players;

    [SerializeField]
    Gun_Base startingGun;




    void Start(){
        players = new Dictionary<NetworkConnection, string>();
        
    }



    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId){

        if(SceneManager.GetActiveScene().name == "Lobby"){
            players.Add(conn, "Player " + Random.Range(0, 999).ToString());  

            UpdatePlayerList();
        }     

    }

    public override void OnServerDisconnect(NetworkConnection conn){
        Debug.Log("Disconnect client");//TODO: OnClientDisconnect doesn't get called on the server when a player leaves...
        if(SceneManager.GetActiveScene().name == "Lobby"){
            players.Remove(conn);

            UpdatePlayerList();
        }

        base.OnServerDisconnect(conn);
    }


    public override void OnStopServer(){
        players.Clear();
        base.OnStopServer();
    }

    private void UpdatePlayerList(){

            Lobby updatePlayerList = GameObject.Find("Lobby").GetComponent<Lobby>();
            Text playerList = updatePlayerList.txtPlayerList;
            
            string newPlayerList = "";
            foreach(KeyValuePair<NetworkConnection, string> entry in players){
                newPlayerList += entry.Value + System.Environment.NewLine;
            }

            playerList.text = newPlayerList;
            playerList.rectTransform.localPosition = new Vector3(0, -30, 0);
            playerList.gameObject.SetActive(true);

            updatePlayerList.RpcUpdateList(newPlayerList);
    }

    public override void OnClientSceneChanged(NetworkConnection conn){
        Debug.Log("Move me to new scene pls");
        //ClientScene.Ready(conn);
         if(SceneManager.GetActiveScene().name == "Tree"){

         }
        base.OnClientSceneChanged(conn);
    }

    public void StartGame(){
        Debug.Log("Start game now");


        ServerChangeScene("Tree");
        foreach(KeyValuePair<NetworkConnection, string> entry in players){
            //ClientScene.Ready(entry.Key);
        }


        foreach(KeyValuePair<NetworkConnection, string> entry in players){
            NetworkConnection conn = entry.Key;
            string name = entry.Value;



            ///
            /// Create and setup the player
            ///
            GameObject newPlayer = Instantiate(playerPrefab, Vector3.one, Quaternion.identity, transform);
            newPlayer.name = name;
            NetworkServer.Spawn(newPlayer);

            NetworkIdentity newId = newPlayer.GetComponent<NetworkIdentity>();
            newId.AssignClientAuthority(conn);

            SetupLocalPlayer slp = newPlayer.GetComponent<SetupLocalPlayer>();
            slp.TargetSetup(conn);


            ///
            /// Create the starting weapon 
            ///
            Gun_Base gun = Instantiate(startingGun.gameObject, Vector3.zero, Quaternion.identity, transform).GetComponent<Gun_Base>();
            NetworkServer.Spawn(gun.gameObject);

            NetworkIdentity gunId = gun.GetComponent<NetworkIdentity>();
            gunId.AssignClientAuthority(conn);
            


            Player_Base pb = newPlayer.GetComponent<Player_Base>();
            NetworkInstanceId gunInstanceId = gun.GetComponent<NetworkIdentity>().netId;
            pb.RpcAddStartingGun(gunInstanceId);



            Debug.Log("Created player: " + name);
        }


    }
}
