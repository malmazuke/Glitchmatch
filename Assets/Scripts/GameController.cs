using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameController : NetworkBehaviour {

	#region Public Properties

	public float maxRoundTime = 300.0f;
	public float endSequenceTime = 10.0f;

	#endregion

	#region Private Properties

	float timeElapsed = 0.0f;
	bool isEndSequence = false;

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

		timeElapsed += Time.deltaTime;

		if (timeElapsed > maxRoundTime) {
			if (!isEndSequence) {
				StartCoroutine (BeginEndSequence ());
			}
		} else {
			Debug.Log ("Time remaining: " + (maxRoundTime - timeElapsed));
		}
	}

	#endregion

	#region Public Methods

	IEnumerator BeginEndSequence () {
		Debug.Log ("Beginning End Sequence");
		isEndSequence = true;
		yield return new WaitForSeconds (endSequenceTime);
		RestartGame ();
	}

	public void RestartGame () {
		// TODO: This should be a bit more thorough than this
		GameObject.FindGameObjectWithTag ("CollectableSpawner").GetComponent<CollectableSpawner> ().SpawnCollectables ();

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");

		foreach (GameObject player in players) {
			PlayerController pc = player.GetComponent<PlayerController> ();
			pc.Reset ();
		}

		timeElapsed = 0.0f;
		isEndSequence = false;
	}

	#endregion
}
