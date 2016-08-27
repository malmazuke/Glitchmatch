using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CollectableSpawner : NetworkBehaviour {

	#region Public Properties

	public GameObject collectablePrefab;
	public int numberOfCollectables;
	public float spawnDistance = 25.0f;
	public float spawnHeight = 1.5f;

	#endregion

	#region Unity Networking

	public override void OnStartServer ()
	{
		for (int i = 0; i < numberOfCollectables; i++) {
			var spawnPosition = new Vector3 (
				Random.Range (-spawnDistance, spawnDistance), 
				spawnHeight, 
				Random.Range (-spawnDistance, spawnDistance));

			GameObject collectable = Instantiate (collectablePrefab, spawnPosition, Quaternion.identity) as GameObject;
			NetworkServer.Spawn (collectable);
		}

		GameController gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		gc.SetNumberOfCollectables (numberOfCollectables);
	}

	#endregion
}
