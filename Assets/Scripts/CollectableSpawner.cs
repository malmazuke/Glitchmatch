using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class CollectableSpawner : NetworkBehaviour {

	#region Public Properties

	public GameObject collectablePrefab;
	public float timeToDespawn = 30.0f;

	#endregion

	#region Private Properties

	List<Collectable> collectables = new List<Collectable> ();

	#endregion

	#region Unity Networking

	public override void OnStartServer ()
	{
		SpawnCollectables ();
	}

	#endregion

	#region Public Methods

	public void SpawnCollectables () {
		GameObject[] spawnableLocations = GameObject.FindGameObjectsWithTag ("CollectableSpawnPoint");

		foreach (GameObject spawnLocation in spawnableLocations) {
			spawnLocation.GetComponent<Renderer> ().enabled = false;
			var spawnPosition = spawnLocation.transform.position;

			GameObject collectable = Instantiate (collectablePrefab, spawnPosition, Quaternion.identity) as GameObject;
			collectables.Add (collectable.GetComponent<Collectable> ());
			NetworkServer.Spawn (collectable);
		}

		GameController gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		gc.SetNumberOfCollectables (collectables.Count);
	}

	public void CollectableWasCollected (Collectable collectable) {
		if (!isServer) {
			return;
		}
		StartCoroutine (TemporarilyDisableCollectable (collectable));
	}

	#endregion

	#region Private Methods

	IEnumerator TemporarilyDisableCollectable (Collectable collectable) {
		collectable.gameObject.SetActive (false);
		
		float timeElapsed = 0.0f;

		while (timeElapsed < timeToDespawn) {
			timeElapsed += Time.deltaTime;
			yield return null;
		}

		collectable.gameObject.SetActive (true);
	}

	#endregion
}
