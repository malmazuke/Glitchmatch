using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {

	#region Public Properties

	public float yOutOfBounds = -200.0f;

	[SyncVar]
	public string playerName;

	#endregion

	#region Private Properties

	NetworkStartPosition[] spawnPoints;

	#endregion

	#region Unity Methods

	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {
			spawnPoints = FindObjectsOfType<NetworkStartPosition> ();
			CmdSetPlayerName (System.Environment.UserName);

			// Disable the Networking HUD
			GameObject network = GameObject.FindGameObjectWithTag ("NetworkManager") as GameObject;
			network.GetComponent<NetworkManagerHUD> ().showGUI = false;
		}
	}

	void Update () {
		if (transform.position.y < yOutOfBounds) {
			Respawn ();
		}
	}

	[Command]
	void CmdSetPlayerName(string playerName) {
		this.playerName = playerName;
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
		Respawn ();
	}

	#endregion

	void Respawn () {
		Vector3 spawnPoint = Vector3.zero;

		if (spawnPoints != null && spawnPoints.Length > 0) {
			spawnPoint = spawnPoints[Random.Range (0, spawnPoints.Length)].transform.position;
		}

		transform.position = spawnPoint;
	}
}
