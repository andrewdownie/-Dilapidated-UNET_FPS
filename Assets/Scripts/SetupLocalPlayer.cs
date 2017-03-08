using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : SetupLocalPlayer_Base {
	[SerializeField]
	List<Behaviour> behavioursToEnable;

	[SerializeField]
	Camera camer_a;

	[ClientRpc]
	public override void RpcSetup(){
		Debug.Log("Setup local player");
		NetworkIdentity netId = GetComponent<NetworkIdentity>();

		if(netId.hasAuthority){
			foreach(Behaviour b in behavioursToEnable){
				b.enabled = true;	
			}
		}		
		





	}


}
