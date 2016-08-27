using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Collectable : NetworkBehaviour {

	#region Public Properties

	public int pointsWorth = 1;
	public bool shouldRandomizeStartingRotation = true;

	public float waveSpeed = 3.0f;
	public float waveAmplitude = 0.1f;

	#endregion

	#region Private Properties

	Vector3 originalPosition;
	float waveOffset = 0.0f;

	#endregion

	#region Unity Methods

	void Awake () {
		originalPosition = transform.position;
	}

	void Start () {
		if (shouldRandomizeStartingRotation) {
			transform.rotation = Quaternion.Euler (transform.rotation.x, Random.Range (0.0f, 359.0f), transform.rotation.z);
			waveOffset = Random.Range (0.0f, 5.0f);
		}
	}

	// Update is called once per frame
	void Update () {
		Vector3 updatedPosition = transform.position;
		updatedPosition.y = originalPosition.y +  Mathf.Sin (Time.time * waveSpeed + waveOffset) * waveAmplitude;
		transform.position = updatedPosition;

		transform.Rotate (0.0f, 1.0f, 0.0f);
	}

	void OnTriggerEnter(Collider other) {
		if (!isServer) {
			return;
		}
		if (other.tag == "Player") {
			other.GetComponent<Points> ().AddPoints (pointsWorth);

//			GameController gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
//			gc.CollectableCollected ();
//			Destroy (gameObject);
			
		}
	}

	#endregion
}
