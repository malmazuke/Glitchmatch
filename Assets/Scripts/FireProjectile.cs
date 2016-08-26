using UnityEngine;
using System.Collections;

public class FireProjectile : MonoBehaviour {

	public GameObject projectilePrefab;
	public Transform projectileSpawn;
	public float projectileForce = 20.0f;
	
	// Update is called once per frame
	void Update () {
		HandleInput ();
	}

	void HandleInput () {
		if (Input.GetButtonDown ("Fire1")) {
			Fire ();
		}
	}

	void Fire () {
		GameObject projectile = (GameObject)GameObject.Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
		Rigidbody rb = projectile.GetComponent<Rigidbody> ();

		rb.AddForce (projectile.transform.forward * projectileForce);
	}
}
