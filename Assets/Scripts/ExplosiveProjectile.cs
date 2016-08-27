using UnityEngine;
using System.Collections;

public class ExplosiveProjectile : MonoBehaviour {

	public float fuseTime = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		fuseTime -= Time.deltaTime;
		if (fuseTime < 0.0f) {
			Explode ();
		}
	}

	void Explode () {
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players) {
			player.GetComponent<PlayerGlitch> ().ProjectileExploded(this);
		}
		Destroy(gameObject);
	}
}
