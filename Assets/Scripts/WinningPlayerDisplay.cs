using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class WinningPlayerDisplay : NetworkBehaviour {

	#region Public Properties

	[Tooltip("The tag of the player")]
	public string playerTag = "Player";
	public Text winText;

	#endregion

	#region Private Properties

	bool isGameOver = false;

	#endregion

	#region Unity Methods
	
	// Update is called once per frame
	void Update () {
		if (isGameOver) {
			if (!winText.enabled) {
				winText.enabled = true;
				var players = GameObject.FindGameObjectsWithTag (playerTag);

				int highestPlayerIndex = 0;

				for (int i = 1; i < players.Length; i++) {
					Points currHighest = players[highestPlayerIndex].GetComponent<Points> ();
					Points nextPlayer = players[i].GetComponent<Points>();
					if (currHighest.currentPoints < nextPlayer.currentPoints) {
						highestPlayerIndex = i;
					}
				}

				GameObject highestPlayer = players[highestPlayerIndex];
				winText.text = GetNameOfPlayer (highestPlayer) + " wins\nScore: " + highestPlayer.GetComponent<Points> ().currentPoints;
			}
		} else if (winText.enabled) {
			winText.enabled = false;
		}
	}

	#endregion

	#region Public Methods

	[ClientRpc]
	public void RpcSetGameOver (bool isGameOver) {
		this.isGameOver = isGameOver;
	}

	#endregion

	#region Private Methods

	private string GetNameOfPlayer(GameObject player) {
		// Get the given name of the player
		PlayerController ctrl = player.GetComponent<PlayerController> ();
		string result = ctrl.playerName;
		return result;
	}

	#endregion
}
