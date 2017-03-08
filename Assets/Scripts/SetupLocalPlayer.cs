using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : SetupLocalPlayer_Base {
	[SerializeField]
	List<Behaviour> behavioursToEnable;

	[SerializeField]
	Camera camer_a;

	[TargetRpc]
	public override void TargetSetup(NetworkConnection conn){
		Debug.Log("Setup local player");
		NetworkIdentity netId = GetComponent<NetworkIdentity>();

		foreach(Behaviour b in behavioursToEnable){
			b.enabled = true;	
		}
		

	}


}
