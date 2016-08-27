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
		numberOfCollectables--;
		Debug.Log ("Number of collectables remaining: " + numberOfCollectables);

		if (numberOfCollectables == 0) {
			// TODO: Game Ended
			Debug.Log ("Game Over!");
		}
	}

	#endregion
}
