using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Kino;

public class PlayerGlitch : NetworkBehaviour {

	#region Public Properties

	public float maximumGlitchTime = 3.0f;

	#endregion

	#region Private Properties

	private Camera myCamera;
	private Datamosh datamosh;

	#endregion

	#region Unity Methods

	// Use this for initialization
	void Start () {
		if (!isLocalPlayer) {
			return;
		}

		myCamera = Camera.main;
		datamosh = myCamera.GetComponent<Datamosh> ();
	}

	#endregion

	#region Public Methods

	public void ProjectileExploded (ExplosiveProjectile projectile) {
		if (!isLocalPlayer) {
			return;
		}

		// Check to see if the projectile is visible
		Vector3 screenPoint = myCamera.WorldToViewportPoint (projectile.transform.position);
		if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1) {
			EnableGlitch ();
		}
	}

	#endregion

	#region Private Methods

	void EnableGlitch () {
		StartCoroutine (GlitchMe ());
	}

	IEnumerator GlitchMe () {
		datamosh.entropy = maximumGlitchTime;
		datamosh.Glitch ();
		while (datamosh.entropy > 0.0f) {
			float amountToDecrease = Time.deltaTime;
			if (datamosh.entropy < 1.0) {
				amountToDecrease /= 10.0f;
			}
			datamosh.entropy -= amountToDecrease;
			yield return null;
		}
		datamosh.entropy = 1.0f;
		datamosh.Reset ();
	}

	#endregion
}
