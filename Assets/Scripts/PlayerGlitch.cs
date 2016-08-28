using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Kino;

public class PlayerGlitch : NetworkBehaviour {

	#region Public Properties

	public float maximumGlitchTime = 3.0f;
	public float minimumRotationHitFactor = 30.0f;
	public float maximumRotationHitFactor = 60.0f;
	public AudioSource glitchAudioSource;

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

		// Gather components
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
			// TODO mask the raycast so that other projectiles do not interfere
			// TODO add maxDistance to the target - using distance between player and projectile
			Vector3 screenPointInPixels = new Vector3 (
				screenPoint.x * myCamera.pixelWidth, 
				screenPoint.y * myCamera.pixelHeight, 
				screenPoint.z);
			Ray ray = myCamera.ScreenPointToRay(screenPointInPixels);
			RaycastHit hitInfo;
			Physics.Raycast (ray, out hitInfo);
			// If we raycast and found that the target we hit was the same projectile
			// Then we enable the crazy glitch effect
			if (hitInfo.collider != null && hitInfo.collider.gameObject == projectile.gameObject) {
				// Calculate the distance between the two. This determines the glitch strength
				float distance = Vector3.Distance (transform.position, projectile.transform.position);
				if (distance <= 0.0f) distance = 0.1f;
				float glitchAmount = 1/(distance/2.0f);
				glitchAmount = Mathf.Clamp (glitchAmount, 0.0f, maximumGlitchTime);
				EnableGlitch (glitchAmount);
			}
		}
	}

	public void Hit (ExplosiveProjectile projectile) {
		FuckWithRotationAndTransform (projectile);
		projectile.Explode ();
		EnableGlitch (maximumGlitchTime);
	}

	#endregion

	#region Private Methods

	void EnableGlitch (float glitchStrength) {
		if (glitchAudioSource != null) {
			glitchAudioSource.Play ();
		}
		StartCoroutine (GlitchMe (glitchStrength));
	}

	IEnumerator GlitchMe (float glitchStrength) {
		datamosh.entropy += glitchStrength;
		datamosh.Glitch ();
		while (datamosh.entropy > 0.0f) {
			float amountToDecrease = Time.deltaTime;
			if (datamosh.entropy < 1.0) {
				amountToDecrease /= 10.0f;
			}
			datamosh.entropy -= amountToDecrease;
			yield return null;
		}
		datamosh.entropy = 0.0f;
		datamosh.Reset ();
	}

	void FuckWithRotationAndTransform (ExplosiveProjectile projectile) {
		// TODO: Fuck with the rotation
		CharacterController cc = GetComponent<CharacterController> ();
		cc.Move (new Vector3 (0.0f, 2.0f, 0.0f));
	}

	#endregion
}
