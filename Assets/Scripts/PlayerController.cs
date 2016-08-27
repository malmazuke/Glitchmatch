using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {

	#region Private Properties

	NetworkStartPosition[] spawnPoints;

	#endregion

	#region Unity Methods

	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {
			spawnPoints = FindObjectsOfType<NetworkStartPosition> ();
		}
	}

	#endregion

	#region Public Methods

	public void Reset () {
		if (!isServer) {
			return;
		}
		GetComponent<Points> ().ResetPoints ();
		RpcRespawn ();
	}

	[ClientRpc]
	public void RpcRespawn () {
		Vector3 spawnPoint = Vector3.zero;

		if (spawnPoints != null && spawnPoints.Length > 0) {
			spawnPoint = spawnPoints[Random.Range (0, spawnPoints.Length)].transform.position;
		}

		transform.position = spawnPoint;
	}

	#endregion
}
