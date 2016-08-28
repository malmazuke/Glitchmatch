using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Points : NetworkBehaviour {

	[SyncVar]
	public int currentPoints = 0;

	public void ResetPoints () {
		if (!isServer) {
			return;
		}
		currentPoints = 0;
	}

	public void AddPoints (int pointsToAdd) {
		if (!isServer) {
			return;
		}
		currentPoints += pointsToAdd;
	}
}
