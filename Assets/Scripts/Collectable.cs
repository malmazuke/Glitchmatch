using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

	#region Public Properties

	public float waveSpeed = 3.0f;
	public float waveAmplitude = 0.1f;

	#endregion

	#region Private Properties

	Vector3 originalPosition;

	#endregion

	#region Unity Methods

	void Awake () {
		originalPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {
		Vector3 updatedPosition = transform.position;
		updatedPosition.y = originalPosition.y +  Mathf.Sin (Time.time * waveSpeed) * waveAmplitude;
		transform.position = updatedPosition;

		transform.Rotate (1.0f, 0.0f, -1.0f);
	}

	#endregion
}
