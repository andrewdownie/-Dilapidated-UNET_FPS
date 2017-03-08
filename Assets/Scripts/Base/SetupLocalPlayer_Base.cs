using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class SetupLocalPlayer_Base : NetworkBehaviour {
	[TargetRpc]
	public abstract void TargetSetup(NetworkConnection conn);
}
