using UnityEngine;
using System.Collections;

public class ExplosiveProjectile : MonoBehaviour {

	#region Public Properties

	public float fuseTime = 3.0f;
	public float flashTime = 0.2f;
	public GameObject[] particleSystemPrefabs;
	public AudioClip explosionSFX;
	public AudioClip beepSFX;
	public Material flashMaterial;

	#endregion

	#region Private Properties

	Material originalMaterial;
	private float nextFlash = 2.0f;

	#endregion

	#region Unity Methods

	void Start () {
		originalMaterial = GetComponent<Renderer> ().material;
	}

	// Update is called once per frame
	void Update () {
		fuseTime -= Time.deltaTime;

		if (fuseTime < nextFlash) {
			nextFlash -= nextFlash/2.0f;
			StartCoroutine (FlashGrenade ());
		}

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
		}

		foreach (GameObject g in particleSystemPrefabs) {
			Instantiate (g, transform.position, Quaternion.identity);
		}

		if (explosionSFX != null) {
			AudioSource.PlayClipAtPoint (explosionSFX, transform.position);
		}

		Destroy(gameObject);
	}

	#endregion

	#region Private Methods

	IEnumerator FlashGrenade () {
		if (beepSFX != null) {
			AudioSource.PlayClipAtPoint (beepSFX, transform.position);
		}
		Renderer myRenderer = GetComponent<Renderer> ();
		myRenderer.material = flashMaterial;
		yield return new WaitForSeconds (flashTime);
		myRenderer.material = originalMaterial;
	}

	#endregion
}
