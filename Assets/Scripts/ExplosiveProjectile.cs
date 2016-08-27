using UnityEngine;
using System.Collections;

public class ExplosiveProjectile : MonoBehaviour {

	#region Public Properties

	public float fuseTime = 3.0f;

	#endregion

	#region Unity Methods
	
	// Update is called once per frame
	void Update () {
		fuseTime -= Time.deltaTime;
		if (fuseTime < 0.0f) {
			Explode ();
		}
	}

	void OnCollisionEnter (Collision other) {
		if (other.transform.tag == "Player") {
			other.transform.GetComponent<PlayerGlitch> ().Hit (this);
			Explode ();
		}
	}

	#endregion

	#region Public Methods

	public void Explode () {
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players) {
			player.GetComponent<PlayerGlitch> ().ProjectileExploded(this);
			Destroy(gameObject);
		}
	}

	#endregion
}
