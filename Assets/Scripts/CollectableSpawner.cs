using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class CollectableSpawner : NetworkBehaviour {

	#region Public Properties

	public GameObject collectablePrefab;
	public float timeToDespawn = 30.0f;

	#endregion

	#region Unity Networking

	public override void OnStartServer ()
	{
		SpawnCollectables ();
	}

	#endregion

	#region Public Methods

	public void SpawnCollectables () {
		if (!isServer) {
			return;
		}
		ClearOldCollectables ();

		GameObject[] spawnableLocations = GameObject.FindGameObjectsWithTag ("CollectableSpawnPoint");

		foreach (GameObject spawnLocation in spawnableLocations) {
			spawnLocation.GetComponent<Renderer> ().enabled = false;
			var spawnPosition = spawnLocation.transform.position;

			GameObject collectable = Instantiate (collectablePrefab, spawnPosition, Quaternion.identity) as GameObject;
			NetworkServer.Spawn (collectable);
		}
	}

	public void CollectableWasCollected (Collectable collectable) {
		if (!isServer) {
			return;
		}
		StartCoroutine (TemporarilyDisableCollectable (collectable));
	}

	#endregion

	#region Private Methods

	void ClearOldCollectables () {
		GameObject[] collectables = GameObject.FindGameObjectsWithTag ("Collectable");
		if (collectables.Length > 0) {
			foreach (GameObject go in collectables) {
				Destroy (go);
			}
		}
	}

	IEnumerator TemporarilyDisableCollectable (Collectable collectable) {
		if (!isServer) {
			yield break;
		}
		Vector3 collectablePosition = collectable.transform.position;

		Destroy (collectable.gameObject);
		
		float timeElapsed = 0.0f;

		while (timeElapsed < timeToDespawn) {
			timeElapsed += Time.deltaTime;
			yield return null;
		}

		GameObject newCollectable = Instantiate (collectablePrefab, collectablePosition, Quaternion.identity) as GameObject;
		NetworkServer.Spawn (newCollectable);
	}

	#endregion
}
