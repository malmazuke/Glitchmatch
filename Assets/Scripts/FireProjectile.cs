using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FireProjectile : NetworkBehaviour {

	public GameObject projectilePrefab;
	public Transform projectileSpawn;
	public float projectileForce = 600.0f;
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}

		HandleInput ();
	}

	void HandleInput () {
		if (Input.GetButtonDown ("Fire1")) {
			CmdFire ();
		}
	}

	[Command]
	void CmdFire () {
		GameObject projectile = (GameObject)GameObject.Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
		Rigidbody rb = projectile.GetComponent<Rigidbody> ();

		rb.AddForce (projectile.transform.forward * projectileForce);

		NetworkServer.Spawn (projectile);
	}
}
