using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FireProjectile : NetworkBehaviour {

	public GameObject projectilePrefab;
	public Transform projectileSpawn;
	public float projectileVelocity = 30.0f;
	
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
		Rigidbody shot = GameObject.Instantiate (projectilePrefab.GetComponent <Rigidbody> (), projectileSpawn.position, projectileSpawn.rotation) as Rigidbody;
		shot.velocity = shot.transform.forward * projectileVelocity;

		NetworkServer.Spawn (shot.gameObject);
	}
}
