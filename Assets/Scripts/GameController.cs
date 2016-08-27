using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameController : NetworkBehaviour {

	#region Public Properties

	public int numberOfCollectables { get; private set; }

	#endregion

	#region Unity Methods

	// Use this for initialization
	void Start () {
		if (!isServer) {
			return;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!isServer) {
			return;
		}
	}

	#endregion

	#region Public Methods

	public void SetNumberOfCollectables (int numberOfCollectables) {
		this.numberOfCollectables = numberOfCollectables;
	}

	public void CollectableCollected () {
		if (!isServer) {
			return;
		}
		numberOfCollectables--;
		Debug.Log ("Number of collectables remaining: " + numberOfCollectables);

		if (numberOfCollectables == 0) {
			RestartGame ();
		}
	}

	public void RestartGame () {
		// TODO: This should be a bit more thorough than this
		GameObject.FindGameObjectWithTag ("CollectableSpawner").GetComponent<CollectableSpawner> ().SpawnCollectables ();

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");

		foreach (GameObject player in players) {
			PlayerController pc = player.GetComponent<PlayerController> ();
			pc.Reset ();
		}
	}

	#endregion
}
