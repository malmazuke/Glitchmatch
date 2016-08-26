using UnityEngine;
using System.Collections;
using Kino;

public class PlayerGlitch : MonoBehaviour {

	#region Public Properties

	public Camera myCamera;
	public Datamosh datamosh;
	public float maximumGlitchTime = 5.0f;

	#endregion

	#region Private Properties

	private float glitchTimeRemaining = 0.0f;

	#endregion

	#region Unity Methods

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#endregion

	#region Public Methods

	public void ProjectileExploded (ExplosiveProjectile projectile) {
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
		glitchTimeRemaining = maximumGlitchTime;
		datamosh.entropy = 1.0f;
		datamosh.Glitch ();
		while (glitchTimeRemaining > 0.0f) {
			glitchTimeRemaining -= Time.deltaTime;
			datamosh.entropy = glitchTimeRemaining / maximumGlitchTime;
			yield return null;
		}
		datamosh.entropy = 1.0f;
		datamosh.Reset ();
	}

	#endregion
}
