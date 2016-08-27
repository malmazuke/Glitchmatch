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
		spawnPoints = FindObjectsOfType<NetworkStartPosition> ();
	}

	#endregion

	#region Public Methods

	public void Reset () {
		GetComponent<Points> ().ResetPoints ();
		Respawn ();
	}

	public void Respawn () {
		Vector3 spawnPoint = Vector3.zero;

		if (spawnPoints != null && spawnPoints.Length > 0) {
			spawnPoint = spawnPoints[Random.Range (0, spawnPoints.Length)].transform.position;
		}

		transform.position = spawnPoint;
	}

	#endregion
}
