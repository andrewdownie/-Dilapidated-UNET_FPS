using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Lobby : NetworkBehaviour {
	public Text txtPlayerList;
	public Button btnStartGame;


	void Start(){
		if(!isServer){
			btnStartGame.gameObject.SetActive(false);	
		}
	}

	[ClientRpc]
	public void RpcUpdateList(string playerList){
		txtPlayerList.text = playerList;
		txtPlayerList.rectTransform.localPosition = new Vector3(0, -30, 0);
		txtPlayerList.gameObject.SetActive(true);
	}

	public void StartGame(){
		if(isServer){
			FindObjectOfType<Net_Manager>().StartGame();
		}
	}
	


}
