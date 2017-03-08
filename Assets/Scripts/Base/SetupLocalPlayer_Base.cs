using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class SetupLocalPlayer_Base : NetworkBehaviour {
	[ClientRpc]
	public abstract void RpcSetup();
}
