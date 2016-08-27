using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Points : NetworkBehaviour {

	[SyncVar(hook = "OnPointsChange")]
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

	void OnPointsChange (int pointsChange) {
		// TODO: Update some UI element
		if (isLocalPlayer) {
			Debug.Log ("My Score: " + pointsChange);
		}
	}
}
